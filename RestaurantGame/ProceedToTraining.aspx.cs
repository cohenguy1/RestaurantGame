using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            

            Response.Redirect("Default.aspx");
        }
    }
}