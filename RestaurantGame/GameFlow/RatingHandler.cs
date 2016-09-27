using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Game : System.Web.UI.Page
    {
        private bool NeedToAskRating()
        {
            if (AlreadyAskedForRating || GameMode != GameMode.Advisor)
            {
                return false;
            }

            if (CurrentPositionNumber == 9)
            {
                return true;
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
                return (CurrentPositionNumber + 1 == RandomHuristicAskPosition);
            }

            if (AskPosition == AskPositionHeuristic.Optimal)
            {
                // AskPosition == Optimal
                /*
                 * 10 Stopping Value: 10
                 * 9 Stopping Value: 3
                 * 8 Stopping Value: 1
                 * 7 Stopping Value: 1
                 * 6 Stopping Value: 1
                 * 5 Stopping Value: 1
                 * 4 Stopping Value: 1
                 * 3 Stopping Value: 1
                 * 2 Stopping Value: 1
                 * 1 Stopping Value: 1
                 */
                var acceptedCandidateRank = CurrentCandidate.CandidateRank;
                if (CurrentPositionNumber == 8)
                {
                    return (acceptedCandidateRank <= 3);
                }

                return (acceptedCandidateRank == 1);
            }
            
            if (AskPosition == AskPositionHeuristic.MonteCarlo)
            {
                int[] accepted = new int[10];
                var stoppingPosition = -1;

                for (var index = 0; index < Positions.Count; index++)
                {
                    if (Positions[index].ChosenCandidate != null)
                    {
                        accepted[index] = Positions[index].ChosenCandidate.CandidateRank;
                        stoppingPosition++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (stoppingPosition == 0)
                {
                    return accepted[0] <= 3;
                } else if (stoppingPosition == 1)
                {
                    return (accepted[0] == 4 && accepted[1] <= 2);
                }

                bool shouldAsk = ShouldAsk(accepted, stoppingPosition, new Random());

                return shouldAsk;
            }

            return false;
        }

        protected void RateAdvisor()
        {
            TimerGame.Enabled = false;

            MultiView2.ActiveViewIndex = 1;

            dbHandler.UpdateTimesTable(GameState.Rate);
        }

        protected void btnRate_Click(object sender, EventArgs e)
        {
            string userRating = ratingHdnValue.Value;

            if (string.IsNullOrEmpty(userRating) || userRating == "0")
            {
                return;
            }

            string reason = reasonTxtBox.Text;
            if (string.IsNullOrEmpty(reason.Trim()))
            {
                Alert.Show("Please explain your rating selection in the text box.");
                return;
            }

            int agentRating = int.Parse(userRating);

            SaveRatingToDB(agentRating);

            MultiView2.ActiveViewIndex = 0;

            dbHandler.UpdateTimesTable(GameState.AfterRate);

            TimerGame.Enabled = true;
        }

        private void SaveRatingToDB(int adviserRating)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                sqlConnection1.Open();

                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserRatings (UserId, AdviserRating, Position1Rank, Position2Rank, " +
                    "Position3Rank, Position4Rank, Position5Rank, Position6Rank, Position7Rank, Position8Rank, Position9Rank, Position10Rank, TotalPrizePoints, " +
                    " InstructionsTime, AskPosition, VectorNum, Reason) " +
                    " VALUES (@UserId, @AdviserRating, @Position1Rank, @Position2Rank, @Position3Rank, @Position4Rank, " +
                    "@Position5Rank, @Position6Rank, @Position7Rank, @Position8Rank, @Position9Rank, @Position10Rank, @TotalPrizePoints, " +
                    "@InstructionsTime, @AskPosition, @VectorNum, @Reason)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AdviserRating", adviserRating.ToString());
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
                    cmd.Parameters.AddWithValue("@TotalPrizePoints", Common.GetTotalPrizePoints(Positions));
                    cmd.Parameters.AddWithValue("@InstructionsTime", Math.Round(InstructionsStopwatch.Elapsed.TotalMinutes, 2));
                    cmd.Parameters.AddWithValue("@AskPosition", AskPosition.ToString());
                    cmd.Parameters.AddWithValue("@VectorNum", VectorNum);
                    cmd.Parameters.AddWithValue("@Reason", reasonTxtBox.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetChosenPositionToInsertToDb(int positionIndex)
        {
            var position = GetPosition(positionIndex - 1);

            return position.ChosenCandidate == null ? null : position.ChosenCandidate.CandidateRank.ToString();
        }
    }
}
