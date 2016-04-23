﻿using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Quiz : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dbHandler.UpdateTimesTable(GameState.Quiz);

                NumOfWrongQuizAnswers = 0;
            }
        }

        public const int MaxNumOfWrongQuizAnswers = 3;

        protected void btnNextToProceedToGame_Click(object sender, EventArgs e)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            var tryNumber = NumOfWrongQuizAnswers + 1;
            var answer1 = rbl1.SelectedIndex;
            var answer2 = rbl2.SelectedIndex;
            var answer3 = rbl3.SelectedIndex;
            var answer4 = rbl4.SelectedIndex;

            var correct = (rbl1.SelectedIndex == 0) &&
                          (rbl2.SelectedIndex == 3) &&
                          (rbl3.SelectedIndex == 3) &&
                          (rbl4.SelectedIndex == 1);

            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand
                        ("INSERT INTO Quiz (UserId, TryNumber, Answer1, Answer2, Answer3, Answer4, Correct) " +
                         "VALUES (@UserId, @TryNumber, @Answer1, @Answer2, @Answer3, @Answer4, @Correct)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@TryNumber", tryNumber);
                        cmd.Parameters.AddWithValue("@Answer1", answer1);
                        cmd.Parameters.AddWithValue("@Answer2", answer2);
                        cmd.Parameters.AddWithValue("@Answer3", answer3);
                        cmd.Parameters.AddWithValue("@Answer4", answer4);
                        cmd.Parameters.AddWithValue("@Correct", correct.ToString());
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error: " + Environment.NewLine + ex.Message);
                return;
            }


            if (correct)
            {
                Response.Redirect("ProceedToGame.aspx");
            }
            else
            {
                NumOfWrongQuizAnswers++;

                if (NumOfWrongQuizAnswers >= MaxNumOfWrongQuizAnswers)
                {
                    Response.Redirect("WrongQuiz.aspx");
                }
                else
                {
                    var triesRemaining = MaxNumOfWrongQuizAnswers - NumOfWrongQuizAnswers;
                    Alert.Show("Wrong answer, please try again. You have " + triesRemaining + " tries remaining.");
                }
            }
        }
    }
}