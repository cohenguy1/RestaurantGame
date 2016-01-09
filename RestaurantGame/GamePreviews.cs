using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
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
            MultiviewInstructions.ActiveViewIndex = 0;

            Session[GameModeStr] = GameMode.Training;
        }

    }
}