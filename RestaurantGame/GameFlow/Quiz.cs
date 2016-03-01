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
        public const int MaxNumOfWrongQuizAnswers = 5;

        protected void btnNextToProceedToGame_Click(object sender, EventArgs e)
        {
            if (rbl1.SelectedIndex == 0 &&
                rbl2.SelectedIndex == 3 &&
                rbl3.SelectedIndex == 3 &&
                rbl4.SelectedIndex == 1)
            {
                MultiView1.ActiveViewIndex = 5;
            }
            else
            {
                NumOfWrongQuizAnswers++;

                if (NumOfWrongQuizAnswers >= MaxNumOfWrongQuizAnswers)
                {
                    QuizWrongLbl.Text = "&nbsp;You have been wrong for " + MaxNumOfWrongQuizAnswers + " times.";
                    MultiView1.ActiveViewIndex = 8;
                }
                else
                {
                    var triesRemianing = MaxNumOfWrongQuizAnswers - NumOfWrongQuizAnswers;
                    Alert.Show("Wrong answer, please try again. You have " + triesRemianing + " tries remianing.");
                }
            }
        }
    }
}