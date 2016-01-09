using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
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

            Session[PositionsStr] = positions;

            var acceptedCandidates = new int[positions.Count];
            Session[AcceptedCandidates] = acceptedCandidates;
        }

        private void GenerateCandidatesForPosition()
        {
            var positionCandidates = new List<Candidate>();

            for (var candidateIndex = 0; candidateIndex < PositionCandidatesNumber; candidateIndex++)
            {
                var newCandidate = new Candidate()
                {
                    CandidateState = CandidateState.New,
                    CandidateNumber = candidateIndex,
                    CandidateAccepted = false
                };

                positionCandidates.Add(newCandidate);
            }

            var ranks = new List<int>();
            for (var index = 1; index <= PositionCandidatesNumber; index++)
            {
                ranks.Add(index);
            }

            var ranksRemaining = PositionCandidatesNumber;
            var randomGenerator = new Random();

            for (var index = 0; index < PositionCandidatesNumber; index++)
            {
                var position = randomGenerator.Next(1, ranksRemaining + 1) - 1;

                positionCandidates[index].CandidateRank = ranks[position];

                ranks.RemoveAt(position);
                ranksRemaining--;
            }

            Session[PositionCandidiatesStr] = positionCandidates;
        }

        private void GenerateCandidatesByNow()
        {
            var candidatesByNow = new List<Candidate>();
            Session[CandidatesByNowStr] = candidatesByNow;
        }

    }
}