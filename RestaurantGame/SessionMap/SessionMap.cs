using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace RestaurantGame
{
    public static class SessionMap
    {
        public const string UserIdStr = "user_id";

        public const string CurrentCandidateStr = "CurrentCandidate";
        public const string PositionsStr = "Positions";
        public const string CurrentPositionNumberStr = "CurrentPositionNumber";

        public const string PositionCandidiatesStr = "PositionCandidates";
        public const string CurrentCandidateNumberStr = "CurrentCandidateNumber";

        public const string CandidatesByNowStr = "CandidatesByNow";

        public const string AskForRatingStr = "AskForRating";
        public const string AlreadyAskedForRatingStr = "AlreadyAskedForRating";

        public const string AcceptedCandidatesStr = "AcceptedCandidates";

        public const string TimerIntervalStr = "TimerInterval";
        public const string TimerEnabledStr = "TimerEnabled";

        public const string GamePlayPauseStateStr = "GamePlayPauseState";
        public const string GameModeStr = "GameMode";
        public const string TrainingStepStr = "TrainingStep";

        public const string CandidateCompletedStepStr = "CandidateCompletedStep";

        public const string TrainingPassedStr = "TrainingPassed";

        public const string SessionStateStr = "SessionState";

        public const string RemainingBlinkStateStr = "RemainingBlinkState";
        public const string NumOfBlinksStr = "NumOfBlinks";

        public const string AskPositionStr = "AskPosition";

        public const string NumOfWrongQuizAnswersStr = "NumOfWrongQuizAnswers";

        public const string RandomHuristicAskPositionStr = "RandomHuristicAskPosition";
        public const string VectorNumStr = "VectorNum";

        public const string InstructionsStopwatchStr = "InstructionsStopwatch";
        public const string GameStopwatchStr = "GameStopwatch";
        public const string GameStateStopwatchStr = "GameStateStopwatch";

        public const string DbHandlerStr = "DbHandlerStr";
    }
}