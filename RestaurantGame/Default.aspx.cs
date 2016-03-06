using RestaurantGame.Enums;
using System;
using System.Text;

namespace RestaurantGame
{
    // TODO limit screen size
    // You were wrong 3 times
    // Why jumping
    // divide to web pages

    // Next Generation
    // TODO convert instructions to javascript

    public partial class Default : System.Web.UI.Page
    {
        public const int NumberOfCandidates = DecisionMaker.NumberOfCandidates;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string val = null;
                
                val = Request.QueryString["assignmentId"];
            
                // friend assigment
                if (val == null)
                {
                    Session["user_id"] = "friend";
                    Session["turkAss"] = "turkAss";
                    Session["hitId"] = "hit id friend";
                    btnNextToInfo.Enabled = true;
                }
                //from AMT but did not took the assigment
                else if (val.Equals("ASSIGNMENT_ID_NOT_AVAILABLE"))
                {
                    btnNextToInfo.Enabled = false;
                    return;
                }
                //from AMT and accepted the assigment - continue to experiment
                else
                {
                    Session["user_id"] = Request.QueryString["workerId"];	//save participant's user ID
                    Session["turkAss"] = val;                               //save participant's assignment ID
                    Session["hitId"] = Request.QueryString["hitId"];        // save the hit id
                    btnNextToInfo.Enabled = true;
                }

                DecideRandomStuff();

                TimerGame.Enabled = false;
                TimerGame.Interval = StartTimerInterval;

                CurrentPositionNumber = 0;
                AlreadyAskedForRating = false;

                TimerInterval = StartTimerInterval;
                TimerEnabled = true;

                GameState = GameState.Playing;

                GeneratePositions();

                CurrentCandidateNumber = 0;

                AskForRating = false;
            }
        }

        private void DecideRandomStuff()
        {
            Random rand = new Random();
            int randHim = rand.Next(2);
            if (randHim == 1)
            {
                backgroundText.Text = backgroundText.Text.Replace("him", "her");
                backgroundText2.Text = backgroundText2.Text.Replace("he", "she");
            }

            AskPosition = DbHandler.GetAskPosition(UserId == "friend");
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
            CandidateCompletedStep = CandidateCompletedStep.ShowCandidatesMap;

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
                    EndGame();
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

        private void EndGame()
        {
            TimerGame.Enabled = false;
            GameStopwatch.Stop();
            MultiView1.ActiveViewIndex = 7;
            double averageRank = CalculateAveragePosition();
            double bonus = 50 - averageRank;
            AverageRank.Text = averageRank.ToString("0.0");
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
                    SessionState = SessionState.WaitingForUserDecision;
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
                TimerBlinkRemainingCandidates.Interval = 250;
            }
            else
            {
                ShowRemainingCandidatesImages();
                RemainingBlinkState = BlinkState.Visible;
                SetCurrentPositionCellVisibility(RemainingBlinkState);
                TimerBlinkRemainingCandidates.Interval = 350;
            }

            if (NumOfBlinks >= 2)
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
            }
            else
            {
                ShowCandidatesSecondRowImages();
                RemainingBlinkState = BlinkState.Visible;
                SetCurrentPositionCellVisibility(RemainingBlinkState);
                NumOfBlinks++;
            }

            if (NumOfBlinks >= 2)
            {
                TimerRearrangeCandidatesMap.Enabled = false;
                SetCurrentPositionCellVisibility(BlinkState.Visible);
                PositionSummary();
            }
        }

        private void PositionSummary()
        {
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
            TrainingPassed++;

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

            double avgRank = CalculateAveragePosition();
            UpdatePositionsTable(currentPosition, avgRank);

            StatusLabel.Text = "It's time to reveal the absolute rankings of the candidates:";
            StatusLabel.Font.Bold = true;
            ShowCandidateMap(currentPosition.ChosenCandidate);
        }

        protected void btnThumbsDown_Click(object sender, EventArgs e)
        {
            if (CurrentCandidateNumber >= NumberOfCandidates - 1)
            {
                Alert.Show("You cannot reject the last candidate, no more candidates avaliable.");
                return;
            }

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

            TimerGame.Interval = 500;
            TimerGame.Enabled = true;
            SessionState = SessionState.Running;
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