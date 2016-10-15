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
            var positionPrizeCell = GetPrizePointsCell(currentPosition);
            positionPrizeCell.Text = "&nbsp;" + (110 - currentPosition.ChosenCandidate.CandidateRank * 10).ToString();

            var rankCell = GetRankCell(currentPosition);
            rankCell.Text = "&nbsp;#" + currentPosition.ChosenCandidate.CandidateRank;

            TotalPrizePointsCell.Text = "&nbsp;Total Prize Points: " + totalPrizePoints.ToString();
        }

        private void ClearPositionsTable()
        {
            for (int positionIndex = 0; positionIndex < 10; positionIndex++)
            {
                var currentPosition = GetPosition(positionIndex);

                currentPosition.ChosenCandidate = null;

                ClearTableRowStyle(positionIndex);
            }

            TotalPrizePointsCell.Text = "&nbsp;Total Prize Points: ";
        }

        private void ClearTableRowStyle(int positionIndex)
        {
            var position = GetPosition(positionIndex);
            ClearCellStyle(position, TableColumnType.Position);
            ClearCellStyle(position, TableColumnType.Rank);
            ClearCellStyle(position, TableColumnType.PrizePoints);
        }

        private void SetSeenTableRowStyle(int positionIndex)
        {
            var position = GetPosition(positionIndex);
            SetSeenCellStyle(position, TableColumnType.Position);
            SetSeenCellStyle(position, TableColumnType.Rank);
            SetSeenCellStyle(position, TableColumnType.PrizePoints);
        }

        private void SetTableRowStyle(int positionIndex)
        {
            var position = GetPosition(positionIndex);
            SetTableRowStyle(position, TableColumnType.Position);
            SetTableRowStyle(position, TableColumnType.Rank);
            SetTableRowStyle(position, TableColumnType.PrizePoints);
        }

        private void ClearCellStyle(Position currentPosition, TableColumnType position)
        {
            var tableCell = GetTableCell(currentPosition, position);

            if (position != TableColumnType.Position)
            {
                tableCell.Text = "";
            }
            tableCell.ForeColor = System.Drawing.Color.Black;
            tableCell.Font.Italic = false;
            tableCell.Font.Bold = false;
        }

        private void SetSeenCellStyle(Position currentPosition, TableColumnType position)
        {
            var tableCell = GetTableCell(currentPosition, position);
            tableCell.ForeColor = System.Drawing.Color.Blue;
            tableCell.Font.Bold = false;
            tableCell.Font.Italic = true;
        }

        private void SetTableRowStyle(Position currentPosition, TableColumnType position)
        {
            var tableCell = GetTableCell(currentPosition, position);
            tableCell.ForeColor = System.Drawing.Color.Green;
            tableCell.Font.Bold = true;
        }

        private TableCell GetTableCell(Position currentPosition, TableColumnType position)
        {
            switch (position)
            {
                case TableColumnType.Position:
                    return GetPositionCell(currentPosition);
                case TableColumnType.Rank:
                    return GetRankCell(currentPosition);
                case TableColumnType.PrizePoints:
                    return GetPrizePointsCell(currentPosition);
            }

            return null;
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

        private TableCell GetPositionCell(Position position)
        {
            switch (position.JobTitle)
            {
                case RestaurantPosition.Waiter1:
                    return Waiter1Cell;
                case RestaurantPosition.Waiter2:
                    return Waiter2Cell;
                case RestaurantPosition.Waiter3:
                    return Waiter3Cell;
                case RestaurantPosition.Waiter4:
                    return Waiter4Cell;
                case RestaurantPosition.Waiter5:
                    return Waiter5Cell;
                case RestaurantPosition.Waiter6:
                    return Waiter6Cell;
                case RestaurantPosition.Waiter7:
                    return Waiter7Cell;
                case RestaurantPosition.Waiter8:
                    return Waiter8Cell;
                case RestaurantPosition.Waiter9:
                    return Waiter9Cell;
                case RestaurantPosition.Waiter10:
                    return Waiter10Cell;
                default:
                    return null;
            }
        }

        private TableCell GetPrizePointsCell(Position position)
        {
            switch (position.JobTitle)
            {
                case RestaurantPosition.Waiter1:
                    return Waiter1PrizeCell;
                case RestaurantPosition.Waiter2:
                    return Waiter2PrizeCell;
                case RestaurantPosition.Waiter3:
                    return Waiter3PrizeCell;
                case RestaurantPosition.Waiter4:
                    return Waiter4PrizeCell;
                case RestaurantPosition.Waiter5:
                    return Waiter5PrizeCell;
                case RestaurantPosition.Waiter6:
                    return Waiter6PrizeCell;
                case RestaurantPosition.Waiter7:
                    return Waiter7PrizeCell;
                case RestaurantPosition.Waiter8:
                    return Waiter8PrizeCell;
                case RestaurantPosition.Waiter9:
                    return Waiter9PrizeCell;
                case RestaurantPosition.Waiter10:
                    return Waiter10PrizeCell;
                default:
                    return null;
            }
        }

        private TableCell GetRankCell(Position position)
        {
            switch (position.JobTitle)
            {
                case RestaurantPosition.Waiter1:
                    return Waiter1RankCell;
                case RestaurantPosition.Waiter2:
                    return Waiter2RankCell;
                case RestaurantPosition.Waiter3:
                    return Waiter3RankCell;
                case RestaurantPosition.Waiter4:
                    return Waiter4RankCell;
                case RestaurantPosition.Waiter5:
                    return Waiter5RankCell;
                case RestaurantPosition.Waiter6:
                    return Waiter6RankCell;
                case RestaurantPosition.Waiter7:
                    return Waiter7RankCell;
                case RestaurantPosition.Waiter8:
                    return Waiter8RankCell;
                case RestaurantPosition.Waiter9:
                    return Waiter9RankCell;
                case RestaurantPosition.Waiter10:
                    return Waiter10RankCell;
                default:
                    return null;
            }
        }
    }
}