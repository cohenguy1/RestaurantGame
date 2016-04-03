using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        protected void btnNextToQuiz_Click(object sender, EventArgs e)
        {
            if (trainingRBL.SelectedIndex == 0)
            {
                // continue training
                MultiView2.ActiveViewIndex = 3;
                ShowUniforms();
            }
            else
            {
                // show quiz
                dbHandler.UpdateTimesTable(GameState.Quiz);

                MultiView1.ActiveViewIndex = 4;
                NumOfWrongQuizAnswers = 0;
            }
        }

    }
}