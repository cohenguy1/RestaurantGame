﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Web;
using RestaurantGame.Enums;
using RestaurantGame.Logic;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        public const int InitialBonus = 20;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GameMode == GameMode.Initial)
                {
                    string assignmentId = Request.QueryString["assignmentId"];

                    // friend assigment
                    if (assignmentId == null)
                    {
                        Session["user_id"] = "friend";
                        Session["turkAss"] = "turkAss";
                        Session["hitId"] = "hit id friend";
                        btnNextToInfo.Enabled = true;
                    }
                    //from AMT but did not took the assigment
                    else if (assignmentId.Equals("ASSIGNMENT_ID_NOT_AVAILABLE"))
                    {
                        btnNextToInfo.Enabled = false;
                        return;
                    }
                    //from AMT and accepted the assigment - continue to experiment
                    else
                    {
                        Session["user_id"] = Request.QueryString["workerId"];   // save participant's user ID
                        Session["turkAss"] = assignmentId;                      // save participant's assignment ID
                        Session["hitId"] = Request.QueryString["hitId"];        // save the hit id
                        btnNextToInfo.Enabled = true;
                    }

                    dbHandler = new DbHandler();

                    GameStateStopwatch = new Stopwatch();
                    GameStateStopwatch.Start();

                    DecideRandomStuff();                    

                    GeneratePositions();
                }
            }
        }

        private void GeneratePositions()
        {
            var positions = new List<Position>();

            positions.Add(new Position(RestaurantPosition.Manager));
            positions.Add(new Position(RestaurantPosition.HeadChef));
            positions.Add(new Position(RestaurantPosition.Cook));
            positions.Add(new Position(RestaurantPosition.Baker));
            positions.Add(new Position(RestaurantPosition.Dishwasher));
            positions.Add(new Position(RestaurantPosition.Waiter1));
            positions.Add(new Position(RestaurantPosition.Waiter2));
            positions.Add(new Position(RestaurantPosition.Waiter3));
            positions.Add(new Position(RestaurantPosition.Host));
            positions.Add(new Position(RestaurantPosition.Bartender));

            Positions = positions;

            var acceptedCandidates = new int[positions.Count];
            AcceptedCandidates = acceptedCandidates;
        }

        private void DecideRandomStuff()
        {
            Random rand = new Random();
            int randHim = rand.Next(2);
            if (randHim == 1)
            {
                backgroundText.Text = backgroundText.Text.Replace("him", "her");
                backgroundText2.Text = backgroundText2.Text.Replace(", he", ", she");
            }

            try
            {
                AskPosition = dbHandler.GetAskPosition(UserId == "friend");
            }
            catch (Exception)
            {
                NoHitSlotsAvailable();
            }
        }

        private void NoHitSlotsAvailable()
        {
            Response.Redirect("SlotsProblem.aspx");
        }

        protected void btnNextToInfo_Click(object sender, EventArgs e)
        {
            GameStopwatch = new Stopwatch();
            GameStopwatch.Start();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            if (!UserId.Equals("friend"))
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("Select UserId from [UserFeedback] Where UserId='" + UserId + "'"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();

                        string userId = (string)cmd.ExecuteScalar();

                        if (userId == null)
                        {
                            //new user - insert to DB
                            using (SQLiteCommand cmd2 = new SQLiteCommand("INSERT INTO Users (UserId, Assignment_Id, hitId, time) VALUES (@UserId, @Assignment_Id, @hitId, @time)"))
                            {
                                cmd2.CommandType = CommandType.Text;
                                cmd2.Connection = sqlConnection1;
                                cmd2.Parameters.AddWithValue("@UserId", UserId);
                                cmd2.Parameters.AddWithValue("@Assignment_Id", (string)Session["turkAss"]);
                                cmd2.Parameters.AddWithValue("@hitId", (string)Session["hitId"]);
                                cmd2.Parameters.AddWithValue("@time", DateTime.Now.ToString());
                                cmd2.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            Alert.Show("You already participated in this game. Please return the HIT");
                            return;
                        }
                    }
                }
            }
            else
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Users (UserId, Assignment_Id, hitId, time) VALUES (@UserId, @Assignment_Id, @hitId, @time)"))
                    {
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
            }


            string heightS = HttpContext.Current.Request.Params["clientScreenHeight"];
            string widthS = HttpContext.Current.Request.Params["clientScreenWidth"];

            int height;
            int width;
            bool heightConvertResult = int.TryParse(heightS, out height);
            bool widthConvertResult = int.TryParse(widthS, out width);

            if (Request.Browser.IsMobileDevice)
            {
                Response.Redirect("ResolutionProblem.aspx");
                return;
            }

            if (heightConvertResult && widthConvertResult)
            {
                if (height < 200 || width < 700)
                {
                    Response.Redirect("ResolutionProblem.aspx");
                    return;
                }
            }

            // experiment opened not from chrome
            HttpBrowserCapabilities browser = Request.Browser;
            var browserType = browser.Type.ToLower();

            if (!browserType.Contains("chrome"))
            {
                Response.Redirect("BrowserProblem.aspx");
            }

            Response.Redirect("UserInfoPage.aspx");
        }
    }
}