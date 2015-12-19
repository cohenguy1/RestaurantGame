using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public class Position
    {
        public RestaurantPosition JobTitle;

        public Candidate ChosenCandidate;

        public Position(RestaurantPosition restaurantPosition)
        {
            JobTitle = restaurantPosition;
        }

        public string GetJobTitle()
        {
            switch (JobTitle)
            {
                case RestaurantPosition.Manager:
                    return "Manager";
                case RestaurantPosition.HeadChef:
                    return "Head Chef";
                case RestaurantPosition.Cook:
                    return "Cook";
                case RestaurantPosition.Baker:
                    return "Baker";
                case RestaurantPosition.Dishwasher:
                    return "Dishwasher";
                case RestaurantPosition.Waiter1:
                    return "Waiter 1";
                case RestaurantPosition.Waiter2:
                    return "Waiter 2";
                case RestaurantPosition.Waiter3:
                    return "Waiter 3";
                case RestaurantPosition.Host:
                    return "Host";
                case RestaurantPosition.Bartender:
                    return "Bartender";
                default:
                    return string.Empty;
            }
        }
    }
}