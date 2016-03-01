﻿using RestaurantGame.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public class DbHandler
    {
        private static string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        public static int VectorNum;


        public static int RandomHuristicAskPosition;

        public static AskPositionHeuristic GetAskPosition()
        {
            var firstPlaceAskRequests = GetIntFromConfig("FirstPlaceAskRequests");
            var askRequestPerMetodology = GetIntFromConfig("AskRequestsPerMetodology");

            if (firstPlaceAskRequests < askRequestPerMetodology)
            {
                firstPlaceAskRequests++;
                VectorNum = firstPlaceAskRequests;
                SetIntToConfig("FirstPlaceAskRequests", firstPlaceAskRequests);

                return AskPositionHeuristic.First;
            }

            var lastPlaceAskRequests = GetIntFromConfig("LastPlaceAskRequests");
            if (lastPlaceAskRequests < askRequestPerMetodology)
            {
                lastPlaceAskRequests++;
                VectorNum = lastPlaceAskRequests;
                SetIntToConfig("LastPlaceAskRequests", lastPlaceAskRequests);

                return AskPositionHeuristic.Last;
            }

            Random random = new Random();
            RandomHuristicAskPosition = random.Next(10);

            var randomPlaceAskRequests = GetIntFromConfig("RandomPlaceAskRequests");
            if (randomPlaceAskRequests < askRequestPerMetodology)
            {
                randomPlaceAskRequests++;
                VectorNum = randomPlaceAskRequests;
                SetIntToConfig("RandomPlaceAskRequests", randomPlaceAskRequests);

                return AskPositionHeuristic.Random;
            }

            var optimalPlaceAskRequests = GetIntFromConfig("OptimalPlaceAskRequests");
            // wrap around
            VectorNum = (optimalPlaceAskRequests % askRequestPerMetodology) + 1;

            optimalPlaceAskRequests++;
            SetIntToConfig("OptimalPlaceAskRequests", optimalPlaceAskRequests);
            return AskPositionHeuristic.Optimal;
        }

        public static int[] GetCandidateRanksForPosition(int positionNumber)
        {
            int[] ranks = new int[DecisionMaker.NumberOfCandidates];
            SQLiteDataReader result;

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand("Select Rank1,Rank2,Rank3,Rank4,Rank5,Rank6, " + 
                    "Rank7,Rank8,Rank9,Rank10,Rank11,Rank12,Rank13,Rank14,Rank15,Rank16,Rank17,Rank18,Rank19,Rank20 " + 
                    "from Vectors Where VectorNum=" + VectorNum +
                    " and PositionNum = " + positionNumber);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();

                result = (SQLiteDataReader)cmd.ExecuteReader();

                while (result.Read())
                {
                    for (int i = 0; i < DecisionMaker.NumberOfCandidates; i++)
                    {
                        ranks[i] = result.GetInt32(i);
                    }
                };
            }

            return ranks;
        }

        private static int GetIntFromConfig(string key)
        {
            string value;
            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand("Select Value from Configuration Where Key='" + key + "'");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();

                value = (string)cmd.ExecuteScalar();
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

        private static void SetIntToConfig(string key, int value)
        {
            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand("update Configuration set Value='" + value.ToString() + "' Where Key='" + key + "'");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}