﻿using RestaurantGame.Enums;
using RestaurantGame.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public partial class DbHandler : System.Web.UI.Page
    {
        public AskPositionHeuristic GetAskPosition(bool isFriend)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            if (isFriend)
            {
                Random ran = new Random();
                int randomAsk = ran.Next(5);
                VectorNum = ran.Next(50) + 1;
                
                switch (randomAsk)
                {
                    case 0:
                        return AskPositionHeuristic.First;
                    case 1:
                        return AskPositionHeuristic.Last;
                    case 2:
                        RandomHuristicAskPosition = GetRandomAskPosition();
                        return AskPositionHeuristic.Random;
                    case 3:
                        return AskPositionHeuristic.Optimal;
                    case 4:
                        return AskPositionHeuristic.MonteCarlo;
                    default:
                        return AskPositionHeuristic.First;
                }
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.First);
            if (VectorNum != null)
            {
                return AskPositionHeuristic.First;
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.Optimal);
            if (VectorNum != null)
            {
                return AskPositionHeuristic.Optimal;
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.MonteCarlo);
            if (VectorNum != null)
            {
                return AskPositionHeuristic.MonteCarlo;
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.Last);
            if (VectorNum != null)
            {
                return AskPositionHeuristic.Last;
            }

            VectorNum = GetFirstVectorSatisfying(AskPositionHeuristic.Random);
            if (VectorNum != null)
            {
                RandomHuristicAskPosition = GetRandomAskPosition();
                return AskPositionHeuristic.Random;
            }

            throw new Exception("No Hit Slots available");
        }

        private int GetRandomAskPosition()
        {
            Random ran = new Random();
            return ran.Next(Common.NumOfPositions) + 1;
        }

        public void SetVectorAssignmentNull()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("update VectorsAssignments set LastStarted = NULL Where VectorNum=" + VectorNum))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int[] GetCandidateRanksForPosition(int positionNumber)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            int[] ranks = new int[Common.NumOfCandidates];

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("Select Rank1,Rank2,Rank3,Rank4,Rank5,Rank6, " +
                    "Rank7,Rank8,Rank9,Rank10 " +
                    "from Vectors Where VectorNum=" + VectorNum +
                    " and PositionNum = " + positionNumber))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    using (SQLiteDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            for (int i = 0; i < Common.NumOfCandidates; i++)
                            {
                                ranks[i] = result.GetInt32(i);
                            }
                        };
                    }
                }
            }

            return ranks;
        }

        public static int? GetFirstVectorSatisfying(AskPositionHeuristic askPosition)
        {
            var askPositionHeuristics = askPosition.ToString();

            if (!IsAskHeuristicsEnabled(askPositionHeuristics))
            {
                return null;
            }

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("Select VectorNum, LastStarted " +
                    "from VectorsAssignments Where NextAskHeuristic='" + askPositionHeuristics + "' order by VectorNum"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    int vectorNum = 0;

                    using (SQLiteDataReader result = (SQLiteDataReader)cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            vectorNum = result.GetInt32(0);

                            DateTime lastStarted;
                            double diffFromNow;

                            if (!result.IsDBNull(1))
                            {
                                lastStarted = DateTime.Parse(result.GetString(1), new CultureInfo("fr-FR", false));
                                diffFromNow = (DateTime.Now - lastStarted).TotalHours;

                                if (diffFromNow < 0.5)
                                {
                                    continue;
                                }
                            }

                            var lastStartedStr = DateTime.Now.ToString(new CultureInfo("fr-FR", false));

                            using (SQLiteCommand cmd2 = new SQLiteCommand("update VectorsAssignments set LastStarted='" + lastStartedStr + "' " +
                                "where VectorNum = " + vectorNum))
                            {
                                cmd2.CommandType = CommandType.Text;
                                cmd2.Connection = sqlConnection1;

                                cmd2.ExecuteNonQuery();
                            }

                            sqlConnection1.Close();
                            return vectorNum;
                        }
                    }
                }

                sqlConnection1.Close();
            }

            return null;
        }

        private static bool IsAskHeuristicsEnabled(string askPositionHeuristics)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            bool enabled = false;
            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    sqlConnection1.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand("Select Enabled from Configuration Where AskHeuristics='" + askPositionHeuristics + "'"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;

                        using (SQLiteDataReader result = (SQLiteDataReader)cmd.ExecuteReader())
                        {
                            while (result.Read())
                            {
                                int value = result.GetInt32(0);
                                enabled = value == 1 ? true : false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                enabled = false;
            }

            return enabled;
        }

        public static int GetIntFromConfig(string key)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            string value;
            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("Select Value from Configuration Where Key='" + key + "'"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    value = (string)cmd.ExecuteScalar();
                }
            }

            return GetIntFromString(value);
        }

        private static int GetIntFromString(string value)
        {
            int intValue;

            if ((value == null) || (!int.TryParse(value, out intValue)))
            {
                return 0;
            }

            return intValue;
        }

        public void SetVectorNextAskPosition(string nextAskPosition)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("update VectorsAssignments set NextAskHeuristic ='" + nextAskPosition + "' Where VectorNum=" + VectorNum))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();

                    cmd.ExecuteNonQuery();
                }

                using (SQLiteCommand cmd2 = new SQLiteCommand("update VectorsAssignments set LastStarted = NULL Where VectorNum=" + VectorNum))
                {
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = sqlConnection1;

                    cmd2.ExecuteNonQuery();
                }
            }
        }

        private string GetGameStateColumn(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.UserInfo:
                    return "UserInfo";
                case GameState.Instructions:
                    return "Instructions";
                case GameState.TrainingStart:
                    return "TrainingStart";
                case GameState.AfterTraining1:
                    return "AfterTraining1";
                case GameState.AfterTraining2:
                    return "AfterTraining2";
                case GameState.AfterTraining3:
                    return "AfterTraining3";
                case GameState.Quiz:
                    return "Quiz";
                case GameState.GameStart:
                    return "GameStart";
                case GameState.BeforeRate:
                    return "BeforeRate";
                case GameState.Rate:
                    return "Rate";
                case GameState.AfterRate:
                    return "AfterRate";
                case GameState.EndGame:
                    return "EndGame";
                case GameState.CollectedPrize:
                    return "CollectedPrize";
            }

            return null;
        }

        public void UpdateTimesTable(GameState gameState)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            GameStateStopwatch.Stop();
            var minutes = Math.Round(GameStateStopwatch.Elapsed.TotalMinutes, 1);

            if (gameState == GameState.UserInfo)
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("Select UserId from [Times] Where UserId='" + UserId + "'"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();

                        string userId = (string)cmd.ExecuteScalar();

                        if (userId != null)
                        {
                            //new user - insert to DB
                            using (SQLiteCommand cmd2 = new SQLiteCommand("Delete from Times Where UserId='" + UserId + "'"))
                            {
                                cmd2.CommandType = CommandType.Text;
                                cmd2.Connection = sqlConnection1;
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }

                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Times (UserId, UserInfo, Instructions, TrainingStart," +
                        " AfterTraining1, AfterTraining2, AfterTraining3, Quiz, GameStart, BeforeRate, Rate, AfterRate, EndGame, CollectedPrize) VALUES " +
                        "(@UserId, @UserInfo, @Instructions, @TrainingStart, @AfterTraining1, @AfterTraining2, @AfterTraining3, " +
                        " @Quiz, @GameStart, @BeforeRate, @Rate, @AfterRate, @EndGame, @CollectedPrize)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@UserInfo", null);
                        cmd.Parameters.AddWithValue("@Instructions", null);
                        cmd.Parameters.AddWithValue("@TrainingStart", null);
                        cmd.Parameters.AddWithValue("@AfterTraining1", null);
                        cmd.Parameters.AddWithValue("@AfterTraining2", null);
                        cmd.Parameters.AddWithValue("@AfterTraining3", null);
                        cmd.Parameters.AddWithValue("@Quiz", null);
                        cmd.Parameters.AddWithValue("@GameStart", null);
                        cmd.Parameters.AddWithValue("@BeforeRate", null);
                        cmd.Parameters.AddWithValue("@Rate", null);
                        cmd.Parameters.AddWithValue("@AfterRate", null);
                        cmd.Parameters.AddWithValue("@EndGame", null);
                        cmd.Parameters.AddWithValue("@CollectedPrize", null);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            string gameStateColumn = GetGameStateColumn(gameState);

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand("Update Times set " + gameStateColumn + " = " + minutes + " Where UserId='" + UserId + "'"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            GameStateStopwatch.Restart();
        }
    }
}