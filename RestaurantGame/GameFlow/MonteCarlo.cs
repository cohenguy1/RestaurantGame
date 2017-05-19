﻿using RestaurantGame.Logic;
using System;
using System.Collections.Generic;

namespace RestaurantGame
{
    public class MonteCarlo
    {
        public const double Alpha = 0.347;

        private static Dictionary<int, double> minimalRankForAsk = new Dictionary<int, double>()
        {
            {10, 10 },
            {9, 3.7040364 },
            {8, 3.4505329875 },
            {7, 3.29737467578125 },
            {6, 3.15378875854492 },
            {5, 3.05282991048813 },
            {4, 2.956603508434 },
            {3, 2.89545964879543 },
            {2, 2.83622653477058 },
            {1, 2.778844455559 }
        };

        public static bool ShouldAsk(int[] accepted, int stoppingDecision)
        {
            double exponentialSmoothing = accepted[0];
            for (int i = 1; i <= stoppingDecision; i++)
            {
                exponentialSmoothing = Alpha * accepted[i] + (1 - Alpha) * exponentialSmoothing;
            }

            var ask = exponentialSmoothing <= minimalRankForAsk[stoppingDecision + 1];
            return ask;
        }
    }
}
