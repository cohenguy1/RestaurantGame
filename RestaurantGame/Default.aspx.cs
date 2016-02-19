using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    // TODO: Finish uniform
    // TODO: Thanks for participating
    // TODO GameBackground Stub
    // TODO: Bonus, according to the performance of the advisor
    // Bonus rephrasing
    // Candidates Blink
    // Candidates Second Row
    // Training Prev., After Training Rephrasing
    // Accept/Reject of advisor
    // Text above walking
    // Bugs
    // Rank Blinks

    public partial class Default : System.Web.UI.Page
    {
        public const int PositionCandidatesNumber = DecisionMaker.PositionCandidatesNumber;


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
                    UserId = "friend";
                    Session["turkAss"] = "turkAss";
                    Session["hitId"] = "hit id friend";
                    btnNext0.Enabled = true;
                }

                Timer1.Enabled = false;
                Timer1.Interval = StartTimerInterval;

                AlreadyAskedForRating = false;

                TimerInterval = StartTimerInterval;
                TimerEnabled = true;

                GameState = GameState.Playing;

                GeneratePositions();

                CurrentCandidateNumber = 0;

                AskForRating = false;
            }
        }

        private void StartInterviewsForPosition(int position)
        {
            Timer1.Enabled = false;

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
            Timer1.Interval = TimerInterval;

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
            Timer1.Enabled = false;

            IncreaseCurrentPosition();

            ClearCandidateImages();
            ClearInterviewImages();

            var currentPositionNumber = GetCurrentPositionNumber();

            if (currentPositionNumber < 10)
            {
                StartInterviewsForPosition(currentPositionNumber);
            }
            else if (GameMode == GameMode.Training)
            {
                // wrap around
                StartInterviewsForPosition(0);
            }
        }

        private void EnterNewCandidate()
        {
            var currentCandidate = PositionCandidates[CurrentCandidateNumber];

            var lastAvailableCandidate = GetRemainingStickManImage(PositionCandidatesNumber - CurrentCandidateNumber);

            if (lastAvailableCandidate != null)
            {
                lastAvailableCandidate.ImageUrl = null;
                lastAvailableCandidate.Visible = false;
            }

            RestoreButtonSizes(btnThumbsUp, btnThumbsDown);

            // disable the buttons at this point
            DisableBtn(btnThumbsDown);
            DisableBtn(btnThumbsUp); 

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
                    Timer1.Enabled = false;
                }
            }
            else if (currentCandidate.CandidateState == CandidateState.Completed)
            {
                DisableBtn(btnThumbsDown);
                DisableBtn(btnThumbsUp);

                if (currentCandidate.CandidateAccepted)
                {
                    var candidateCompletedStep = CandidateCompletedStep;

                    switch (candidateCompletedStep)
                    {
                        case CandidateCompletedStep.ShowCandidatesMap:
                            UpdatePositionToAcceptedCandidate(currentCandidate);
                            Timer1.Interval = 6000;
                            CandidateCompletedStep = CandidateCompletedStep.PickUniform;
                            break;
                        case CandidateCompletedStep.PickUniform:

                            Timer1.Enabled = false;
                            TrainingPassed++;

                            if (gameMode == GameMode.Training && TrainingPassed >= 3)
                            {
                                MultiView2.ActiveViewIndex = 2;
                            }
                            else
                            {
                                MultiView2.ActiveViewIndex = 3;
                                ShowUniforms();
                            }

                            CandidateCompletedStep = CandidateCompletedStep.FillNextPosition;
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
                Timer1.Enabled = TimerEnabled;
            }
            else
            {
                Timer1.Enabled = defaultCommand;
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
            return (CurrentCandidateNumber < PositionCandidatesNumber);
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
                EnableBtn(btnThumbsUp);
                EnableBtn(btnThumbsDown);
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

        private void EnableBtn(ImageButton btn)
        {
            EnableDisableBtn(btn, true);
        }

        private void DisableBtn(ImageButton btn)
        {
            EnableDisableBtn(btn, false);
        }

        private void EnableDisableBtn(ImageButton btn, bool enable)
        {
            btn.Enabled = enable;

            if (enable)
            {
                btn.Style.Remove(HtmlTextWriterStyle.Cursor);
                btn.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
            }
            else
            {
                btn.Style.Remove(HtmlTextWriterStyle.Cursor);
                btn.Style.Add(HtmlTextWriterStyle.Cursor, "default");
            }
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

            Timer1.Enabled = true;
        }
    }
}