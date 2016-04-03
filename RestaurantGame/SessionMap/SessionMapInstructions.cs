using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace RestaurantGame
{
    public partial class InstructionsPage : System.Web.UI.Page
    {
        public PlayPauseState GamePlayPauseState
        {
            get { return (PlayPauseState)Session[SessionMap.GamePlayPauseStateStr]; }
            set { Session[SessionMap.GamePlayPauseStateStr] = value; }
        }

        public GameMode GameMode
        {
            get { return (GameMode)Session[SessionMap.GameModeStr]; }
            set { Session[SessionMap.GameModeStr] = value; }
        }

        public string UserId
        {
            get { var userId = Session[SessionMap.UserIdStr] == null ? string.Empty : (string)Session[SessionMap.UserIdStr]; return userId; }
            set { Session[SessionMap.UserIdStr] = value; }
        }

        public bool AlreadyAskedForRating
        {
            get { return (bool)Session[SessionMap.AlreadyAskedForRatingStr]; }
            set { Session[SessionMap.AlreadyAskedForRatingStr] = value; }
        }

        public int TimerInterval
        {
            get { return (int)Session[SessionMap.TimerIntervalStr]; }
            set { Session[SessionMap.TimerIntervalStr] = value; }
        }

        public bool TimerEnabled
        {
            get { return (bool)Session[SessionMap.TimerEnabledStr]; }
            set { Session[SessionMap.TimerEnabledStr] = value; }
        }

        public bool AskForRating
        {
            get { return (bool)Session[SessionMap.AskForRatingStr]; }
            set { Session[SessionMap.AskForRatingStr] = value; }
        }

        public int CurrentPositionNumber
        {
            get { return (int)Session[SessionMap.CurrentPositionNumberStr]; }
            set { Session[SessionMap.CurrentPositionNumberStr] = value; }
        }

        public List<Position> Positions
        {
            get { return (List<Position>)Session[SessionMap.PositionsStr]; }
            set { Session[SessionMap.PositionsStr] = value; }
        }

        public int CurrentCandidateNumber
        {
            get { return (int)Session[SessionMap.CurrentCandidateNumberStr]; }
            set { Session[SessionMap.CurrentCandidateNumberStr] = value; }
        }

        public int[] AcceptedCandidates
        {
            get { return (int[])Session[SessionMap.AcceptedCandidatesStr]; }
            set { Session[SessionMap.AcceptedCandidatesStr] = value; }
        }

        public CandidateCompletedStep CandidateCompletedStep
        {
            get { return (CandidateCompletedStep)Session[SessionMap.CandidateCompletedStepStr]; }
            set { Session[SessionMap.CandidateCompletedStepStr] = value; }
        }

        public List<Candidate> PositionCandidates
        {
            get { return (List<Candidate>)Session[SessionMap.PositionCandidiatesStr]; }
            set { Session[SessionMap.PositionCandidiatesStr] = value; }
        }

        public Candidate CurrentCandidate
        {
            get { return (Candidate)Session[SessionMap.CurrentCandidateStr]; }
            set { Session[SessionMap.CurrentCandidateStr] = value; }
        }

        public List<Candidate> CandidatesByNow
        {
            get { return (List<Candidate>)Session[SessionMap.CandidatesByNowStr]; }
            set { Session[SessionMap.CandidatesByNowStr] = value; }
        }

        public int TrainingPassed
        {
            get { return (int)Session[SessionMap.TrainingPassedStr]; }
            set { Session[SessionMap.TrainingPassedStr] = value; }
        }

        public SessionState SessionState
        {
            get
            {
                if (Session[SessionMap.SessionStateStr] == null)
                {
                    return SessionState.Running;
                }
                else
                {
                    return (SessionState)Session[SessionMap.SessionStateStr];
                }
            }
            set { Session[SessionMap.SessionStateStr] = value; }
        }

        public BlinkState RemainingBlinkState
        {
            get { return (BlinkState)Session[SessionMap.RemainingBlinkStateStr]; }
            set { Session[SessionMap.RemainingBlinkStateStr] = value; }
        }

        public int NumOfBlinks
        {
            get { return (int)Session[SessionMap.NumOfBlinksStr]; }
            set { Session[SessionMap.NumOfBlinksStr] = value; }
        }

        public AskPositionHeuristic AskPosition
        {
            get { return (AskPositionHeuristic)Session[SessionMap.AskPositionStr]; }
            set { Session[SessionMap.AskPositionStr] = value; }
        }

        public int NumOfWrongQuizAnswers
        {
            get { return (int)Session[SessionMap.NumOfWrongQuizAnswersStr]; }
            set { Session[SessionMap.NumOfWrongQuizAnswersStr] = value; }
        }

        public int RandomHuristicAskPosition
        {
            get { return (int)Session[SessionMap.RandomHuristicAskPositionStr]; }
            set { Session[SessionMap.RandomHuristicAskPositionStr] = value; }
        }

        public int? VectorNum
        {
            get { return (int?)Session[SessionMap.VectorNumStr]; }
            set { Session[SessionMap.VectorNumStr] = value; }
        }

        public Stopwatch InstructionsStopwatch
        {
            get { return (Stopwatch)Session[SessionMap.InstructionsStopwatchStr]; }
            set { Session[SessionMap.InstructionsStopwatchStr] = value; }
        }

        public Stopwatch GameStopwatch
        {
            get { return (Stopwatch)Session[SessionMap.GameStopwatchStr]; }
            set { Session[SessionMap.GameStopwatchStr] = value; }
        }

        public Stopwatch GameStateStopwatch
        {
            get { return (Stopwatch)Session[SessionMap.GameStateStopwatchStr]; }
            set { Session[SessionMap.GameStateStopwatchStr] = value; }
        }

        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }
    }
}