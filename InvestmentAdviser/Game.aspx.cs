using InvestmentAdviser.Enums;
using InvestmentAdviser.Logic;
using System;
using System.Linq;
using System.Text;

namespace InvestmentAdviser
{
    public partial class Game : System.Web.UI.Page
    {
        public const int StartTimerInterval = 2500;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimerGame.Interval = StartTimerInterval;
                TimerGame.Enabled = false;

                AlreadyAskedForRating = false;
                AskForRating = false;

                MultiView2.ActiveViewIndex = 0;

                ClearTurnTable();

                ImageInterview.Visible = true;
                LabelInterviewing.Visible = true;

                CurrentTurnNumber = 0;

                StartInterviewsForPosition(0);
            }
        }

        protected void btnNextToQuiz_Click(object sender, EventArgs e)
        {
            Response.Redirect("Quiz.aspx");
        }

        private void StartInterviewsForPosition(int position)
        {
            TimerGame.Enabled = false;

            StatusLabel.Text = "";

            SetTitle();

            CleanCurrentScenarioTurn();

            CurrentTurnStatus = TurnStatus.Initial;

            TimerGame.Enabled = true;
        }


        private void SetTitle()
        {
            string turnTitle = GetCurrentJobTitle();
            var currentTurnNumber = CurrentTurnNumber;

            PositionHeader.Text = turnTitle;

            if (currentTurnNumber >= 0)
            {
                MovingToNextPositionLabel.Text = "Moving on to the next turn:" + "<br />" + "<br />";
                MovingToNextPositionLabel.Visible = true;

                MovingJobTitleLabel.Text = turnTitle + "<br />" + "<br />" + "<br />";
                MovingJobTitleLabel.Visible = true;

                if (currentTurnNumber > 0)
                {
                    SetSeenTableRowStyle(currentTurnNumber - 1);
                }
            }

            SetTableRowStyle(currentTurnNumber);
        }

        protected void TimerGame_Tick(object sender, EventArgs e)
        {
            if (AskForRating)
            {
                RateAdvisor();

                AskForRating = false;
                AlreadyAskedForRating = true;

                return;
            }

            if (NewCandidateAwaits())
            {
                if (CurrentTurnStatus == TurnStatus.Initial)
                {
                    CurrentTurnStatus = TurnStatus.Processing;
                    MovingToNextPositionLabel.Visible = false;
                    MovingJobTitleLabel.Visible = false;
                }
                else if (CurrentTurnStatus == TurnStatus.Processing)
                {
                    ProcessCandidate();
                }
            }
            else
            {
                if (CurrentTurnNumber >= 9)
                {
                    TimerGame.Enabled = false;
                    Response.Redirect("EndGame.aspx");
                }
            }
        }

        private void FillNextPosition()
        {
            IncreaseCurrentPosition();

            StartInterviewsForPosition(CurrentTurnNumber);
        }

        private void CleanCurrentScenarioTurn()
        {
            TimerGame.Enabled = false;

            ClearInterviewImages();
        }

        private void ProcessCandidate()
        {
            if (!TimerGame.Enabled)
            {
                TimerGame.Interval = 1000;
                TimerGame.Enabled = true;
            }

            UpdatePositionToAcceptedCandidate();
            TurnSummary();
        }

        private void TurnSummary()
        {
            TimerGame.Enabled = false;
            CurrentTurnStatus = TurnStatus.Initial;
            ImageInterview.Visible = false;
            LabelInterviewing.Visible = false;
            TurnSummaryLbl1.Visible = true;
            TurnSummaryLbl2.Visible = true;
            TurnSummaryLbl3.Visible = true;
            PrizePointsLbl1.Visible = true;
            PrizePointsLbl2.Visible = true;
            PrizePointsLbl3.Visible = true;
            SummaryNextLbl.Visible = true;
            btnNextToUniform.Visible = true;

            TurnSummaryLbl2.Text = GetCurrentTurn().Gain.ToString();
            PrizePointsLbl2.Text = (110 - 1 * 10).ToString();
            SummaryNextLbl.Text = "<br /><br />Press 'Next' to proceed to the next turn.<br />";
        }

        private void PickUniform()
        {
            if (NeedToAskRating())
            {
                AskForRating = true;
            }

            MultiView2.ActiveViewIndex = 2;
            ShowUniforms();

            CurrentTurnStatus = TurnStatus.MoveToNextTurn;
        }

        private void ShowUniforms()
        {
            var jobTitle = GetCurrentJobTitle();

            if (jobTitle.StartsWith("ScenarioTurn"))
            {
                jobTitle = jobTitle.Remove(jobTitle.Length - 2);
            }

            UniformPickForPosition.Text = " Pick the uniform for position " + jobTitle + ":";

            Uniform1.ImageUrl = "~/Images/" + jobTitle + ".Uniform1.jpg";
            Uniform2.ImageUrl = "~/Images/" + jobTitle + ".Uniform2.jpg";
            Uniform3.ImageUrl = "~/Images/" + jobTitle + ".Uniform3.jpg";
        }

        protected void btnPickUniform_Click(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 0;

            if (CurrentTurnNumber < 9)
            {
                FillNextPosition();
            }
            else
            {
                TimerGame.Enabled = true;
            }
        }

        private bool NewCandidateAwaits()
        {
            return true;
        }

        private void UpdatePositionToAcceptedCandidate()
        {
            var currentTurn = GetCurrentTurn();

            int totalPrizePoints = Common.GetTotalPrizePoints(ScenarioTurns);
            UpdateTurnsTable(currentTurn, totalPrizePoints);
        }

        protected void btnNextToUniform_Click(object sender, EventArgs e)
        {
            TurnSummaryLbl1.Visible = false;
            TurnSummaryLbl2.Visible = false;
            TurnSummaryLbl3.Visible = false;
            PrizePointsLbl1.Visible = false;
            PrizePointsLbl2.Visible = false;
            PrizePointsLbl3.Visible = false;
            SummaryNextLbl.Visible = false;
            btnNextToUniform.Visible = false;
            PickUniform();
        }
    }
}