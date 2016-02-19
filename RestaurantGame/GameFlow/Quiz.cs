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
        protected void btnNextToProceedToGame_Click(object sender, EventArgs e)
        {
            if (rbl1.SelectedIndex == 0 && rbl2.SelectedIndex == 3 && rbl3.SelectedIndex == 3)
            {
                MultiView1.ActiveViewIndex = 5;
            }
            else
            {
                Alert.Show("wrong answer, please try again");
            }
        }
    }
}