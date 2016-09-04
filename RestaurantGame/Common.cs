using RestaurantGame.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantGame
{
    public static class Common
    {
        public static int GetTotalPrizePoints(IEnumerable<Position> positions)
        {
            int totalPrizePoints = positions.Where(position => position.ChosenCandidate != null).
                Sum(pos => 110 - pos.ChosenCandidate.CandidateRank * 10);

            // with one decimal precision
            return totalPrizePoints;
        }
    }
}