using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        protected void btnNext2_Click(object sender, EventArgs e)
        {
            if (rbl1.SelectedIndex == 0 && rbl2.SelectedIndex == 3 && rbl3.SelectedIndex == 3)
            {
                MultiView1.ActiveViewIndex = 5;

                GameMode = GameMode.Advisor;

                MultiView2.ActiveViewIndex = 0;

                ClearCandidateImages();

                ImageInterview.Visible = false;
                ImageManForward.Visible = true;

                SetCurrentPositionNumber(0);

                ClearPositionsTable();

                StartInterviewsForPosition(0);
            }
            else
            {
                Alert.Show("wrong answer, please try again");
            }
        }
    }
}