﻿using RestaurantGame.Enums;

namespace RestaurantGame
{
    public partial class ProceedToGame : System.Web.UI.Page
    {
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
        
        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }
    }
}