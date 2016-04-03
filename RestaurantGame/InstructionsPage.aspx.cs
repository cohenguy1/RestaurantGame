using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class InstructionsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiviewInstructions.ActiveViewIndex = 0;
                ProgressBar1.Value = 0;
            }
        }

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

            if (MultiviewInstructions.ActiveViewIndex == NumOfInstructions - 1)
            {
                // training start
                InstructionsStopwatch.Stop();
                dbHandler.UpdateTimesTable(GameState.TrainingStart);

                Response.Redirect("ProceedToTraining.aspx");

                return;
            }

            MultiviewInstructions.ActiveViewIndex++;
            btnPrevInstruction.Enabled = true;

            if (MultiviewInstructions.ActiveViewIndex == NumOfInstructions - 1)
            {
                btnNextInstruction.Text = "Continue to Training";
            }
        }

        public int GetTrainingProgress(int currentInstructionPage)
        {
            double progress = (currentInstructionPage + 1) / (double)NumOfInstructions * 100;
            return (int)progress;
        }
    }
}