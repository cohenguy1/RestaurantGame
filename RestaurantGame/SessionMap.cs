using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
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

        public const string GameStateStr = "GameState";
        public const string GameModeStr = "GameMode";
        public const string TrainingStepStr = "TrainingStep";

        public const string CandidateCompletedStepStr = "CandidateCompletedStep";

        public const string TrainingPassedStr = "TrainingPassed";

        public const string SessionStateStr = "SessionState";

        public const string RemainingBlinkStateStr = "RemainingBlinkState";
        public const string NumOfBlinksStr = "NumOfBlinks";

        public const string AskPositionStr = "AskPosition";

        public const string NumOfWrongQuizAnswersStr = "NumOfWrongQuizAnswers";

        public GameState GameState
        {
            get { return (GameState)Session[GameStateStr]; }
            set { Session[GameStateStr] = value; }
        }

        public GameMode GameMode
        {
            get { return (GameMode)Session[GameModeStr]; }
            set { Session[GameModeStr] = value; }
        }

        public string UserId
        {
            get { var userId = Session[UserIdStr] == null ? string.Empty : (string)Session[UserIdStr]; return userId; }
            set { Session[UserIdStr] = value; }
        }

        public bool AlreadyAskedForRating
        {
            get { return (bool)Session[AlreadyAskedForRatingStr]; }
            set { Session[AlreadyAskedForRatingStr] = value; }
        }

        public int TimerInterval
        {
            get { return (int)Session[TimerIntervalStr]; }
            set { Session[TimerIntervalStr] = value; }
        }

        public bool TimerEnabled
        {
            get { return (bool)Session[TimerEnabledStr]; }
            set { Session[TimerEnabledStr] = value; }
        }

        public bool AskForRating
        {
            get { return (bool)Session[AskForRatingStr]; }
            set { Session[AskForRatingStr] = value; }
        }

        public int CurrentPositionNumber
        {
            get { return (int)Session[CurrentPositionNumberStr]; }
            set { Session[CurrentPositionNumberStr] = value; }
        }

        public List<Position> Positions
        {
            get { return (List<Position>)Session[PositionsStr]; }
            set { Session[PositionsStr] = value; }
        }

        public int CurrentCandidateNumber
        {
            get { return (int)Session[CurrentCandidateNumberStr]; }
            set { Session[CurrentCandidateNumberStr] = value; }
        }

        public int[] AcceptedCandidates
        {
            get { return (int[])Session[AcceptedCandidatesStr]; }
            set { Session[AcceptedCandidatesStr] = value; }
        }

        public CandidateCompletedStep CandidateCompletedStep
        {
            get { return (CandidateCompletedStep)Session[CandidateCompletedStepStr]; }
            set { Session[CandidateCompletedStepStr] = value; }
        }

        public List<Candidate> PositionCandidates
        {
            get { return (List<Candidate>)Session[PositionCandidiatesStr]; }
            set { Session[PositionCandidiatesStr] = value; }
        }

        public Candidate CurrentCandidate
        {
            get { return (Candidate)Session[CurrentCandidateStr]; }
            set { Session[CurrentCandidateStr] = value; }
        }

        public List<Candidate> CandidatesByNow
        {
            get { return (List<Candidate>)Session[CandidatesByNowStr]; }
            set { Session[CandidatesByNowStr] = value; }
        }

        public int TrainingPassed
        {
            get { return (int)Session[TrainingPassedStr]; }
            set { Session[TrainingPassedStr] = value; }
        }

        public SessionState SessionState
        {
            get
            {
                if (Session[SessionStateStr] == null)
                {
                    return SessionState.Running;
                }
                else
                {
                    return (SessionState)Session[SessionStateStr];
                }
            }
            set { Session[SessionStateStr] = value; }
        }

        public BlinkState RemainingBlinkState
        {
            get { return (BlinkState)Session[RemainingBlinkStateStr]; }
            set { Session[RemainingBlinkStateStr] = value; }
        }

        public int NumOfBlinks
        {
            get { return (int)Session[NumOfBlinksStr]; }
            set { Session[NumOfBlinksStr] = value; }
        }

        public AskPositionHeuristic AskPosition
        {
            get { return (AskPositionHeuristic)Session[AskPositionStr]; }
            set { Session[AskPositionStr] = value; }
        }

        public int NumOfWrongQuizAnswers
        {
            get { return (int)Session[NumOfWrongQuizAnswersStr]; }
            set { Session[NumOfWrongQuizAnswersStr] = value; }
        }
    }
}