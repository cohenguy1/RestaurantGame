using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        public const int MaxNumOfWrongQuizAnswers = 5;

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
                    SQLiteCommand cmd = new SQLiteCommand
                        ("INSERT INTO Quiz (UserId, TryNumber, Answer1, Answer2, Answer3, Answer4, Correct) " +
                         "VALUES (@UserId, @TryNumber, @Answer1, @Answer2, @Answer3, @Answer4, @Correct)");
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
            catch (Exception ex)
            {
                Alert.Show("Error: " + Environment.NewLine + ex.Message);
                return;
            }



            if (correct)
            {
                MultiView1.ActiveViewIndex = 5;

                UpdateTimesTable(GameState.GameStart);
            }
            else
            {
                NumOfWrongQuizAnswers++;

                if (NumOfWrongQuizAnswers >= MaxNumOfWrongQuizAnswers)
                {
                    QuizWrongLbl.Text = "&nbsp;You have been wrong for " + MaxNumOfWrongQuizAnswers + " times.";
                    MultiView1.ActiveViewIndex = 8;
                }
                else
                {
                    var triesRemianing = MaxNumOfWrongQuizAnswers - NumOfWrongQuizAnswers;
                    Alert.Show("Wrong answer, please try again. You have " + triesRemianing + " tries remianing.");
                }
            }
        }
    }
}