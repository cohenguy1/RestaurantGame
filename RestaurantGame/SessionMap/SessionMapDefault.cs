using RestaurantGame.Enums;
using RestaurantGame.Logic;
using System.Collections.Generic;
using System.Diagnostics;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        public PlayPauseState GamePlayPauseState
        {
            get { return (PlayPauseState)Session[SessionMap.GamePlayPauseStateStr]; }
            set { Session[SessionMap.GamePlayPauseStateStr] = value; }
        }

        public GameMode GameMode
        {
            get
            {
                if (Session[SessionMap.GameModeStr] == null)
                {
                    return GameMode.Initial;
                }
                else
                {
                    return (GameMode)Session[SessionMap.GameModeStr];
                }
            }
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

        public bool AskForRating
        {
            get { return (bool)Session[SessionMap.AskForRatingStr]; }
            set { Session[SessionMap.AskForRatingStr] = value; }
        }

        public List<Position> Positions
        {
            get { return (List<Position>)Session[SessionMap.PositionsStr]; }
            set { Session[SessionMap.PositionsStr] = value; }
        }

        public int[] AcceptedCandidates
        {
            get { return (int[])Session[SessionMap.AcceptedCandidatesStr]; }
            set { Session[SessionMap.AcceptedCandidatesStr] = value; }
        }

        public AskPositionHeuristic AskPosition
        {
            get { return (AskPositionHeuristic)Session[SessionMap.AskPositionStr]; }
            set { Session[SessionMap.AskPositionStr] = value; }
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