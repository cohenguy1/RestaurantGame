using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    // TODO: Divide to classes
    // TODO: Add uniform
    // TODO: random position - you are the uniform picker
    // TODO: Thanks for participating

    public partial class Default : System.Web.UI.Page
    {
        public const int MinTimerInterval = 500;
        public int StartTimerInterval = 1500;
        public const int MaxTimerInterval = 4500;

        public const int PositionCandidatesNumber = DecisionMaker.PositionCandidatesNumber;

        public const string PositionsStr = "Positions";
        public const string PositionToFillStr = "PositionToFill";

        public const string StickManImageList = "StickManImageList";

        public const string PositionCandidiatesStr = "PositionCandidates";
        public const string CurrentCandidateNumberStr = "CurrentCandidateNumber";

        public const string CandidatesByNowStr = "CandidatesByNow";

        public const string AskForRating = "AskForRatingStr";

        public const string AcceptedCandidates = "AcceptedCandidates";

        public const string TimerInterval = "TimerInterval";
        public const string TimerEnabled = "TimerEnabled";

        public const string AlreadyAskedForRating = "AlreadyAskedForRating";

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

                GeneratePositions();

                CreateStickManImageList();

                Session[CurrentCandidateNumberStr] = 0;

                Session[AskForRating] = false;
            }
        }

        private void GeneratePositions()
        {
            var positions = new List<Position>();

            positions.Add(new Position(RestaurantPosition.Manager));
            positions.Add(new Position(RestaurantPosition.HeadChef));
            positions.Add(new Position(RestaurantPosition.Cook));
            positions.Add(new Position(RestaurantPosition.Baker));
            positions.Add(new Position(RestaurantPosition.Dishwasher));
            positions.Add(new Position(RestaurantPosition.Waiter1));
            positions.Add(new Position(RestaurantPosition.Waiter2));
            positions.Add(new Position(RestaurantPosition.Waiter3));
            positions.Add(new Position(RestaurantPosition.Host));
            positions.Add(new Position(RestaurantPosition.Bartender));

            Session[PositionsStr] = positions;

            var acceptedCandidates = new int[positions.Count];
            Session[AcceptedCandidates] = acceptedCandidates;
        }

        private void GenerateCandidatesForPosition()
        {
            var positionCandidates = new List<Candidate>();

            for (var candidateIndex = 0; candidateIndex < PositionCandidatesNumber; candidateIndex++)
            {
                var newCandidate = new Candidate()
                {
                    CandidateState = CandidateState.New,
                    CandidateNumber = candidateIndex,
                    CandidateAccepted = false
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

        private void StartInterviewsForPosition(int position)
        {
            Timer1.Enabled = false;

            Session["Position"] = null;
            Session[PositionToFillStr] = position;

            SetTitle();

            GenerateCandidatesForPosition();
            GenerateCandidatesByNow();

            Session[CurrentCandidateNumberStr] = 0;

            Timer1.Enabled = true;
        }

        private void SetTitle()
        {
            var positionToFill = (int)Session[PositionToFillStr];

            var positions = (List<Position>)Session[PositionsStr];

            PositionHeader.Text = "Position: " + positions[positionToFill].GetJobTitle();
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

            var positionToFill = (int)Session[PositionToFillStr];

            positionToFill++;

            Session[PositionToFillStr] = positionToFill;

            ClearCandidateImages();
            ClearInterviewImages();

            if (positionToFill < 10)
            {
                StartInterviewsForPosition(positionToFill);
            }
        }

        private void EnterNewCandidate()
        {
            var currentCandidateNumber = (int)Session[CurrentCandidateNumberStr];
            var positionCandidates = (List<Candidate>)Session[PositionCandidiatesStr];
            var currentCandidate = positionCandidates[currentCandidateNumber];

            Session["Position"] = currentCandidate;

            UpdateImages(CandidateState.New);
            currentCandidate.CandidateState = CandidateState.Interview;
        }

        private void ProcessCandidate()
        {
            var currentCandidate = (Candidate)Session["Position"];

            if (currentCandidate == null)
            {
                EnterNewCandidate();
            }
            else if (currentCandidate.CandidateState == CandidateState.Interview)
            {
                UpdateImages(currentCandidate.CandidateState);
                DetermineCandidateRank(currentCandidate);

                currentCandidate.CandidateState = CandidateState.Completed;
            }
            else if (currentCandidate.CandidateState == CandidateState.Completed)
            {
                if (currentCandidate.CandidateAccepted)
                {
                    UpdatePositionToAcceptedCandidate(currentCandidate);

                    FillNextPosition();

                    if ((bool)Session[AlreadyAskedForRating] == false)
                    {
                        Session[AskForRating] = true;
                        Session[AlreadyAskedForRating] = true;
                    }

                    Timer1.Interval = 3000;
                }
                else
                {
                    Session[CurrentCandidateNumberStr] = (int)Session[CurrentCandidateNumberStr] + 1;

                    EnterNewCandidate();
                }
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

        private void ClearInterviewImages()
        {
            ImageManForward.Visible = false;
            ImageInterview.Visible = false;
            ImageHired.Visible = true;
        }

        private void UpdateImages(CandidateState candidateState)
        {
            ImageManForward.Visible = (candidateState == CandidateState.New);
            ImageInterview.Visible = (candidateState == CandidateState.Interview);
            ImageHired.Visible = false;
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
            var accepted = dm.Decide(candidatesByNow, newCandidateIndex);

            newCandidate.CandidateAccepted = accepted;

            ImageHandler.DrawCandidatesByNow(candidatesByNow, newCandidateIndex, this);
        }

        private void ClearCandidateImages()
        {
            for (var candidateIndex = 0; candidateIndex < PositionCandidatesNumber; candidateIndex++)
            {
                var stickManImage = GetStickManImage(candidateIndex + 1);
                stickManImage.ImageUrl = null;
                stickManImage.Visible = false;
            }
        }

        private void UpdatePositionToAcceptedCandidate(Candidate candidate)
        {
            var positionToFill = (int)Session[PositionToFillStr];
            var positions = (List<Position>)Session[PositionsStr];

            var currentPosition = positions[positionToFill];

            currentPosition.ChosenCandidate = candidate;
            var positionCell = GetPositionCell(currentPosition);

            var acceptedCandidates = (int[])Session[AcceptedCandidates];
            acceptedCandidates[positionToFill] = currentPosition.ChosenCandidate.CandidateRank;
            Session[AcceptedCandidates] = acceptedCandidates;

            positionCell.Text = " " + currentPosition.GetJobTitle() + ": " + currentPosition.ChosenCandidate.CandidateRank;
            positionCell.ForeColor = System.Drawing.Color.Blue;
            positionCell.Font.Italic = true;
        }

        private TableCell GetPositionCell(Position position)
        {
            switch (position.JobTitle)
            {
                case RestaurantPosition.Manager:
                    return ManagerCell;
                case RestaurantPosition.HeadChef:
                    return HeadChefCell;
                case RestaurantPosition.Cook:
                    return CookCell;
                case RestaurantPosition.Baker:
                    return BakerCell;
                case RestaurantPosition.Waiter1:
                    return Waiter1Cell;
                case RestaurantPosition.Waiter2:
                    return Waiter2Cell;
                case RestaurantPosition.Waiter3:
                    return Waiter3Cell;
                case RestaurantPosition.Host:
                    return HostCell;
                case RestaurantPosition.Bartender:
                    return BartenderCell;
                case RestaurantPosition.Dishwasher:
                    return DishwasherCell;
                default:
                    return null;
            }

        }

        public Image GetStickManImage(int imageNum)
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

        protected void btnNext_Click(object sender, EventArgs e)
        {
            String user = (String)Session["user_id"];

            if (!user.Equals("friend"))
            {
                String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Select Assignment_Id from [User] Where UserId='" + Session["user_id"] + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    string UserId = (string)cmd.ExecuteScalar();

                    if (UserId == null)
                    {
                        //new user -insert to DB
                        DateTime curentT = DateTime.UtcNow;
                        cmd = new SqlCommand("insert into [User] (UserId, Assignment_Id,time) VALUES ('" + Session["user_id"] + "','" + Session["turkAss"] + "','" + curentT.ToString() + "')");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        Alert.Show("You already participated in this game. Please return the HIT");
                        return;
                    }
                }
            }

            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnNext2_Click(object sender, EventArgs e)
        {
            if (rbl1.SelectedIndex == 1 && rbl2.SelectedIndex == 1)
            {
                MultiView1.ActiveViewIndex = 2;
            }
            else
            {
                Alert.Show("wrong answer, please try again");
            }
        }

        protected void btnNext3_Click(object sender, EventArgs e)
        {
            // Save user info to DB
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            String user = (String)Session["user_id"];
            string mobile = "not_mobile";
            if (Request.Browser.IsMobileDevice)
            {
                mobile = "mobile_user";
            }
            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO UserInfo (UserId, Gender, Age, Education, Nationality, Reason, Mobile ) VALUES (@UserId, @Gender, @Age, @Education,@Nationality,@Reason,@Mobile)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                cmd.Parameters.AddWithValue("@UserId", user);
                cmd.Parameters.AddWithValue("@Gender", DropDownList1.Text);
                cmd.Parameters.AddWithValue("@Age", DropDownList2.Text);
                cmd.Parameters.AddWithValue("@Education", DropDownList3.Text);
                cmd.Parameters.AddWithValue("@Nationality", DropDownList4.Text);
                cmd.Parameters.AddWithValue("@Reason", DropDownList5.Text);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
            }

            MultiView1.ActiveViewIndex = 3;

            StartInterviewsForPosition(0);
        }

        protected void btnFB_Click(object sender, EventArgs e)
        {
            Session[TimerEnabled] = Timer1.Enabled;
            Timer1.Enabled = false;

            int newTimerInterval = Math.Min((int)Session[TimerInterval] + 500, MaxTimerInterval);
            Session[TimerInterval] = newTimerInterval;

            UpdateFastPlaySpeed(newTimerInterval);

            Timer1.Enabled = (bool)Session[TimerEnabled];
        }

        protected void btnFF_Click(object sender, EventArgs e)
        {
            Session[TimerEnabled] = Timer1.Enabled;
            Timer1.Enabled = false;

            int newTimerInterval = Math.Max((int)Session[TimerInterval] - 500, MinTimerInterval);
            Session[TimerInterval] = newTimerInterval;

            UpdateFastPlaySpeed(newTimerInterval);

            Timer1.Enabled = (bool)Session[TimerEnabled];
        }

        private void UpdateFastPlaySpeed(int newTimerInterval)
        {
            Timer1.Interval = newTimerInterval;

            FB.Enabled = (newTimerInterval != MaxTimerInterval);
            FF.Enabled = (newTimerInterval != MinTimerInterval);

            string speedRate = (1500.0 / newTimerInterval).ToString("0.0");
            LabelSpeed.Text = " Speed: x" + speedRate;
        }

        protected void RateAdviser()
        {
            Timer1.Enabled = false;

            MultiView2.ActiveViewIndex = 1;
        }

        protected void btnRate_Click(object sender, EventArgs e)
        {
            int agentRating = RatingRbL.SelectedIndex + 1;

            SaveRatingToDB(agentRating);

            MultiView2.ActiveViewIndex = 0;

            Timer1.Enabled = true;
        }

        private void SaveRatingToDB(int agentRating)
        {
            var positionToFill = (int)Session[PositionToFillStr];

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string user = (string)Session["user_id"];

            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO UserRatings (UserId, AdviserRating, RatingPosition, Position1Rank, Position2Rank, " +
                    "Position3Rank, Position4Rank, Position5Rank, Position6Rank, Position7Rank, Position8Rank, Position9Rank, Position10Rank ) " + 
                    " VALUES (@UserId, @AdviserRating, @RatingPosition, @Position1Rank, @Position2Rank, @Position3Rank, @Position4Rank, " +
                    "@Position5Rank, @Position6Rank, @Position7Rank, @Position8Rank, @Position9Rank, @Position10Rank)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                cmd.Parameters.AddWithValue("@UserId", user);
                cmd.Parameters.AddWithValue("@AdviserRating", agentRating.ToString());
                cmd.Parameters.AddWithValue("@RatingPosition", positionToFill.ToString());
                cmd.Parameters.AddWithValue("@Position1Rank", GetChosenPositionToInsertToDb(1));
                cmd.Parameters.AddWithValue("@Position2Rank", GetChosenPositionToInsertToDb(2));
                cmd.Parameters.AddWithValue("@Position3Rank", GetChosenPositionToInsertToDb(3));
                cmd.Parameters.AddWithValue("@Position4Rank", GetChosenPositionToInsertToDb(4));
                cmd.Parameters.AddWithValue("@Position5Rank", GetChosenPositionToInsertToDb(5));
                cmd.Parameters.AddWithValue("@Position6Rank", GetChosenPositionToInsertToDb(6));
                cmd.Parameters.AddWithValue("@Position7Rank", GetChosenPositionToInsertToDb(7));
                cmd.Parameters.AddWithValue("@Position8Rank", GetChosenPositionToInsertToDb(8));
                cmd.Parameters.AddWithValue("@Position9Rank", GetChosenPositionToInsertToDb(9));
                cmd.Parameters.AddWithValue("@Position10Rank", GetChosenPositionToInsertToDb(10));
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private string GetChosenPositionToInsertToDb(int position)
        { 
            var positions = (List<Position>)Session[PositionsStr];

            return positions[position - 1].ChosenCandidate == null ? "NULL" : positions[position - 1].ChosenCandidate.CandidateRank.ToString();
        }

    }


}