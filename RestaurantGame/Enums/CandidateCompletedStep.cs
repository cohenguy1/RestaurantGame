using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public enum CandidateCompletedStep
    {
        Initial,
        BlinkRemaimingCandidates,
        RearrangeCandidatesMap,
        FillNextPosition
    }
}