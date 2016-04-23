using RestaurantGame.Enums;

namespace RestaurantGame
{
    public partial class ProceedToTraining : System.Web.UI.Page
    {
        public GameMode GameMode
        {
            get { return (GameMode)Session[SessionMap.GameModeStr]; }
            set { Session[SessionMap.GameModeStr] = value; }
        }
    }
}