using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class UserInfoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNextToInstructions_Click(object sender, EventArgs e)
        {
            // Save user info to DB
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand
                        ("INSERT INTO UserInfo (UserId, Gender, Age, Education, Nationality, Reason, VectorNum, AskPosition, time) " +
                         "VALUES (@UserId, @Gender, @Age, @Education, @Nationality, @Reason, @VectorNum, @AskPosition, @time)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    //cmd.Parameters.AddWithValue("@Gender", DropDownList1.Text);
                    cmd.Parameters.AddWithValue("@Age", DropDownList2.Text);
                    cmd.Parameters.AddWithValue("@Education", DropDownList3.Text);
                    cmd.Parameters.AddWithValue("@Nationality", DropDownList4.Text);
                    cmd.Parameters.AddWithValue("@Reason", DropDownList5.Text);
                    cmd.Parameters.AddWithValue("@VectorNum", VectorNum);
                    cmd.Parameters.AddWithValue("@AskPosition", AskPosition.ToString());
                    cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString());
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error: " + Environment.NewLine + ex.Message);
                return;
            }

            Response.Redirect("InstructionsPage.aspx");
            /*
            MultiView1.ActiveViewIndex = 2;
            MultiviewInstructions.ActiveViewIndex = 0;
            ProgressBar1.Value = 0;
            */
            InstructionsStopwatch = new Stopwatch();
            InstructionsStopwatch.Start();

            dbHandler.UpdateTimesTable(GameState.Instructions);
        }
    }
}