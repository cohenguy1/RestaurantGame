using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;

namespace RestaurantGame
{
    // Next Generation
    // TODO convert instructions to javascript

    public partial class Default : System.Web.UI.Page
    {
        public int StartTimerInterval = Game.StartTimerInterval;

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

                    TimerInterval = StartTimerInterval;
                    TimerEnabled = true;

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
    }
}