using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        private bool NeedToAskRating()
        {
            if (AlreadyAskedForRating || GameMode != GameMode.Advisor)
            {
                return false;
            }

            if (AskPosition == AskPositionHeuristic.First)
            {
                return (CurrentPositionNumber + 1 == 1);
            }
            
            if (AskPosition == AskPositionHeuristic.Last)
            {
                return (CurrentPositionNumber + 1 == 10);
            }

            if (AskPosition == AskPositionHeuristic.Random)
            {
                return (CurrentPositionNumber + 1 == DbHandler.RandomHuristicAskPosition);
            }

            // AskPosition == Optimal
            /*
             * 10 Stopping Value: 20
             * 9 Stopping Value: 2
             * 8 Stopping Value: 1
             * 7 Stopping Value: 1
             * 6 Stopping Value: 1
             * 5 Stopping Value: 1
             * 4 Stopping Value: 1
             * 3 Stopping Value: 1
             * 2 Stopping Value: 1
             * 1 Stopping Value: 1
             */
            if (CurrentPositionNumber == 9)
            {
                return true;
            }

            var acceptedCandidateRank = CurrentCandidate.CandidateRank;
            if (CurrentPositionNumber == 8)
            {
                if (acceptedCandidateRank <= 2)
                {
                    return true;
                }
            }

            return (acceptedCandidateRank == 1);
        }

        protected void RateAdvisor()
        {
            TimerGame.Enabled = false;

            MultiView2.ActiveViewIndex = 1;
        }

        protected void btnRate_Click(object sender, EventArgs e)
        {
            int agentRating = RatingRbL.SelectedIndex + 1;

            SaveRatingToDB(agentRating);

            MultiView2.ActiveViewIndex = 0;

            TimerGame.Enabled = true;
        }

        private void SaveRatingToDB(int adviserRating)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserRatings (UserId, AdviserRating, RatingPosition, Position1Rank, Position2Rank, " +
                    "Position3Rank, Position4Rank, Position5Rank, Position6Rank, Position7Rank, Position8Rank, Position9Rank, Position10Rank, AvgRanking, InstructionsTime ) " +
                    " VALUES (@UserId, @AdviserRating, @RatingPosition, @Position1Rank, @Position2Rank, @Position3Rank, @Position4Rank, " +
                    "@Position5Rank, @Position6Rank, @Position7Rank, @Position8Rank, @Position9Rank, @Position10Rank, @AvgRanking, @InstructionsTime)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AdviserRating", adviserRating.ToString());
                cmd.Parameters.AddWithValue("@RatingPosition", (CurrentPositionNumber + 1).ToString());
                cmd.Parameters.AddWithValue("@Position1Rank", GetChosenPositionToInsertToDb(1));
                cmd.Parameters.AddWithValue("@Position2Rank", GetChosenPositionToInsertToDb(2));
                cmd.Parameters.AddWithValue("@Position3Rank", GetChosenPositionToInsertToDb(3));
                cmd.Parameters.AddWithValue("@Position4Rank", GetChosenPositionToInsertToDb(4));
                cmd.Parameters.AddWithValue("@Position5Rank", GetChosenPositionToInsertToDb(5));
                cmd.Parameters.AddWithValue("@Position6Rank", GetChosenPositionToInsertToDb(6));
                cmd.Parameters.AddWithValue("@Position7Rank", GetChosenPositionToInsertToDb(7));
                cmd.Parameters.AddWithValue("@Position8Rank", GetChosenPositionToInsertToDb(8));
                cmd.Parameters.AddWithValue("@Position9Rank", GetChosenPositionToInsertToDb(9));
                cmd.Parameters.AddWithValue("@Position10Rank", GetChosenPositionToInsertToDb(10));
                cmd.Parameters.AddWithValue("@AvgRanking", CalculateAveragePosition());
                cmd.Parameters.AddWithValue("@InstructionsTime", Math.Round(InstructionsStopwatch.Elapsed.TotalMinutes, 3));
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private string GetChosenPositionToInsertToDb(int positionIndex)
        {
            var position = GetPosition(positionIndex - 1);

            return position.ChosenCandidate == null ? "NULL" : position.ChosenCandidate.CandidateRank.ToString();
        }
    }
}
 