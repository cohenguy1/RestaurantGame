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
        public const int PositionCandidatesNumber = 20;

        public const string PositionCandidiatesStr = "PositionCandidates";
        public const string CurrentCandidateNumberStr = "CurrentCandidateNumber";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Timer1.Enabled = false;
                GenerateCandidatesForPosition();

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
                    CandidateState = CandidateState.New
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
    }


}