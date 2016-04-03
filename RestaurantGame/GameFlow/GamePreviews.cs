﻿using Amazon.WebServices.MechanicalTurk;
using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        protected void btnNextToInfo_Click(object sender, EventArgs e)
        {
            GameStopwatch = new Stopwatch();
            GameStopwatch.Start();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            dbHandler.UpdateTimesTable(GameState.UserInfo);

            if (!UserId.Equals("friend"))
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand("Select UserId from [UserRatings] Where UserId='" + UserId + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    string userId = (string)cmd.ExecuteScalar();

                    if (userId == null)
                    {
                        //new user - insert to DB
                        cmd = new SQLiteCommand("INSERT INTO Users (UserId, Assignment_Id, hitId, time) VALUES (@UserId, @Assignment_Id, @hitId, @time)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@Assignment_Id", (string)Session["turkAss"]);
                        cmd.Parameters.AddWithValue("@hitId", (string)Session["hitId"]);
                        cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString());
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        Alert.Show("You already participated in this game. Please return the HIT");
                        return;
                    }
                }
            }
            else
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Users (UserId, Assignment_Id, hitId, time) VALUES (@UserId, @Assignment_Id, @hitId, @time)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Assignment_Id", (string)Session["turkAss"]);
                    cmd.Parameters.AddWithValue("@hitId", (string)Session["hitId"]);
                    cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString());
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                }
            }


            string heightS = HttpContext.Current.Request.Params["clientScreenHeight"];
            string widthS = HttpContext.Current.Request.Params["clientScreenWidth"];

            int height;
            int width;
            bool heightConvertResult = int.TryParse(heightS, out height);
            bool widthConvertResult = int.TryParse(widthS, out width);

            if (Request.Browser.IsMobileDevice)
            {
                MultiView1.ActiveViewIndex = 9;
                return;
            }

            if (heightConvertResult && widthConvertResult)
            {
                if (height < 200 || width < 700)
                {
                    MultiView1.ActiveViewIndex = 9;
                    return;
                }
            }

            // experiment opened from iexplorer
            HttpBrowserCapabilities browser = Request.Browser;
            var browserType = browser.Type.ToLower();
            if (browserType.Contains("internetexplorer"))
            {
                MultiView1.ActiveViewIndex = 11;
                return;
            }

            Response.Redirect("UserInfoPage.aspx");
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

        protected void rewardBtn_Click(object sender, EventArgs e)
        {
            string workerId = UserId;

            dbHandler.UpdateTimesTable(GameState.CollectedPrize);

            string assignmentId = (string)Session["turkAss"];

            NameValueCollection data = new NameValueCollection();
            data.Add("assignmentId", assignmentId);
            data.Add("workerId", workerId);
            data.Add("hitId", (string)Session["hitId"]);

            double averageRank = CalculateAveragePosition();
            double bonusAmount = (InitialBonus - averageRank)/100.0;
            decimal bonusDecimal = Convert.ToDecimal(bonusAmount);

            if (workerId != "friend")
            {
                //SimpleClient client = new SimpleClient();
                //client.GrantBonus(workerId, bonusDecimal, assignmentId, "Thanks for doing great work!");

                Alert.RedirectAndPOST(this.Page, "https://www.mturk.com/mturk/externalSubmit", data);

                IncreaseAskPositionCount();
            }

            SendFeedback(bonusAmount);

            rewardBtn.Enabled = false;
        }

        public void IncreaseAskPositionCount()
        {
            string nextAskPosition = null;

            switch (AskPosition)
            {
                case AskPositionHeuristic.First:
                    nextAskPosition = AskPositionHeuristic.Last.ToString();
                    break;
                case AskPositionHeuristic.Last:
                    nextAskPosition = AskPositionHeuristic.Random.ToString();
                    break;
                case AskPositionHeuristic.Random:
                    nextAskPosition = AskPositionHeuristic.Optimal.ToString();
                    break;
                case AskPositionHeuristic.Optimal:
                    nextAskPosition = "Done";
                    break;
            }

            DbHandler dbHandler = new DbHandler();
            dbHandler.SetVectorNextAskPosition(nextAskPosition);
        }

        private void SendFeedback(double bonus)
        {
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string feedback = feedbackTxtBox.Text;

            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserFeedback (UserId, Feedback, TotalTime, Bonus) VALUES (@UserId, @Feedback, @TotalTime, @Bonus)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Feedback", feedback);
                    cmd.Parameters.AddWithValue("@TotalTime", Math.Round(GameStopwatch.Elapsed.TotalMinutes, 1));
                    cmd.Parameters.AddWithValue("@Bonus", Math.Round(bonus, 3));
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();

                    sqlConnection1.Close();
                }
            }
            catch (SQLiteException ex)
            {
            }
        }
    }
}