using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {

        protected void btnPrevInstruction_Click(object sender, EventArgs e)
        {
            MultiviewInstructions.ActiveViewIndex--;

            if (MultiviewInstructions.ActiveViewIndex == 0)
            {
                btnPrevInstruction.Enabled = false;
            }
        }

        protected void btnNextInstruction_Click(object sender, EventArgs e)
        {
            if (MultiviewInstructions.ActiveViewIndex == 20)
            {
                MultiView1.ActiveViewIndex = 3;

                return;
            }

            MultiviewInstructions.ActiveViewIndex++;
            btnPrevInstruction.Enabled = true;

            if (MultiviewInstructions.ActiveViewIndex == 20)
            {
                btnNextInstruction.Text = "Continue to Training";
            }
        }

        protected void btnTrainingSend_Click(object sender, EventArgs e)
        {
            if (trainingRBL.SelectedIndex == 0)
            {
                MultiView2.ActiveViewIndex = 3;
                ShowUniforms();
            }
            else
            {
                MultiView1.ActiveViewIndex = 4;
            }
        }

    }
}