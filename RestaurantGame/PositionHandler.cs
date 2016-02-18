using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        private int GetCurrentPositionNumber()
        {
            return PositionToFill;
        }

        private void SetCurrentPositionNumber(int positionNumber)
        {
            PositionToFill = positionNumber;
        }

        private void GeneratePositions()
        {
            var positions = new List<Position>();

            positions.Add(new Position(RestaurantPosition.Manager));
            positions.Add(new Position(RestaurantPosition.HeadChef));
            positions.Add(new Position(RestaurantPosition.Cook));
            positions.Add(new Position(RestaurantPosition.Baker));
            positions.Add(new Position(RestaurantPosition.Dishwasher));
            positions.Add(new Position(RestaurantPosition.Waiter1));
            positions.Add(new Position(RestaurantPosition.Waiter2));
            positions.Add(new Position(RestaurantPosition.Waiter3));
            positions.Add(new Position(RestaurantPosition.Host));
            positions.Add(new Position(RestaurantPosition.Bartender));

            Positions = positions;

            var acceptedCandidates = new int[positions.Count];
            AcceptedCandidates = acceptedCandidates;
        }

        private void IncreaseCurrentPosition()
        {
            SetCurrentPositionNumber(PositionToFill + 1);
        }

        private double CalculateAveragePosition()
        {
            return Positions.Where(position => position.ChosenCandidate != null).Average(pos => pos.ChosenCandidate.CandidateRank);
        }

        private void UpdatePositionsTable(Position currentPosition, double avgRank)
        {
            var positionCell = GetPositionCell(currentPosition);
            positionCell.Text = "&nbsp;" + currentPosition.GetJobTitle() + ": " + currentPosition.ChosenCandidate.CandidateRank;

            AvgRankCell.Text = "&nbsp;Average Rank: " + avgRank.ToString("0.00");
        }

        private void ClearPositionsTable()
        {
            for (int positionIndex = 0; positionIndex < 10; positionIndex++)
            {
                var currentPosition = GetPosition(positionIndex);

                currentPosition.ChosenCandidate = null;

                var positionCell = GetPositionCell(currentPosition);

                positionCell.Text = "&nbsp;" + currentPosition.GetJobTitle() + ": " ;
                positionCell.ForeColor = System.Drawing.Color.Black;
                positionCell.Font.Italic = false;
                positionCell.Font.Bold = false;
            }

            AvgRankCell.Text = "&nbsp;Average Rank: ";
        }

        private Position GetPosition(int indexPosition)
        {
            return Positions[indexPosition];
        }

        private Position GetCurrentPosition()
        {
            return GetPosition(PositionToFill);
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

        private TableCell GetCurrentPositionCell()
        {
            var currentPosition = GetCurrentPosition();
            return GetPositionCell(currentPosition);
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

    }
}