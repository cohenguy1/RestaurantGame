﻿using RestaurantGame.Enums;
using System;
using System.Text;

namespace RestaurantGame
{
    // TODO: Finish uniform images
    // TODO: Thanks for participating
    // TODO Bonus rephrasing
    // TODO Bug of only one training after speeding in training
    // TODO short blinking
    // TODO configuration of rating position
    // TODO Upload Site

    // Next Generation
    // TODO convert instructions to javascript

    public partial class Default : System.Web.UI.Page
    {
        public const int NumberOfCandidates = DecisionMaker.NumberOfCandidates;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RandomHimHer();

                var t = new DecisionMaker();

                String val = null;

                // friend assigment
                val = Request.QueryString["assignmentId"];
                if (val == null)
                {
                    UserId = "friend";
                    Session["turkAss"] = "turkAss";
                    Session["hitId"] = "hit id friend";
                    btnNextToInfo.Enabled = true;
                }

                TimerGame.Enabled = false;
                TimerGame.Interval = StartTimerInterval;

                AlreadyAskedForRating = false;

                TimerInterval = StartTimerInterval;
                TimerEnabled = true;

                GameState = GameState.Playing;

                GeneratePositions();

                CurrentCandidateNumber = 0;

                AskForRating = false;
            }
        }

        private void RandomHimHer()
        {
            Random rand = new Random();
            int randHim = rand.Next(2);
            if (randHim == 1)
            {
                backgroundText.Text = backgroundText.Text.Replace("him", "her");
            }
        }

        private void StartInterviewsForPosition(int position)
        {
            TimerGame.Enabled = false;

            CurrentCandidate = null;
            SetCurrentPositionNumber(position);

            StatusLabel.Text = "";

            SetTitle();

            ShowAllRemainingCandidatesImages();
            ImageHired.Visible = false;

            GenerateCandidatesForPosition();
            GenerateCandidatesByNow();

            CurrentCandidateNumber = 0;
            CandidateCompletedStep = CandidateCompletedStep.ShowCandidatesMap;

            TimerGame.Interval = 4000;

            TimerGame.Enabled = true;
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

        protected void TimerGame_Tick(object sender, EventArgs e)
        {
            TimerGame.Interval = TimerInterval;

            if (AskForRating)
            {
                RateAdvisor();

                AskForRating = false;

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
            TimerGame.Enabled = false;

            IncreaseCurrentPosition();

            ClearCandidateImages();
            ClearInterviewImages();

            var currentPositionNumber = GetCurrentPositionNumber();

            if (currentPositionNumber < 10)
            {
                StartInterviewsForPosition(currentPositionNumber);
            }
            else 
            {
                if (GameMode == GameMode.Training)
                {
                    // wrap around
                    StartInterviewsForPosition(0);
                }
                else
                {
                    EndGame();
                }
            }
        }

        private void EndGame()
        {
            MultiView1.ActiveViewIndex = 7;
            double averageRank = CalculateAveragePosition();
            int bonus = NumberOfCandidates - (int)Math.Round(averageRank);
            AverageRank.Text = averageRank.ToString("0.00");
            Bonus.Text = bonus.ToString() + " cents";
        }

        private void EnterNewCandidate()
        {
            var currentCandidate = PositionCandidates[CurrentCandidateNumber];

            var lastAvailableCandidate = GetRemainingStickManImage(NumberOfCandidates - CurrentCandidateNumber);

            if (lastAvailableCandidate != null)
            {
                lastAvailableCandidate.ImageUrl = EmptyCandidateImage;
            }

            RestoreButtonSizes(btnThumbsUp, btnThumbsDown);

            // disable the buttons at this point
            DisableThumbsButtons();

            CurrentCandidate = currentCandidate;

            UpdateImages(CandidateState.New);
            currentCandidate.CandidateState = CandidateState.Interview;
        }

        private void ProcessCandidate()
        {
            var gameMode = GameMode;
            var currentCandidate = CurrentCandidate;

            if (currentCandidate == null)
            {
                EnterNewCandidate();
            }
            else if (currentCandidate.CandidateState == CandidateState.Interview)
            {
                UpdateImages(currentCandidate.CandidateState);
                DetermineCandidateRank(currentCandidate);

                if (gameMode == GameMode.Advisor)
                {
                    currentCandidate.CandidateState = CandidateState.Completed;
                }
                else
                {
                    TimerGame.Enabled = false;
                    SessionState = Enums.SessionState.WaitingForUserDecision;
                }
            }
            else if (currentCandidate.CandidateState == CandidateState.Completed)
            {
                DisableThumbsButtons();

                if (currentCandidate.CandidateAccepted)
                {
                    switch (CandidateCompletedStep)
                    {
                        case CandidateCompletedStep.ShowCandidatesMap:
                            UpdatePositionToAcceptedCandidate(currentCandidate);
                            TimerGame.Interval = 3000;
                            CandidateCompletedStep = CandidateCompletedStep.BlinkRemaimingCandidates;
                            break;
                        case CandidateCompletedStep.BlinkRemaimingCandidates:
                            BlinkRemainingCandidates();
                            CandidateCompletedStep = CandidateCompletedStep.RearrangeCandidatesMap;
                            break;
                        case CandidateCompletedStep.PickUniform:
                            PickUniform();
                            break;
                        case CandidateCompletedStep.FillNextPosition:
                            FillNextPosition();

                            if (!AlreadyAskedForRating && gameMode == GameMode.Advisor)
                            {
                                AskForRating = true;
                                AlreadyAskedForRating = true;
                            }
                            break;
                    }
                }
                else
                {
                    CurrentCandidateNumber++;

                    EnterNewCandidate();
                }
            }
        }

        private void BlinkRemainingCandidates()
        {
            NumOfBlinks = 0;
            RemainingBlinkState = BlinkState.Visible;
            TimerGame.Enabled = false;
            TimerBlinkRemainingCandidates.Enabled = true;
            TimerBlinkRemainingCandidates.Interval = 500;
        }

        protected void TimerBlinkRemainingCandidates_Tick(object sender, EventArgs e)
        {
            if (RemainingBlinkState == BlinkState.Visible)
            {
                HideRemainingCandidatesImages();
                RemainingBlinkState = BlinkState.Hidden;
                SetCurrentPositionCellVisibility(RemainingBlinkState);
                NumOfBlinks++;
            }
            else
            {
                ShowRemainingCandidatesImages();
                RemainingBlinkState = BlinkState.Visible;
                SetCurrentPositionCellVisibility(RemainingBlinkState);
            }

            if (NumOfBlinks >= 5)
            {
                TimerBlinkRemainingCandidates.Enabled = false;
                RearrangeCandidatesMap();
            }
        }

        private void RearrangeCandidatesMap()
        {
            NumOfBlinks = 0;
            RemainingBlinkState = BlinkState.Hidden;
            TimerRearrangeCandidatesMap.Enabled = true;
            TimerRearrangeCandidatesMap.Interval = 500;
        }

        protected void TimerRearrangeCandidatesMap_Tick(object sender, EventArgs e)
        {
            if (RemainingBlinkState == BlinkState.Visible)
            {
                HideCandidatesSecondRowImages();
                RemainingBlinkState = BlinkState.Hidden;
                SetCurrentPositionCellVisibility(RemainingBlinkState);
                NumOfBlinks++;
            }
            else
            {
                ShowCandidatesSecondRowImages();
                RemainingBlinkState = BlinkState.Visible;
                SetCurrentPositionCellVisibility(RemainingBlinkState);
            }

            if (NumOfBlinks >= 5)
            {
                TimerRearrangeCandidatesMap.Enabled = false;
                SetCurrentPositionCellVisibility(BlinkState.Visible);
                FullyHideCandidatesSecondRowImages();
                PickUniform();
            }
        }

        private void PickUniform()
        {
            TrainingPassed++;

            if (GameMode == GameMode.Training && TrainingPassed >= 3)
            {
                MultiView2.ActiveViewIndex = 2;
            }
            else
            {
                MultiView2.ActiveViewIndex = 3;
                ShowUniforms();
            }

            CandidateCompletedStep = CandidateCompletedStep.FillNextPosition;
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
            if (TimerEnabled)
            {
                TimerGame.Enabled = TimerEnabled;
            }
            else
            {
                TimerGame.Enabled = defaultCommand;
            }
        }

        private bool CandidateInProcess()
        {
            var currentCandidate = CurrentCandidate;

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
            return (CurrentCandidateNumber < NumberOfCandidates);
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
            var candidatesByNow = CandidatesByNow;

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

            if (GameMode == GameMode.Advisor)
            {
                var accepted = dm.Decide(candidatesByNow, newCandidateIndex);

                newCandidate.CandidateAccepted = accepted;

                if (accepted)
                {
                    IncreaseButtonSize(btnThumbsUp);
                }
                else
                {
                    IncreaseButtonSize(btnThumbsDown);
                }
            }

            SetStatusLabel(newCandidateIndex, candidatesByNow.Count);
            DrawCandidatesByNow(candidatesByNow, newCandidateIndex, this);

            // allow the user to choose
            if (GameMode == GameMode.Training)
            {
                EnableThumbsButtons();
            }
        }

        private void SetStatusLabel(int newCandidateIndex, int totalCandidatesByNow)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("The new candidate has a relative rank of ");
            sb.Append(newCandidateIndex + 1);
            sb.Append(" out of ");
            sb.Append(totalCandidatesByNow);
            sb.Append(".");

            if (GameMode == GameMode.Training)
            {
                sb.Append("<br />");
                sb.Append("Choose to Accept or Reject the candidate, using the thumbs buttons.");
            }

            StatusLabel.Text = sb.ToString();
            StatusLabel.Font.Bold = false;
        }

        private void UpdatePositionToAcceptedCandidate(Candidate candidate)
        {
            var currentPosition = GetCurrentPosition();

            currentPosition.ChosenCandidate = candidate;

            ImageHired.Visible = true;
            ImageInterview.Visible = false;

            AcceptedCandidates[PositionToFill] = currentPosition.ChosenCandidate.CandidateRank;

            double avgRank = CalculateAveragePosition();
            UpdatePositionsTable(currentPosition, avgRank);

            StatusLabel.Text = "It's time to reveal the absolute rankings of the candidates:";
            StatusLabel.Font.Bold = true;
            ShowCandidateMap(currentPosition.ChosenCandidate);
        }

        protected void btnThumbsDown_Click(object sender, EventArgs e)
        {
            AcceptCandidateByUser(false);
            IncreaseButtonSize(btnThumbsDown);
        }

        protected void btnThumbsUp_Click(object sender, EventArgs e)
        {
            AcceptCandidateByUser(true);
            IncreaseButtonSize(btnThumbsUp);
        }

        private void AcceptCandidateByUser(bool accepted)
        {
            var currentCandidate = CurrentCandidate;

            if (currentCandidate == null)
            {
                EnterNewCandidate();
            }
            else if (currentCandidate.CandidateState == CandidateState.Interview)
            {
                currentCandidate.CandidateAccepted = accepted;

                currentCandidate.CandidateState = CandidateState.Completed;
            }

            TimerGame.Enabled = true;
            SessionState = Enums.SessionState.Running;
        }
    }
}