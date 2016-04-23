using System;
using RestaurantGame.Enums;

namespace RestaurantGame
{
    public partial class ProceedToTraining : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnNextToTraining_Click(object sender, EventArgs e)
        {
            GameMode = GameMode.Training;

            Response.Redirect("Game.aspx");
        }
    }
}