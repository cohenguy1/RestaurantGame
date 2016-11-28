using RestaurantGame.Enums;
using RestaurantGame.Logic;
using System;
using System.Collections.Generic;

namespace RestaurantGame
{
    public partial class Game : System.Web.UI.Page
    {
        private List<Candidate> GenerateCandidatesForPosition()
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

            return positionCandidates;
        }

        public void InitCandidatesForPosition(List<Candidate> positionCandidates, Random randomGenerator)
        {
            List<int> ranks = new List<int>();
            for (var index = 1; index <= DecisionMaker.NumberOfCandidates; index++)
            {
                ranks.Add(index);
            }

            var ranksRemaining = DecisionMaker.NumberOfCandidates;
            int position;

            for (var index = 0; index < DecisionMaker.NumberOfCandidates; index++)
            {
                if (ranksRemaining > 1)
                {
                    position = randomGenerator.Next(1, ranksRemaining + 1) - 1;
                }
                else
                {
                    position = 0;
                }
                positionCandidates[index].CandidateRank = ranks[position];
                positionCandidates[index].CandidateAccepted = false;

                ranks.RemoveAt(position);
                ranksRemaining--;
            }
        }
    }
}