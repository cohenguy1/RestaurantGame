using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

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