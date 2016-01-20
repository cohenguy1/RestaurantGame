﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    // TODO: Add uniform
    // TODO: random position - you are the uniform picker
    // TODO: Thanks for participating

    public partial class Default : System.Web.UI.Page
    {
        public const int PositionCandidatesNumber = DecisionMaker.PositionCandidatesNumber;

        public const string PositionsStr = "Positions";
        public const string PositionToFillStr = "PositionToFill";

        public const string PositionCandidiatesStr = "PositionCandidates";
        public const string CurrentCandidateNumberStr = "CurrentCandidateNumber";

        public const string CandidatesByNowStr = "CandidatesByNow";

        public const string AskForRating = "AskForRatingStr";

        public const string AcceptedCandidates = "AcceptedCandidates";

        public const string TimerInterval = "TimerInterval";
        public const string TimerEnabled = "TimerEnabled";

        public const string AlreadyAskedForRating = "AlreadyAskedForRating";

        public const string GameStateStr = "GameState";
        public const string GameModeStr = "GameMode";
        public const string TrainingStepStr = "TrainingStep";

        public const string CandidateCompletedStepStr = "CandidateCompletedStep";

        public const string TrainingPassed = "TrainingPassed";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var t = new DecisionMaker();

                String val = null;

                // friend assigment
                val = Request.QueryString["assignmentId"];
                if (val == null)
                {
                    Session["user_id"] = "friend";
                    Session["turkAss"] = "turkAss";
                    Session["hitId"] = "hit id friend";
                    btnNext0.Enabled = true;
                }

                Timer1.Enabled = false;
                Timer1.Interval = StartTimerInterval;

                Session[AlreadyAskedForRating] = false;

                Session[TimerInterval] = StartTimerInterval;
                Session[TimerEnabled] = true;

                Session[GameStateStr] = GameState.Playing;

                GeneratePositions();

                Session[CurrentCandidateNumberStr] = 0;

                Session[AskForRating] = false;
            }
        }

        private void StartInterviewsForPosition(int position)
        {
            Timer1.Enabled = false;

            Session["Position"] = null;
            SetCurrentPositionNumber(position);

            StatusLabel.Text = "";

            SetTitle();

            ShowAllRemainingCandidatesImages();
            ImageHired.Visible = false;

            if ((GameMode)Session[GameModeStr] == GameMode.Adviser)
            {
                btnThumbsDown.Visible = false;
                btnThumbsUp.Visible = false;
            }

            GenerateCandidatesForPosition();
            GenerateCandidatesByNow();

            Session[CurrentCandidateNumberStr] = 0;
            Session[CandidateCompletedStepStr] = CandidateCompletedStep.ShowCandidatesMap;

            Timer1.Interval = 4000;

            Timer1.Enabled = true;
        }

        private void SetTitle()
        {
            string jobTitle = GetCurrentJobTitle();
            var currentPositionNumber = GetCurrentPositionNumber();

            PositionHeader.Text = "Position: " + jobTitle;

            if (currentPositionNumber > 0)
            {
                MovingToNextPositionLabel.Text = "Moving on to fill the next position:" + "<br />" + "<br />";
                MovingToNextPositionLabel.Visible = true;

                MovingJobTitleLabel.Text = jobTitle + "<br />" + "<br />" + "<br />";
                MovingJobTitleLabel.Visible = true;

                var prevPositionCell = GetPositionCell(currentPositionNumber - 1);
                prevPositionCell.ForeColor = System.Drawing.Color.Blue;
                prevPositionCell.Font.Bold = false;
                prevPositionCell.Font.Italic = true;
            }

            var positionCell = GetCurrentPositionCell();
            positionCell.ForeColor = System.Drawing.Color.Green;
            positionCell.Font.Bold = true;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Interval = (int)Session[TimerInterval];

            bool askForRating = (bool)Session[AskForRating];
            if (askForRating)
            {
                RateAdviser();

                Session[AskForRating] = false;

                return;
            }

            if (NewCandidateAwaits())
            {
                ProcessCandidate();
            }
            else
            {
                FillNextPosition();
            }
        }

        private void FillNextPosition()
        {
            Timer1.Enabled = false;

            IncreaseCurrentPosition();

            ClearCandidateImages();
            ClearInterviewImages();

            var currentPositionNumber = GetCurrentPositionNumber();

            if (currentPositionNumber < 10)
            {
                StartInterviewsForPosition(currentPositionNumber);
            }
            else if ((GameMode)Session[GameModeStr] == GameMode.Training)
            {
                // wrap around
                StartInterviewsForPosition(0);
            }
        }

        private void EnterNewCandidate()
        {
            var currentCandidateNumber = (int)Session[CurrentCandidateNumberStr];
            var positionCandidates = (List<Candidate>)Session[PositionCandidiatesStr];
            var currentCandidate = positionCandidates[currentCandidateNumber];

            var lastAvailableCandidate = GetRemainingStickManImage(PositionCandidatesNumber - currentCandidateNumber);

            if (lastAvailableCandidate != null)
            {
                lastAvailableCandidate.ImageUrl = null;
                lastAvailableCandidate.Visible = false;
            }

            Session["Position"] = currentCandidate;

            UpdateImages(CandidateState.New);
            currentCandidate.CandidateState = CandidateState.Interview;
        }

        private void ProcessCandidate()
        {
            var gameMode = (GameMode)Session[GameModeStr];
            var currentCandidate = (Candidate)Session["Position"];

            if (currentCandidate == null)
            {
                EnterNewCandidate();
            }
            else if (currentCandidate.CandidateState == CandidateState.Interview)
            {
                UpdateImages(currentCandidate.CandidateState);
                DetermineCandidateRank(currentCandidate);

                if (gameMode == GameMode.Adviser)
                {
                    currentCandidate.CandidateState = CandidateState.Completed;
                }
                else
                {
                    Timer1.Enabled = false;
                }
            }
            else if (currentCandidate.CandidateState == CandidateState.Completed)
            {
                if (currentCandidate.CandidateAccepted)
                {
                    var candidateCompletedStep = (CandidateCompletedStep)Session[CandidateCompletedStepStr];

                    switch (candidateCompletedStep)
                    {
                        case CandidateCompletedStep.ShowCandidatesMap:
                            UpdatePositionToAcceptedCandidate(currentCandidate);
                            Timer1.Interval = 6000;
                            Session[CandidateCompletedStepStr] = CandidateCompletedStep.PickUniform;
                            break;
                        case CandidateCompletedStep.PickUniform:

                            Timer1.Enabled = false;
                            Session[TrainingPassed] = (int)Session[TrainingPassed] + 1;

                            if (gameMode == GameMode.Training && (int)Session[TrainingPassed] >= 3)
                            {
                                MultiView2.ActiveViewIndex = 2;
                            }
                            else
                            {
                                MultiView2.ActiveViewIndex = 3;
                                ShowUniforms();
                            }
                            
                            Session[CandidateCompletedStepStr] = CandidateCompletedStep.FillNextPosition;
                            break;
                        case CandidateCompletedStep.FillNextPosition:
                            FillNextPosition();

                            if ((bool)Session[AlreadyAskedForRating] == false && gameMode == GameMode.Adviser)
                            {
                                Session[AskForRating] = true;
                                Session[AlreadyAskedForRating] = true;
                            }
                            break;
                    }
                }
                else
                {
                    Session[CurrentCandidateNumberStr] = (int)Session[CurrentCandidateNumberStr] + 1;

                    EnterNewCandidate();
                }
            }
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

            EnableDisableTimer(true);
        }

        private void EnableDisableTimer(bool defaultCommand)
        {
            if (Session[TimerEnabled] != null)
            {
                Timer1.Enabled = (bool)Session[TimerEnabled];
            }
            else
            {
                Timer1.Enabled = defaultCommand;
            }
        }

        private bool CandidateInProcess()
        {
            var currentCandidate = (Candidate)Session["Position"];

            if (currentCandidate == null)
            {
                return false;
            }

            var candidateInProcess = (currentCandidate.CandidateState == CandidateState.Interview) ||
                (currentCandidate.CandidateState == CandidateState.Completed);

            return candidateInProcess;
        }

        private bool NewCandidateAwaits()
        {
            var currentCandidateNumber = (int)Session[CurrentCandidateNumberStr];
            return (currentCandidateNumber < PositionCandidatesNumber);
        }

        private void UpdateImages(CandidateState candidateState)
        {
            ImageManForward.Visible = (candidateState == CandidateState.New);
            ImageInterview.Visible = (candidateState == CandidateState.Interview);
            MovingToNextPositionLabel.Visible = false;
            MovingJobTitleLabel.Visible = false;
        }

        private void DetermineCandidateRank(Candidate newCandidate)
        {
            var candidatesByNow = (List<Candidate>)Session[CandidatesByNowStr];

            int newCandidateIndex = 0;
            foreach (var candidate in candidatesByNow)
            {
                if (candidate.CandidateRank > newCandidate.CandidateRank)
                {
                    break;
                }

                newCandidateIndex++;
            }

            candidatesByNow.Insert(newCandidateIndex, newCandidate);

            var dm = new DecisionMaker();

            var gameMode = (GameMode)Session[GameModeStr];

            if (gameMode == GameMode.Adviser)
            {
                var accepted = dm.Decide(candidatesByNow, newCandidateIndex);

                newCandidate.CandidateAccepted = accepted;
            }

            SetStatusLabel(newCandidateIndex, candidatesByNow.Count);
            DrawCandidatesByNow(candidatesByNow, newCandidateIndex, this);
        }

        private void SetStatusLabel(int newCandidateIndex, int totalCandidatesByNow)
        {
            StatusLabel.Text = "The new candidate has a relative rank of " + (newCandidateIndex + 1) + " out of " + totalCandidatesByNow + ".";
        }

        private void UpdatePositionToAcceptedCandidate(Candidate candidate)
        {
            var positionToFill = (int)Session[PositionToFillStr];

            var currentPosition = GetCurrentPosition();

            currentPosition.ChosenCandidate = candidate;

            ImageHired.Visible = true;
            ImageInterview.Visible = false;

            var acceptedCandidates = (int[])Session[AcceptedCandidates];
            acceptedCandidates[positionToFill] = currentPosition.ChosenCandidate.CandidateRank;
            Session[AcceptedCandidates] = acceptedCandidates;

            double avgRank = CalculateAveragePosition();
            UpdatePositionsTable(currentPosition, avgRank);

            StatusLabel.Text = "It's time to reveal the absolute rankings of the candidates:";
            ShowCandidateMap(currentPosition.ChosenCandidate);
        }

        protected void btnThumbsDown_Click(object sender, EventArgs e)
        {
            AcceptCandidateByUser(false);
        }

        protected void btnThumbsUp_Click(object sender, EventArgs e)
        {
            AcceptCandidateByUser(true);
        }

        private void AcceptCandidateByUser(bool accepted)
        {
            var currentCandidate = (Candidate)Session["Position"];

            if (currentCandidate == null)
            {
                EnterNewCandidate();
            }
            else if (currentCandidate.CandidateState == CandidateState.Interview)
            {
                currentCandidate.CandidateAccepted = accepted;

                currentCandidate.CandidateState = CandidateState.Completed;
            }

            Timer1.Enabled = true;
        }
    }
}