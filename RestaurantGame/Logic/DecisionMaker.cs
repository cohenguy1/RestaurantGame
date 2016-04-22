using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public class DecisionMaker
    {
        private static DecisionMaker _instance = null;

        public const int NumberOfCandidates = 20;

        private static double[] c = new double[NumberOfCandidates + 1];

        public static int[] StoppingRule = new int[NumberOfCandidates + 1];

        public static DecisionMaker GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DecisionMaker();
            }

            return _instance;
        }

        private DecisionMaker()
        {
            int n = NumberOfCandidates;

            c[n - 1] = (n + 1) / 2.0;

            StoppingRule[n] = n;
            StoppingRule[n - 1] = (int)Math.Floor((n - 1 + 1) / ((double)n + 1) * c[n - 1]);

            for (var i = n - 1; i >= 1; i--)
            {
                c[i - 1] = 1 / (double)(i) * (((n + 1) / (double)(i + 1)) * (StoppingRule[i] * (StoppingRule[i] + 1)) / 2.0 + (i - StoppingRule[i]) * c[i]);
                StoppingRule[i - 1] = (int)Math.Floor((i) / ((double)n + 1) * c[i - 1]);
            }
        }

        public bool Decide(List<Candidate> candidatesByNow, int newCandidateIndex)
        {
            return (newCandidateIndex + 1 <= StoppingRule[candidatesByNow.Count]);
        }

        public bool Decide2(List<Candidate> candidatesByNow, Candidate newCandidate)
        {
            if (candidatesByNow.Count <= (int)Math.Sqrt(NumberOfCandidates))
            {
                return false;
            }

            if (candidatesByNow.Count == NumberOfCandidates - 1)
            {
                return true;
            }

            var firstSqrtCandidates = candidatesByNow.Where(candidate => candidate.CandidateNumber < Math.Sqrt(NumberOfCandidates));

            var minRank = firstSqrtCandidates.Min(candidate => candidate.CandidateRank);

            return (newCandidate.CandidateRank < minRank);
        }

        public int GetCandidateRelativePosition(List<Candidate> candidatesByNow, Candidate newCandidate)
        {
            int newCandidateIndex = 0;
            foreach (var candidate in candidatesByNow)
            {
                if (candidate.CandidateRank > newCandidate.CandidateRank)
                {
                    break;
                }

                newCandidateIndex++;
            }

            return newCandidateIndex;
        }
    }
}