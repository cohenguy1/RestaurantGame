using RestaurantGame.Enums;

namespace RestaurantGame.Logic
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
                case RestaurantPosition.Waiter1:
                    return "Waiter 1";
                case RestaurantPosition.Waiter2:
                    return "Waiter 2";
                case RestaurantPosition.Waiter3:
                    return "Waiter 3";
                case RestaurantPosition.Waiter4:
                    return "Waiter 4";
                case RestaurantPosition.Waiter5:
                    return "Waiter 5";
                case RestaurantPosition.Waiter6:
                    return "Waiter 6";
                case RestaurantPosition.Waiter7:
                    return "Waiter 7";
                case RestaurantPosition.Waiter8:
                    return "Waiter 8";
                case RestaurantPosition.Waiter9:
                    return "Waiter 9";
                case RestaurantPosition.Waiter10:
                    return "Waiter 10";
                default:
                    return string.Empty;
            }
        }
    }
}