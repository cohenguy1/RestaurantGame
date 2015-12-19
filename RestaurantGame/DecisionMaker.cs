using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public class DecisionMaker
    {
        public const int PositionCandidatesNumber = 20;

        public bool Decide(List<Candidate> candidatesByNow, Candidate newCandidate)
        {
            if (candidatesByNow.Count <= (int)Math.Sqrt(PositionCandidatesNumber))
            {
                return false;
            }

            if (candidatesByNow.Count == PositionCandidatesNumber - 1)
            {
                return true;
            }

            var firstSqrtCandidates = candidatesByNow.Where(candidate => candidate.CandidateNumber < Math.Sqrt(PositionCandidatesNumber));

            var minRank = firstSqrtCandidates.Min(candidate => candidate.CandidateRank);

            return (newCandidate.CandidateRank < minRank);
        }
    }
}