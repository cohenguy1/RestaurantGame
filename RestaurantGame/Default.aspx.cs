using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        public const int PositionCandidatesNumber = DecisionMaker.PositionCandidatesNumber;

        public const string StickManImageList = "StickManImageList";

        public const string PositionCandidiatesStr = "PositionCandidates";
        public const string CurrentCandidateNumberStr = "CurrentCandidateNumber";

        public const string CandidatesByNowStr = "CandidatesByNow";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Timer1.Enabled = false;

                GenerateCandidatesForPosition();
                GenerateCandidatesByNow();

                CreateStickManImageList();

                Session[CurrentCandidateNumberStr] = 0;

                Timer1.Enabled = true;
            }

        }

        private void GenerateCandidatesForPosition()
        {
            var positionCandidates = new List<Candidate>();

            for (var candidateIndex = 0; candidateIndex < PositionCandidatesNumber; candidateIndex++)
            {
                var newCandidate = new Candidate()
                {
                    CandidateState = CandidateState.New,
                    CandidateNumber = candidateIndex
                };

                positionCandidates.Add(newCandidate);
            }

            var ranks = new List<int>();
            for (var index = 1; index <= PositionCandidatesNumber; index++)
            {
                ranks.Add(index);
            }

            var ranksRemaining = PositionCandidatesNumber;
            var randomGenerator = new Random();

            for (var index = 0; index < PositionCandidatesNumber; index++)
            {
                var position = randomGenerator.Next(1, ranksRemaining) - 1;

                positionCandidates[index].CandidateRank = ranks[position];

                ranks.RemoveAt(position);
                ranksRemaining--;
            }

            Session[PositionCandidiatesStr] = positionCandidates;
        }

        private void GenerateCandidatesByNow()
        {
            var candidatesByNow = new List<Candidate>();
            Session[CandidatesByNowStr] = candidatesByNow;
        }

        private void CreateStickManImageList()
        {
            var stickManImageList = new List<Image>();

            stickManImageList.Add(StickMan1);
            stickManImageList.Add(StickMan2);
            stickManImageList.Add(StickMan3);
            stickManImageList.Add(StickMan4);
            stickManImageList.Add(StickMan5);
            stickManImageList.Add(StickMan6);
            stickManImageList.Add(StickMan7);
            stickManImageList.Add(StickMan8);
            stickManImageList.Add(StickMan9);
            stickManImageList.Add(StickMan10);
            stickManImageList.Add(StickMan11);
            stickManImageList.Add(StickMan12);
            stickManImageList.Add(StickMan13);
            stickManImageList.Add(StickMan14);
            stickManImageList.Add(StickMan15);
            stickManImageList.Add(StickMan16);
            stickManImageList.Add(StickMan17);
            stickManImageList.Add(StickMan18);
            stickManImageList.Add(StickMan19);
            stickManImageList.Add(StickMan20);

            Session[StickManImageList] = stickManImageList;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {         
            if (NewCandidateAwaits())
            {
                if (CandidateInProcess())
                {
                    ProcessCandidate();
                }
                else
                {
                    EnterNewCandidate();
                }
            }
            else
            {
                Timer1.Enabled = false;
            }
        }

        private void EnterNewCandidate()
        {
            var currentCandidateNumber = (int)Session[CurrentCandidateNumberStr];
            var positionCandidates = (List<Candidate>)Session[PositionCandidiatesStr];
            var currentCandidate = positionCandidates[currentCandidateNumber];

            Session["Position"] = currentCandidate;

            ImageManForward.Visible = true;
            ImageManBack.Visible = false;
            ImageInterview.Visible = false;

            currentCandidate.CandidateState = CandidateState.Interview;
        }

        private void ProcessCandidate()
        {
            var currentCandidate = (Candidate)Session["Position"];

            if (currentCandidate.CandidateState == CandidateState.Interview)
            {
                UpdateImages(currentCandidate.CandidateState);
                DetermineCandidateRank(currentCandidate);

                currentCandidate.CandidateState = CandidateState.InterviewEnded;
            }
            else if (currentCandidate.CandidateState == CandidateState.InterviewEnded)
            {
                UpdateImages(currentCandidate.CandidateState);

                currentCandidate.CandidateState = CandidateState.Completed;
                Session[CurrentCandidateNumberStr] = (int)Session[CurrentCandidateNumberStr] + 1;
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
                                     (currentCandidate.CandidateState == CandidateState.InterviewEnded);

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
            ImageManBack.Visible = (candidateState == CandidateState.InterviewEnded);
            ImageInterview.Visible = (candidateState == CandidateState.Interview);
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

            var dm = new DecisionMaker();
            var accepted = dm.Decide(candidatesByNow, newCandidate);

            candidatesByNow.Insert(newCandidateIndex, newCandidate);
            DrawCandidatesByNow(candidatesByNow, newCandidateIndex);
        }

        private void DrawCandidatesByNow(List<Candidate> candidatesByNow, int newCandidateIndex)
        {
            for (var candidateIndex = 0; candidateIndex < candidatesByNow.Count; candidateIndex++)
            {
                var stickManImage = GetStickManImage(candidateIndex + 1);

                var oldStickManImage = stickManImage.ImageUrl;
                string newStickManImage;

                if (candidateIndex == newCandidateIndex)
                {
                    newStickManImage = "~/Images/StickMan" + (newCandidateIndex + 1) + "Red.png";
                }
                else
                {
                    if ((candidateIndex == 0) || (candidateIndex == candidatesByNow.Count - 1))
                    {
                        newStickManImage = "~/Images/StickMan" + (candidateIndex + 1) + ".png";
                    }
                    else
                    {
                        newStickManImage = "~/Images/StickMan.png";
                    }
                }

                if (newStickManImage != oldStickManImage)
                {
                    stickManImage.ImageUrl = newStickManImage;
                    stickManImage.Visible = true;
                }
            }
        }

        private Image GetStickManImage(int imageNum)
        {
            switch (imageNum)
            {
                case 1:
                    return StickMan1;
                case 2:
                    return StickMan2;
                case 3:
                    return StickMan3;
                case 4:
                    return StickMan4;
                case 5:
                    return StickMan5;
                case 6:
                    return StickMan6;
                case 7:
                    return StickMan7;
                case 8:
                    return StickMan8;
                case 9:
                    return StickMan9;
                case 10:
                    return StickMan10;
                case 11:
                    return StickMan11;
                case 12:
                    return StickMan12;
                case 13:
                    return StickMan13;
                case 14:
                    return StickMan14;
                case 15:
                    return StickMan15;
                case 16:
                    return StickMan16;
                case 17:
                    return StickMan17;
                case 18:
                    return StickMan18;
                case 19:
                    return StickMan19;
                case 20:
                    return StickMan20;
                default:
                    return StickMan1;
            }
        }
    }


}