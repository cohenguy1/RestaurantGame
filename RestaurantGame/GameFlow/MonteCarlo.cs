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

        public const double alpha = 0.45;

        public bool ShouldAsk(int[] accepted, int stoppingDecision, Random random)
        {
            return true;
        }

        private int SelectCandidate(List<Candidate> positionCandidates, List<Candidate> candidatesByNow, int positionIndex)
        {
            candidatesByNow.Clear();
            for (int candidateIndex = 0; candidateIndex < Common.NumOfCandidates; candidateIndex++)
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
