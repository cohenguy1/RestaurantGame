using RestaurantGame.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantGame
{
    public static class Common
    {
        public static double GetTotalBonus(IEnumerable<Position> positions)
        {
            int totalBonus = positions.Where(position => position.ChosenCandidate != null).
                Sum(pos => 10 - pos.ChosenCandidate.CandidateRank + 1);

            // with one decimal precision
            return Math.Round(totalBonus * 0.2, 1);
        }
    }
}