﻿using RestaurantGame.Enums;
using RestaurantGame.Logic;
using System;
using System.Linq;
using System.Text;

namespace RestaurantGame
{
    public partial class Game : System.Web.UI.Page
    {
        // TODO modify instructions ---
        // TODO think about consequences ---
        // change to 10 candidates & update probabilities

        public const int StartTimerInterval = 2500;
        public const int NumberOfCandidates = DecisionMaker.NumberOfCandidates;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimerGame.Interval = StartTimerInterval;
                TimerGame.Enabled = false;

                AlreadyAskedForRating = false;
                AskForRating = false;

                MultiView2.ActiveViewIndex = 0;

                ClearPositionsTable();

                ImageInterview.Visible = true;
                LabelInterviewing.Visible = true;

                CurrentPositionNumber = 0;

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

            CurrentCandidate = null;

            StatusLabel.Text = "";

            SetTitle();

            CleanCurrentPosition();

            GenerateCandidatesForPosition();

            CurrentPositionStatus = PositionStatus.Initial;

            TimerGame.Enabled = true;
        }


        private void SetTitle()
        {
            string jobTitle = GetCurrentJobTitle();
            var currentPositionNumber = CurrentPositionNumber;

            PositionHeader.Text = "Position: " + jobTitle;

            if (currentPositionNumber >= 0)
            {
                MovingToNextPositionLabel.Text = "Moving on to fill the next position:" + "<br />" + "<br />";
                MovingToNextPositionLabel.Visible = true;

                MovingJobTitleLabel.Text = jobTitle + "<br />" + "<br />" + "<br />";
                MovingJobTitleLabel.Visible = true;

                if (currentPositionNumber > 0)
                {
                    var prevPositionCell = GetPositionCell(currentPositionNumber - 1);
                    prevPositionCell.ForeColor = System.Drawing.Color.Blue;
                    prevPositionCell.Font.Bold = false;
                    prevPositionCell.Font.Italic = true;
                }
            }

            var positionCell = GetCurrentPositionCell();
            positionCell.ForeColor = System.Drawing.Color.Green;
            positionCell.Font.Bold = true;
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
                if (CurrentPositionStatus == PositionStatus.Initial)
                {
                    UpdateImages(CandidateState.Interview);
                    CurrentPositionStatus = PositionStatus.Interviewing;
                }
                else if (CurrentPositionStatus == PositionStatus.Interviewing)
                {
                    ProcessCandidate();
                }
            }
            else
            {
                if (CurrentPositionNumber >= 9)
                {
                    TimerGame.Enabled = false;
                    Response.Redirect("EndGame.aspx");
                }
            }
        }

        private void FillNextPosition()
        {
            IncreaseCurrentPosition();

            StartInterviewsForPosition(CurrentPositionNumber);
        }

        private void CleanCurrentPosition()
        {
            TimerGame.Enabled = false;

            ClearInterviewImages();
        }

        private void ProcessCandidate()
        {
            var dm = DecisionMaker.GetInstance();
            Candidate hiredWorker = dm.ProcessNextPosition(PositionCandidates);
            hiredWorker.CandidateState = CandidateState.Completed;

            if (!TimerGame.Enabled)
            {
                TimerGame.Interval = 1000;
                TimerGame.Enabled = true;
            }

            CurrentCandidate = hiredWorker;
            UpdatePositionToAcceptedCandidate(hiredWorker);
            PositionSummary();
        }

        private void PositionSummary()
        {
            TimerGame.Enabled = false;

            if (NeedToAskRating())
            {
                dbHandler.UpdateTimesTable(GameState.BeforeRate);
            }

            CurrentPositionStatus = PositionStatus.Initial;
            ImageInterview.Visible = false;
            LabelInterviewing.Visible = false;
            ImageHired.Visible = true;
            PositionSummaryLbl1.Visible = true;
            PositionSummaryLbl2.Visible = true;
            PositionSummaryLbl3.Visible = true;
            BonusLbl1.Visible = true;
            BonusLbl2.Visible = true;
            BonusLbl3.Visible = true;
            SummaryNextLbl.Visible = true;
            btnNextToUniform.Visible = true;

            PositionSummaryLbl2.Text = CurrentCandidate.CandidateRank.ToString();
            BonusLbl2.Text = Math.Round((11 - CurrentCandidate.CandidateRank) * 0.2, 2).ToString() + "\u00A2";
            SummaryNextLbl.Text = "<br /><br />Press 'Next' to pick uniform for the " + GetCurrentJobTitle() + ".<br />";
        }

        private void PickUniform()
        {
            if (NeedToAskRating())
            {
                AskForRating = true;
            }

            MultiView2.ActiveViewIndex = 2;
            ShowUniforms();

            CurrentPositionStatus = PositionStatus.FillNextPosition;
        }

        private void ShowUniforms()
        {
            var jobTitle = GetCurrentJobTitle();

            if (jobTitle.StartsWith("Waiter"))
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

            if (CurrentPositionNumber < 9)
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
            if (CurrentCandidate == null)
            {
                return true;
            }

            if (CurrentCandidate.CandidateAccepted)
            {
                // finished candidate
                return false;
            }

            return true;
        }

        private void UpdatePositionToAcceptedCandidate(Candidate candidate)
        {
            var currentPosition = GetCurrentPosition();

            currentPosition.ChosenCandidate = candidate;

            AcceptedCandidates[CurrentPositionNumber] = currentPosition.ChosenCandidate.CandidateRank;

            double totalBonus = Common.GetTotalBonus(Positions);
            UpdatePositionsTable(currentPosition, totalBonus);
        }

        protected void btnNextToUniform_Click(object sender, EventArgs e)
        {
            ImageHired.Visible = false;
            PositionSummaryLbl1.Visible = false;
            PositionSummaryLbl2.Visible = false;
            PositionSummaryLbl3.Visible = false;
            BonusLbl1.Visible = false;
            BonusLbl2.Visible = false;
            BonusLbl3.Visible = false;
            SummaryNextLbl.Visible = false;
            btnNextToUniform.Visible = false;
            PickUniform();
        }
    }
}