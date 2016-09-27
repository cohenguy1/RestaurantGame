using RestaurantGame.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantGame
{
    public partial class Game : System.Web.UI.Page
    {
        public const int NumOfVectors = 1000000;

        public bool ShouldAsk(int[] accepted, int stoppingDecision, Random random)
        {
            if (AlreadyPerformingMonteCarlo)
            {
                return false;
            }

            AlreadyPerformingMonteCarlo = true;

            var positionCandidates = GenerateCandidatesForPosition();
            var candidatesByNow = new List<Candidate>();

            double[] exponentialSmoothing = new double[10];
            double[] exponentialSmoothingAccumulated = new double[10];

            var alpha = 0.45;
            for (var positionIndex = 0; positionIndex <= stoppingDecision; positionIndex++)
            {
                if (positionIndex == 0)
                {
                    exponentialSmoothing[positionIndex] = accepted[0];
                }
                else
                {
                    exponentialSmoothing[positionIndex] = alpha * accepted[positionIndex] + (1 - alpha) * exponentialSmoothing[positionIndex - 1];
                }
            }

            for (var i = 0; i < NumOfVectors; i++)
            {
                for (var positionIndex = stoppingDecision + 1; positionIndex < 10; positionIndex++)
                {
                    InitCandidatesForPosition(positionCandidates, random);

                    accepted[positionIndex] = SelectCandidate(positionCandidates, candidatesByNow, positionIndex);
                }

                for (var positionIndex = stoppingDecision + 1; positionIndex < 10; positionIndex++)
                {
                    exponentialSmoothing[positionIndex] = alpha * accepted[positionIndex] + (1 - alpha) * exponentialSmoothing[positionIndex - 1];
                    exponentialSmoothingAccumulated[positionIndex] += exponentialSmoothing[positionIndex];
                }
            }

            for (var positionIndex = 0; positionIndex <= stoppingDecision; positionIndex++)
            {
                exponentialSmoothingAccumulated[positionIndex] = exponentialSmoothing[positionIndex];
            }

            for (var positionIndex = stoppingDecision + 1; positionIndex < 10; positionIndex++)
            {
                exponentialSmoothingAccumulated[positionIndex] /= NumOfVectors;
            }

            bool foundBetter = false;
            var currentES = exponentialSmoothingAccumulated[stoppingDecision];
            for (var positionIndex = stoppingDecision + 1; positionIndex < 10; positionIndex++)
            {
                if (exponentialSmoothingAccumulated[positionIndex] < currentES)
                {
                    foundBetter = true;
                }
            }

            AlreadyPerformingMonteCarlo = false;

            return !foundBetter;
        }

        private int SelectCandidate(List<Candidate> positionCandidates, List<Candidate> candidatesByNow, int positionIndex)
        {
            candidatesByNow.Clear();
            for (int candidateIndex = 0; candidateIndex < DecisionMaker.NumberOfCandidates; candidateIndex++)
            {
                var currentCandidate = positionCandidates[candidateIndex];
                DecisionMaker.GetInstance().DetermineCandidateRank(candidatesByNow, currentCandidate);

                if (currentCandidate.CandidateAccepted)
                {
                    return currentCandidate.CandidateRank;
                }
            }

            return 0;
        }
    }
}
