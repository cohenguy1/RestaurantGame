using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        public static Stopwatch InstructionsTimer = new Stopwatch();

        protected void btnNextToInfo_Click(object sender, EventArgs e)
        {
            if (!UserId.Equals("friend"))
            {
                String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand("Select Assignment_Id from [Users] Where UserId='" + UserId + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    string assignmentId = (string)cmd.ExecuteScalar();

                    if (assignmentId == null)
                    {
                        //new user -insert to DB
                        DateTime currentTime = DateTime.Now;
                        cmd = new SQLiteCommand("insert into [Users] (UserId, Assignment_Id, time) VALUES ('" + UserId + "','" + Session["turkAss"] + "','" + currentTime.ToString() + "')");
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

        protected void btnNextToTraining_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 6;

            GameMode = GameMode.Training;
            DisableThumbsButtons();
            TrainingPassed = 0;

            CurrentPositionNumber = 0;
            StartInterviewsForPosition(0);
        }

        protected void btnNextToInstructions_Click(object sender, EventArgs e)
        {
            // Save user info to DB
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string mobile = "not_mobile";

            string heightS = HttpContext.Current.Request.Params["clientScreenHeight"];
            string widthS = HttpContext.Current.Request.Params["clientScreenWidth"];

            int height;
            int width;
            bool heightConvertResult = int.TryParse(heightS, out height);
            bool widthConvertResult = int.TryParse(widthS, out width);
            
            if (Request.Browser.IsMobileDevice)
            {
                MultiView1.ActiveViewIndex = 9;
                return;
            }

            if (heightConvertResult && widthConvertResult)
            {
                if (height < 200 || width < 700)
                {
                    MultiView1.ActiveViewIndex = 9;
                    return;
                }
            }

            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserInfo (UserId, Gender, Age, Education, Nationality, Reason, VectorNum, AskPosition) VALUES (@UserId, @Gender, @Age, @Education,@Nationality,@Reason,@VectorNum, @AskPosition)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Gender", DropDownList1.Text);
                    cmd.Parameters.AddWithValue("@Age", DropDownList2.Text);
                    cmd.Parameters.AddWithValue("@Education", DropDownList3.Text);
                    cmd.Parameters.AddWithValue("@Nationality", DropDownList4.Text);
                    cmd.Parameters.AddWithValue("@Reason", DropDownList5.Text);
                    cmd.Parameters.AddWithValue("@VectorNum", DbHandler.VectorNum);
                    cmd.Parameters.AddWithValue("@AskPosition", AskPosition.ToString());
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                Alert.Show("Error: " + Environment.NewLine + ex.Message);
                return;
            }



            MultiView1.ActiveViewIndex = 2;
            MultiviewInstructions.ActiveViewIndex = 0;
            ProgressBar1.Value = 0;

            InstructionsTimer.Start();
        }

        protected void btnNextToGame_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 6;

            GameMode = GameMode.Advisor;
            RestoreButtonSizes(btnThumbsDown, btnThumbsUp);

            MultiView2.ActiveViewIndex = 0;

            ClearCandidateImages();

            ImageInterview.Visible = false;
            ImageManForward.Visible = true;

            CurrentPositionNumber = 0;

            ClearPositionsTable();

            StartInterviewsForPosition(0);
        }

        protected void rewardBtn_Click(object sender, EventArgs e)
        {
            NameValueCollection data = new NameValueCollection();
            data.Add("assignmentId", (String)Session["turkAss"]);
            data.Add("workerId", (String)Session["user_id"]);
            data.Add("hitId", (String)Session["hitId"]);

            Alert.RedirectAndPOST(this.Page, "https://www.mturk.com/mturk/externalSubmit", data);
        }
    }
}