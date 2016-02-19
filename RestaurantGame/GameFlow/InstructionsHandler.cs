using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        public const int NumOfInstructions = 21;

        protected void btnPrevInstruction_Click(object sender, EventArgs e)
        {
            ProgressBar1.Value = GetTrainingProgress(MultiviewInstructions.ActiveViewIndex - 1);

            MultiviewInstructions.ActiveViewIndex--;

            btnNextInstruction.Text = "Next";

            if (MultiviewInstructions.ActiveViewIndex == 0)
            {
                btnPrevInstruction.Enabled = false;
            }
        }

        protected void btnNextInstruction_Click(object sender, EventArgs e)
        {
            ProgressBar1.Value = GetTrainingProgress(MultiviewInstructions.ActiveViewIndex + 1);

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

        public int GetTrainingProgress(int currentInstructionPage)
        {
            double progress = (currentInstructionPage + 1) / (double)NumOfInstructions * 100;
            return (int)progress;
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