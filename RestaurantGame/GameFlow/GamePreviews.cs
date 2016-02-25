﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        protected void btnNextToInfo_Click(object sender, EventArgs e)
        {
            if (!UserId.Equals("friend"))
            {
                String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand("Select Assignment_Id from [User] Where UserId='" + UserId + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    string assignmentId = (string)cmd.ExecuteScalar();

                    if (assignmentId == null)
                    {
                        //new user -insert to DB
                        DateTime curentT = DateTime.UtcNow;
                        cmd = new SQLiteCommand("insert into [User] (UserId, Assignment_Id,time) VALUES ('" + UserId + "','" + Session["turkAss"] + "','" + curentT.ToString() + "')");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        Alert.Show("You already participated in this game. Please return the HIT");
                        return;
                    }
                }
            }

            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnNextToTraining_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 6;

            GameMode = GameMode.Training;
            DisableThumbsButtons();
            TrainingPassed = 0;

            CurrentPositionNumber = 0;
            StartInterviewsForPosition(0);
        }

        protected void btnNextToInstructions_Click(object sender, EventArgs e)
        {
            // Save user info to DB
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string mobile = "not_mobile";
            if (Request.Browser.IsMobileDevice)
            {
                mobile = "mobile_user";
            }

            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserInfo (UserId, Gender, Age, Education, Nationality, Reason, Mobile ) VALUES (@UserId, @Gender, @Age, @Education,@Nationality,@Reason,@Mobile)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Gender", DropDownList1.Text);
                    cmd.Parameters.AddWithValue("@Age", DropDownList2.Text);
                    cmd.Parameters.AddWithValue("@Education", DropDownList3.Text);
                    cmd.Parameters.AddWithValue("@Nationality", DropDownList4.Text);
                    cmd.Parameters.AddWithValue("@Reason", DropDownList5.Text);
                    cmd.Parameters.AddWithValue("@Mobile", mobile);
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                Alert.Show("Error: " + Environment.NewLine + ex.Message);
                return;
            }



            MultiView1.ActiveViewIndex = 2;
            MultiviewInstructions.ActiveViewIndex = 0;
            ProgressBar1.Value = 0;
        }

        protected void btnNextToGame_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 6;

            GameMode = GameMode.Advisor;
            RestoreButtonSizes(btnThumbsDown, btnThumbsUp);

            MultiView2.ActiveViewIndex = 0;

            ClearCandidateImages();

            ImageInterview.Visible = false;
            ImageManForward.Visible = true;

            CurrentPositionNumber = 0;

            ClearPositionsTable();

            StartInterviewsForPosition(0);
        }
    }
}