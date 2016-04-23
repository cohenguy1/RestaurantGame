using RestaurantGame.Enums;
using RestaurantGame.Logic;
using System.Collections.Generic;
using System.Diagnostics;

namespace RestaurantGame
{
    public partial class EndGame : System.Web.UI.Page
    {
        
        public string UserId
        {
            get { var userId = Session[SessionMap.UserIdStr] == null ? string.Empty : (string)Session[SessionMap.UserIdStr]; return userId; }
            set { Session[SessionMap.UserIdStr] = value; }
        }
        
        public List<Position> Positions
        {
            get { return (List<Position>)Session[SessionMap.PositionsStr]; }
            set { Session[SessionMap.PositionsStr] = value; }
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

        
        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }

        public List<Candidate> PositionCandidates
        {
            get { return (List<Candidate>)Session[SessionMap.PositionCandidiatesStr]; }
            set { Session[SessionMap.PositionCandidiatesStr] = value; }
        }
    }
}