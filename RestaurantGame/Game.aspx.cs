using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Game : System.Web.UI.Page
    {
        public const int NumberOfCandidates = DecisionMaker.NumberOfCandidates;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimerGame.Interval = StartTimerInterval;
                TimerGame.Enabled = false;

                AlreadyAskedForRating = false;
                AskForRating = false;

                GamePlayPauseState = PlayPauseState.Playing;

                MultiView2.ActiveViewIndex = 0;

                DisableThumbsButtons();
                RestoreButtonSizes(btnThumbsDown, btnThumbsUp);

                if (GameMode == GameMode.Training)
                {
                    TrainingPassed = 0;
                }

                ClearPositionsTable();

                ClearCandidateImages();

                ImageInterview.Visible = false;
                ImageManForward.Visible = true;

                CurrentPositionNumber = 0;

                StartInterviewsForPosition(0);
            }
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
                Response.Redirect("Quiz.aspx");
            }
        }

        private void StartInterviewsForPosition(int position)
        {
            TimerGame.Enabled = false;

            CurrentCandidate = null;

            StatusLabel.Text = "";

            SetTitle();

            CleanCurrentPosition();
            ShowAllRemainingCandidatesImages();
            ImageHired.Visible = false;

            GenerateCandidatesForPosition();
            GenerateCandidatesByNow();

            CurrentCandidateNumber = 0;
            CandidateCompletedStep = CandidateCompletedStep.Initial;

            TimerGame.Enabled = true;
        }


        private void SetTitle()
        {
            string jobTitle = GetCurrentJobTitle();
            var currentPositionNumber = CurrentPositionNumber;

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
                AlreadyAskedForRating = true;

                return;
            }

            if (NewCandidateAwaits())
            {
                ProcessCandidate();
            }
            else
            {
                if (CurrentPositionNumber < 9 || GameMode == GameMode.Training)
                {
                    FillNextPosition();
                }
                else
                {
                    TimerGame.Enabled = false;
                    Response.Redirect("EndGame.aspx");
                }
            }
        }

        private void FillNextPosition()
        {
            IncreaseCurrentPosition();

            if (GameMode == GameMode.Training && CurrentPositionNumber == 11)
            {
                // wrap around
                CurrentPositionNumber = 0;
            }

            StartInterviewsForPosition(CurrentPositionNumber);
        }

        private void CleanCurrentPosition()
        {
            TimerGame.Enabled = false;

            ClearCandidateImages();
            ClearInterviewImages();

        }

        private void EnterNewCandidate()
        {
            CurrentCandidate = PositionCandidates[CurrentCandidateNumber];

            var lastAvailableCandidate = GetRemainingStickManImage(NumberOfCandidates - CurrentCandidateNumber);

            if (lastAvailableCandidate != null)
            {
                lastAvailableCandidate.ImageUrl = EmptyCandidateImage;
            }

            RestoreButtonSizes(btnThumbsUp, btnThumbsDown);

            // disable the buttons at this point
            DisableThumbsButtons();

            UpdateImages(CandidateState.New);
            CurrentCandidate.CandidateState = CandidateState.Interview;
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
                    SessionState = SessionState.WaitingForUserDecision;
                }
            }
            else if (currentCandidate.CandidateState == CandidateState.Completed)
            {
                if (GameMode == GameMode.Training)
                {
                    DisableThumbsButtons();
                }

                if (currentCandidate.CandidateAccepted)
                {
                    if (!TimerGame.Enabled)
                    {
                        TimerGame.Enabled = true;
                    }
                    TimerGame.Interval = 750;

                    switch (CandidateCompletedStep)
                    {
                        case CandidateCompletedStep.Initial:
                            UpdatePositionToAcceptedCandidate(currentCandidate);
                            CandidateCompletedStep = CandidateCompletedStep.BlinkRemaimingCandidates;
                            NumOfBlinks = 0;
                            NumOfBlinks2 = 0;
                            RemainingBlinkState = BlinkState.Visible;
                            break;
                        case CandidateCompletedStep.BlinkRemaimingCandidates:
                            BlinkRemainingCandidates();

                            if (NumOfBlinks >= 2)
                            {
                                RemainingBlinkState = BlinkState.Hidden;
                                CandidateCompletedStep = CandidateCompletedStep.RearrangeCandidatesMap;
                            }
                            break;
                        case CandidateCompletedStep.RearrangeCandidatesMap:
                            RearrangeCandidatesMap();

                            if (NumOfBlinks2 >= 1)
                            {
                                SetCurrentPositionCellVisibility(BlinkState.Visible);
                                PositionSummary();
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
        }

        private void RearrangeCandidatesMap()
        {
            if (RemainingBlinkState == BlinkState.Visible)
            {
                HideCandidatesSecondRowImages();
                RemainingBlinkState = BlinkState.Hidden;
                SetCurrentPositionCellVisibility(RemainingBlinkState);
            }
            else
            {
                ShowCandidatesSecondRowImages();
                RemainingBlinkState = BlinkState.Visible;
                SetCurrentPositionCellVisibility(RemainingBlinkState);
                NumOfBlinks2++;
            }
        }

        private void PositionSummary()
        {
            TimerGame.Enabled = false;
            TimerGame.Interval = TimerInterval;

            if (NeedToAskRating())
            {
                dbHandler.UpdateTimesTable(GameState.BeforeRate);
            }

            CandidateCompletedStep = CandidateCompletedStep.Initial;
            btnThumbsDown.Visible = false;
            btnThumbsUp.Visible = false;
            btnFastBackwards.Visible = false;
            btnPausePlay.Visible = false;
            btnFastForward.Visible = false;
            LabelSpeed.Visible = false;
            PositionSummaryLbl1.Visible = true;
            PositionSummaryLbl2.Visible = true;
            PositionSummaryLbl3.Visible = true;
            SummaryNextLbl.Visible = true;
            btnNextToUniform.Visible = true;
            PanelBasket.Visible = false;

            PositionSummaryLbl2.Text = CurrentCandidate.CandidateRank.ToString();
            SummaryNextLbl.Text = "<br /><br />Press 'Next' to pick uniform for the " + GetCurrentJobTitle() + ".<br />";
        }

        private void PickUniform()
        {
            if (GameMode == GameMode.Training)
            {
                TrainingPassed++;

                if (TrainingPassed == 1)
                {
                    dbHandler.UpdateTimesTable(GameState.AfterTraining1);
                }
                else if (TrainingPassed == 2)
                {
                    dbHandler.UpdateTimesTable(GameState.AfterTraining2);
                }
                else if (TrainingPassed == 3)
                {
                    dbHandler.UpdateTimesTable(GameState.AfterTraining3);
                }
            }

            if (NeedToAskRating())
            {
                AskForRating = true;
            }

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

        private bool NewCandidateAwaits()
        {
            if (CurrentCandidate == null)
            {
                return true;
            }
            else if (CurrentCandidate.CandidateAccepted && CandidateCompletedStep == CandidateCompletedStep.FillNextPosition)
            {
                // finished candidate
                return false;
            }

            return (CurrentCandidateNumber < NumberOfCandidates); ;
        }

        private void DetermineCandidateRank(Candidate newCandidate)
        {
            var candidatesByNow = CandidatesByNow;

            var dm = new DecisionMaker();

            var newCandidateIndex = dm.GetCandidateRelativePosition(candidatesByNow, newCandidate);

            candidatesByNow.Insert(newCandidateIndex, newCandidate);

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

            sb.Append("The new candidate has a relative ranking of ");
            sb.Append(newCandidateIndex + 1);
            sb.Append(" out of the ");
            sb.Append(totalCandidatesByNow);
            sb.Append(" interviewed by now.");

            if (GameMode == GameMode.Training)
            {
                sb.Append("<br />");
                sb.Append("Choose to Accept or Reject the candidate, using the thumbs buttons below.");
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

            AcceptedCandidates[CurrentPositionNumber] = currentPosition.ChosenCandidate.CandidateRank;

            double avgRank = Common.CalculateAveragePosition(Positions);
            UpdatePositionsTable(currentPosition, avgRank);

            StatusLabel.Text = "It's time to reveal the absolute rankings of the candidates:";
            StatusLabel.Font.Bold = true;
            ShowCandidateMap(currentPosition.ChosenCandidate);
        }

        protected void btnNextToUniform_Click(object sender, EventArgs e)
        {
            btnThumbsDown.Visible = true;
            btnThumbsUp.Visible = true;
            btnFastBackwards.Visible = true;
            btnPausePlay.Visible = true;
            btnFastForward.Visible = true;
            LabelSpeed.Visible = true;
            PositionSummaryLbl1.Visible = false;
            PositionSummaryLbl2.Visible = false;
            PositionSummaryLbl3.Visible = false;
            SummaryNextLbl.Visible = false;
            btnNextToUniform.Visible = false;
            PanelBasket.Visible = true;

            FullyHideCandidatesSecondRowImages();
            PickUniform();
        }
    }
}