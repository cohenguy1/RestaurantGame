using Amazon.WebServices.MechanicalTurk;
using RestaurantGame.Enums;
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
        protected void btnNextToInfo_Click(object sender, EventArgs e)
        {
            GameStopwatch = new Stopwatch();
            GameStopwatch.Start();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            if (!UserId.Equals("friend"))
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand("Select UserId from [UserRatings] Where UserId='" + UserId + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    string userId = (string)cmd.ExecuteScalar();

                    if (userId == null)
                    {
                        //new user - insert to DB
                        cmd = new SQLiteCommand("INSERT INTO Users (UserId, Assignment_Id, hitId, time) VALUES (@UserId, @Assignment_Id, @hitId, @time)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@Assignment_Id", (string)Session["turkAss"]);
                        cmd.Parameters.AddWithValue("@hitId", (string)Session["hitId"]);
                        cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString());
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        Alert.Show("You already participated in this game. Please return the HIT");
                        return;
                    }
                }
            }
            else
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Users (UserId, Assignment_Id, hitId, time) VALUES (@UserId, @Assignment_Id, @hitId, @time)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Assignment_Id", (string)Session["turkAss"]);
                    cmd.Parameters.AddWithValue("@hitId", (string)Session["hitId"]);
                    cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString());
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                }
            }


            string heightS = HttpContext.Current.Request.Params["clientScreenHeight"];
            string widthS = HttpContext.Current.Request.Params["clientScreenWidth"];

            int height;
            int width;
            bool heightConvertResult = int.TryParse(heightS, out height);
            bool widthConvertResult = int.TryParse(widthS, out width);

            if (Request.Browser.IsMobileDevice)
            {
                Response.Redirect("ResolutionProblem.aspx");
                return;
            }

            if (heightConvertResult && widthConvertResult)
            {
                if (height < 200 || width < 700)
                {
                    Response.Redirect("ResolutionProblem.aspx");
                    return;
                }
            }

            // experiment opened from iexplorer
            HttpBrowserCapabilities browser = Request.Browser;
            var browserType = browser.Type.ToLower();
            if (browserType.Contains("internetexplorer"))
            {
                Response.Redirect("IExplorerProblem.aspx");
                return;
            }

            Response.Redirect("UserInfoPage.aspx");
        }
    }
}