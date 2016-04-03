using RestaurantGame.Enums;
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

            if (MultiviewInstructions.ActiveViewIndex == NumOfInstructions - 1)
            {
                // training start
                InstructionsStopwatch.Stop();
                MultiView1.ActiveViewIndex = 3;
                dbHandler.UpdateTimesTable(GameState.TrainingStart);

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

        protected void btnNextToQuiz_Click(object sender, EventArgs e)
        {
            if (trainingRBL.SelectedIndex == 0)
            {
                // continue training
                MultiView2.ActiveViewIndex = 3;
                ShowUniforms();
            }
            else
            {
                // show quiz
                dbHandler.UpdateTimesTable(GameState.Quiz);

                MultiView1.ActiveViewIndex = 4;
                NumOfWrongQuizAnswers = 0;
            }
        }

    }
}