using RestaurantGame.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantGame
{
    public static class Common
    {
        public static double CalculateAveragePosition(IEnumerable<Position> positions)
        {
            double average = positions.Where(position => position.ChosenCandidate != null).
                Average(pos => pos.ChosenCandidate.CandidateRank);

            // with one decimal precision
            return Math.Round(average, 1);
        }
    }
}