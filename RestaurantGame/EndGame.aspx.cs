using Amazon.WebServices.MechanicalTurk;
using RestaurantGame.Enums;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;

namespace RestaurantGame
{
    public partial class EndGame : System.Web.UI.Page
    {
        public int InitialBonus = Default.InitialBonus;

        private string _bonusReason = "Thank you for participating in the Restaurant Game";

        private static SimpleClient _client = new SimpleClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GameStopwatch.Stop();
                
                double averageRank = Common.CalculateAveragePosition(Positions);
                double bonus = InitialBonus - averageRank;

                AverageRank.Text = averageRank.ToString("0.0");
                Bonus.Text = bonus.ToString() + " cents";

                dbHandler.UpdateTimesTable(GameState.EndGame);
            }
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

            double averageRank = Common.CalculateAveragePosition(Positions);
            double bonusAmount = (InitialBonus - averageRank) / 100.0;
            decimal bonusDecimal = Convert.ToDecimal(Math.Round(bonusAmount, 2));

            SendFeedback(bonusAmount);

            rewardBtn.Enabled = false;

            if (workerId != "friend")
            {
                IncreaseAskPositionCount();

                _client.GrantBonus(workerId, bonusDecimal, assignmentId, _bonusReason);

                DisposeSession();

                Alert.RedirectAndPOST(Page, "https://www.mturk.com/mturk/externalSubmit", data);
            }
        }

        public void IncreaseAskPositionCount()
        {
            string nextAskPosition = null;

            switch (AskPosition)
            {
                case AskPositionHeuristic.First:
                    nextAskPosition = AskPositionHeuristic.Optimal.ToString();
                    break;
                case AskPositionHeuristic.Optimal:
                    nextAskPosition = AskPositionHeuristic.Last.ToString();
                    break;
                case AskPositionHeuristic.Last:
                    nextAskPosition = AskPositionHeuristic.Random.ToString();
                    break;
                case AskPositionHeuristic.Random:
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
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserFeedback (UserId, Feedback, TotalTime, Bonus) VALUES (@UserId, @Feedback, @TotalTime, @Bonus)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@Feedback", feedback);
                        cmd.Parameters.AddWithValue("@TotalTime", Math.Round(GameStopwatch.Elapsed.TotalMinutes, 1));
                        cmd.Parameters.AddWithValue("@Bonus", Math.Round(bonus, 2));
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();

                        sqlConnection1.Close();
                    }
                }
            }
            catch (SQLiteException)
            {
            }
        }

        private void DisposeSession()
        {
            if (dbHandler != null)
            {
                dbHandler.Dispose();
            }
            
            if (Positions != null)
            {
                Positions = null;
            }

            if (PositionCandidates != null)
            {
                PositionCandidates = null;
            }
        }
    }
}