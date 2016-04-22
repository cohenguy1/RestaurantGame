using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                //MultiviewInstructions.ActiveViewIndex = 0;
                Session["CurrentInstruction"] = 0;

                ProgressBar1.Value = 0;

                InstructionsStopwatch = new Stopwatch();
                InstructionsStopwatch.Start();

                dbHandler.UpdateTimesTable(GameState.Instructions);
            }
        }

        public const int NumOfInstructions = 21;

        protected void btnPrevInstruction_Click(object sender, EventArgs e)
        {
            ProgressBar1.Value = GetTrainingProgress((int)Session["CurrentInstruction"] - 1);

            Session["CurrentInstruction"] = (int)Session["CurrentInstruction"] - 1;

            InstructionImage1.ImageUrl = "~/Images/Instructions" + Session["CurrentInstruction"] + ".png";

            if ((int)Session["CurrentInstruction"] != NumOfInstructions - 1)
            {
                btnNextInstruction.Text = "Next";
            }

            if ((int)Session["CurrentInstruction"] == 0)
            {
                btnPrevInstruction.Enabled = false;
            }
        }

        protected void btnNextInstruction_Click(object sender, EventArgs e)
        {
            ProgressBar1.Value = GetTrainingProgress((int)Session["CurrentInstruction"] + 1);

            if ((int)Session["CurrentInstruction"] == NumOfInstructions - 1)
            {
                // training start
                InstructionsStopwatch.Stop();
                dbHandler.UpdateTimesTable(GameState.TrainingStart);

                Response.Redirect("ProceedToTraining.aspx");

                return;
            }

            Session["CurrentInstruction"] = (int)Session["CurrentInstruction"] + 1;
            InstructionImage1.ImageUrl = "~/Images/Instructions" + Session["CurrentInstruction"] + ".png";
            btnPrevInstruction.Enabled = true;

            if ((int)Session["CurrentInstruction"] == NumOfInstructions - 1)
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