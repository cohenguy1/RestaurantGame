using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantGame.Enums;
using System.Data.SQLite;
using System.Data;

namespace RestaurantGame
{
    public partial class Game : System.Web.UI.Page
    {
        protected void btnThumbsDown_Click(object sender, EventArgs e)
        {
            var currentCandidate = CurrentCandidate;

            if (currentCandidate.CandidateState == CandidateState.Interview)
            {
                UpdateDbInUserDecision(currentCandidate, false);
            }

            if (CurrentCandidateNumber >= NumberOfCandidates - 1)
            {
                Alert.Show("You cannot reject the last candidate, no more candidates avaliable.");
                return;
            }

            AcceptCandidateByUser(currentCandidate, false);
            IncreaseButtonSize(btnThumbsDown);
        }

        protected void btnThumbsUp_Click(object sender, EventArgs e)
        {
            var currentCandidate = CurrentCandidate;

            if (currentCandidate.CandidateState == CandidateState.Interview)
            {
                UpdateDbInUserDecision(currentCandidate, true);
            }

            AcceptCandidateByUser(currentCandidate, true);
            IncreaseButtonSize(btnThumbsUp);
        }

        private void AcceptCandidateByUser(Candidate currentCandidate, bool accepted)
        {
            if (currentCandidate == null)
            {
                EnterNewCandidate();
            }
            else if (currentCandidate.CandidateState == CandidateState.Interview)
            {
                currentCandidate.CandidateAccepted = accepted;

                currentCandidate.CandidateState = CandidateState.Completed;
            }

            TimerGame.Interval = 500;
            TimerGame.Enabled = true;
            SessionState = SessionState.Running;
        }

        private void UpdateDbInUserDecision(Candidate currentCandidate, bool accepted)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            var currentCandidateNumber = CurrentCandidateNumber + 1;
            var decision = accepted ? "Accept" : "Reject";
            var currentCandidateRank = CandidatesByNow.IndexOf(currentCandidate) + 1;

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Training (UserId, PositionNumber, CandidateNumber, CandidateRank , Decision ) " +
                    " VALUES (@UserId, @PositionNumber, @CandidateNumber, @CandidateRank, @Decision)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@PositionNumber", CurrentPositionNumber);
                cmd.Parameters.AddWithValue("@CandidateNumber", currentCandidateNumber);
                cmd.Parameters.AddWithValue("@CandidateRank",currentCandidateRank);
                cmd.Parameters.AddWithValue("@Decision", decision);
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();

                sqlConnection1.Close();
            }
        }
    }
}