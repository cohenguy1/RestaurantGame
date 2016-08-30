using RestaurantGame.Enums;
using RestaurantGame.Logic;
using System.Collections.Generic;

namespace RestaurantGame
{
    public partial class Game : System.Web.UI.Page
    {
        private void GenerateCandidatesForPosition()
        {
            var positionNumber = CurrentPositionNumber;

            var positionCandidates = new List<Candidate>();

            int[] candidateRanks = dbHandler.GetCandidateRanksForPosition(positionNumber);

            for (var candidateIndex = 0; candidateIndex < NumberOfCandidates; candidateIndex++)
            {
                var newCandidate = new Candidate()
                {
                    CandidateState = CandidateState.Interview,
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