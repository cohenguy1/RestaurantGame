using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        private double CalculateAveragePosition(List<Position> positions)
        {
            return positions.Where(position => position.ChosenCandidate != null).Average(pos => pos.ChosenCandidate.CandidateRank);
        }

        private void UpdatePositionsTable(Position currentPosition, double avgRank)
        {
            var positionCell = GetPositionCell(currentPosition);
            positionCell.Text = "&nbsp;" + currentPosition.GetJobTitle() + ": " + currentPosition.ChosenCandidate.CandidateRank;
            positionCell.ForeColor = System.Drawing.Color.Blue;
            positionCell.Font.Italic = true;

            AvgRankCell.Text = "&nbsp;Average Rank: " + avgRank.ToString("0.00");
        }

        private void ClearPositionsTable()
        {
            var positions = (List<Position>)Session[PositionsStr];

            for (int positionIndex = 0; positionIndex < 10; positionIndex++)
            {
                var currentPosition = positions[positionIndex];

                currentPosition.ChosenCandidate = null;

                var positionCell = GetPositionCell(currentPosition);

                positionCell.Text = "&nbsp;" + currentPosition.GetJobTitle() + ": " ;
                positionCell.ForeColor = System.Drawing.Color.Black;
                positionCell.Font.Italic = false;
            }

            AvgRankCell.Text = "&nbsp;Average Rank: ";
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