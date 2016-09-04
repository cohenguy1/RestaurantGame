using RestaurantGame.Enums;
using RestaurantGame.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Game : System.Web.UI.Page
    {

        private void IncreaseCurrentPosition()
        {
            CurrentPositionNumber++;
        }

        private void UpdatePositionsTable(Position currentPosition, int totalPrizePoints)
        {
            var positionCell = GetPositionCell(currentPosition);
            positionCell.Text = "&nbsp;" + currentPosition.GetJobTitle() + ": #" + currentPosition.ChosenCandidate.CandidateRank;

            var positionPrizeCell = GetPositionPrizeCell(currentPosition);
            positionPrizeCell.Text = "&nbsp;" + (110 - currentPosition.ChosenCandidate.CandidateRank * 10).ToString();

            TotalPrizePointsCell.Text = "&nbsp;Total Prize Points: " + totalPrizePoints.ToString();
        }

        private void ClearPositionsTable()
        {
            for (int positionIndex = 0; positionIndex < 10; positionIndex++)
            {
                var currentPosition = GetPosition(positionIndex);

                currentPosition.ChosenCandidate = null;

                var positionCell = GetPositionCell(currentPosition);
                var positionPrizeCell = GetPositionPrizeCell(currentPosition);
                positionCell.Text = "&nbsp;" + currentPosition.GetJobTitle() + ": " ;
                positionCell.ForeColor = System.Drawing.Color.Black;
                positionCell.Font.Italic = false;
                positionCell.Font.Bold = false;

                positionPrizeCell.Text = "";
                positionPrizeCell.ForeColor = System.Drawing.Color.Black;
                positionPrizeCell.Font.Italic = false;
                positionPrizeCell.Font.Bold = false;

            }

            TotalPrizePointsCell.Text = "&nbsp;Total Prize Points: ";
        }

        private Position GetPosition(int indexPosition)
        {
            return Positions[indexPosition];
        }

        private Position GetCurrentPosition()
        {
            return GetPosition(CurrentPositionNumber);
        }

        private string GetCurrentJobTitle()
        {
            var currentPosition = GetCurrentPosition();
            return currentPosition.GetJobTitle();
        }

        private TableCell GetPositionCell(int positionIndex)
        {
            var position = GetPosition(positionIndex);
            return GetPositionCell(position);
        }

        private TableCell GetPositionPrizeCell(int positionIndex)
        {
            var position = GetPosition(positionIndex);
            return GetPositionPrizeCell(position);
        }

        private TableCell GetCurrentPositionCell()
        {
            var currentPosition = GetCurrentPosition();
            return GetPositionCell(currentPosition);
        }

        private TableCell GetCurrentPositionPrizeCell()
        {
            var currentPosition = GetCurrentPosition();
            return GetPositionPrizeCell(currentPosition);
        }

        private TableCell GetPositionCell(Position position)
        {
            switch (position.JobTitle)
            {
                case RestaurantPosition.Manager:
                    return ManagerCell;
                case RestaurantPosition.HeadChef:
                    return HeadChefCell;
                case RestaurantPosition.Cook:
                    return CookCell;
                case RestaurantPosition.Baker:
                    return BakerCell;
                case RestaurantPosition.Waiter1:
                    return Waiter1Cell;
                case RestaurantPosition.Waiter2:
                    return Waiter2Cell;
                case RestaurantPosition.Waiter3:
                    return Waiter3Cell;
                case RestaurantPosition.Host:
                    return HostCell;
                case RestaurantPosition.Bartender:
                    return BartenderCell;
                case RestaurantPosition.Dishwasher:
                    return DishwasherCell;
                default:
                    return null;
            }
        }

        private TableCell GetPositionPrizeCell(Position position)
        {
            switch (position.JobTitle)
            {
                case RestaurantPosition.Manager:
                    return ManagerPrizeCell;
                case RestaurantPosition.HeadChef:
                    return HeadChefPrizeCell;
                case RestaurantPosition.Cook:
                    return CookPrizeCell;
                case RestaurantPosition.Baker:
                    return BakerPrizeCell;
                case RestaurantPosition.Waiter1:
                    return Waiter1PrizeCell;
                case RestaurantPosition.Waiter2:
                    return Waiter2PrizeCell;
                case RestaurantPosition.Waiter3:
                    return Waiter3PrizeCell;
                case RestaurantPosition.Host:
                    return HostPrizeCell;
                case RestaurantPosition.Bartender:
                    return BartenderPrizeCell;
                case RestaurantPosition.Dishwasher:
                    return DishwasherPrizeCell;
                default:
                    return null;
            }
        }
    }
}