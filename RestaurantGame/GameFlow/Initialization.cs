using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
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
                AskPosition = GetAskPosition(UserId == "friend");
            }
            catch (Exception)
            {
                NoHitSlotsAvailable();
            }
        }

        private void NoHitSlotsAvailable()
        {
            MultiView1.ActiveViewIndex = 10;
        }


        private void GenerateCandidatesForPosition()
        {
            var positionNumber = CurrentPositionNumber;

            var positionCandidates = new List<Candidate>();

            int[] candidateRanks = GetCandidateRanksForPosition(positionNumber);

            for (var candidateIndex = 0; candidateIndex < NumberOfCandidates; candidateIndex++)
            {
                var newCandidate = new Candidate()
                {
                    CandidateState = CandidateState.New,
                    CandidateNumber = candidateIndex,
                    CandidateAccepted = false,
                    CandidateRank = candidateRanks[candidateIndex]
                };

                positionCandidates.Add(newCandidate);
            }

            PositionCandidates = positionCandidates;
        }

        private void GenerateCandidatesByNow()
        {
            CandidatesByNow = new List<Candidate>();
        }

    }
}