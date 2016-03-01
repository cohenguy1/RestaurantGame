using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        private void GenerateCandidatesForPosition()
        {
            var positionNumber = CurrentPositionNumber;

            var positionCandidates = new List<Candidate>();

            int[] candidateRanks = DbHandler.GetCandidateRanksForPosition(positionNumber);

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