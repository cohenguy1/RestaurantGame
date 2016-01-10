﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
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