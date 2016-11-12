using InvestmentAdviser.Enums;
using InvestmentAdviser.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace InvestmentAdviser
{
    public partial class Game : System.Web.UI.Page
    {

        private void IncreaseCurrentPosition()
        {
            CurrentTurnNumber++;
        }

        private void UpdateTurnsTable(ScenarioTurn currentTurn, int totalPrizePoints)
        {
            var positionPrizeCell = GetPrizePointsCell(currentTurn);
            positionPrizeCell.Text = "&nbsp;" + (110 - currentTurn.Profit * 10).ToString();

            var rankCell = GetRankCell(currentTurn);
            rankCell.Text = "&nbsp;#" + currentTurn.Profit;

            TotalPrizePointsCell.Text = "&nbsp;Total Prize Points: " + totalPrizePoints.ToString();
        }

        private void ClearTurnTable()
        {
            for (int turnIndex = 0; turnIndex < 10; turnIndex++)
            {
                var currentTurn = GetScenarioTurn(turnIndex);

                currentTurn.Profit = null;

                ClearTableRowStyle(turnIndex);
            }

            TotalPrizePointsCell.Text = "&nbsp;Total Prize Points: ";
        }

        private void ClearTableRowStyle(int turnIndex)
        {
            var position = GetScenarioTurn(turnIndex);
            ClearCellStyle(position, TableColumnType.Turn);
            ClearCellStyle(position, TableColumnType.Gain);
            ClearCellStyle(position, TableColumnType.PrizePoints);
        }

        private void SetSeenTableRowStyle(int turnIndex)
        {
            var position = GetScenarioTurn(turnIndex);
            SetSeenCellStyle(position, TableColumnType.Turn);
            SetSeenCellStyle(position, TableColumnType.Gain);
            SetSeenCellStyle(position, TableColumnType.PrizePoints);
        }

        private void SetTableRowStyle(int turnIndex)
        {
            var position = GetScenarioTurn(turnIndex);
            SetTableRowStyle(position, TableColumnType.Turn);
            SetTableRowStyle(position, TableColumnType.Gain);
            SetTableRowStyle(position, TableColumnType.PrizePoints);
        }

        private void ClearCellStyle(ScenarioTurn currentTurn, TableColumnType position)
        {
            var tableCell = GetTableCell(currentTurn, position);

            if (position != TableColumnType.Turn)
            {
                tableCell.Text = "";
            }
            tableCell.ForeColor = System.Drawing.Color.Black;
            tableCell.Font.Italic = false;
            tableCell.Font.Bold = false;
        }

        private void SetSeenCellStyle(ScenarioTurn currentTurn, TableColumnType columnType)
        {
            var tableCell = GetTableCell(currentTurn, columnType);
            tableCell.ForeColor = System.Drawing.Color.Blue;
            tableCell.Font.Bold = false;
            tableCell.Font.Italic = true;
        }

        private void SetTableRowStyle(ScenarioTurn currentTurn, TableColumnType columnType)
        {
            var tableCell = GetTableCell(currentTurn, columnType);
            tableCell.ForeColor = System.Drawing.Color.Green;
            tableCell.Font.Bold = true;
        }

        private TableCell GetTableCell(ScenarioTurn currentTurn, TableColumnType columnType)
        {
            switch (columnType)
            {
                case TableColumnType.Turn:
                    return GetTurnCell(currentTurn);
                case TableColumnType.Gain:
                    return GetRankCell(currentTurn);
                case TableColumnType.PrizePoints:
                    return GetPrizePointsCell(currentTurn);
            }

            return null;
        }

        private ScenarioTurn GetScenarioTurn(int indexPosition)
        {
            return ScenarioTurns[indexPosition];
        }

        private ScenarioTurn GetCurrentTurn()
        {
            return GetScenarioTurn(CurrentTurnNumber);
        }

        private string GetCurrentJobTitle()
        {
            var currentTurn = GetCurrentTurn();
            return currentTurn.GetTurnTitle();
        }

        private TableCell GetTurnCell(ScenarioTurn scenarioTurn)
        {
            switch (scenarioTurn.TurnEnum)
            {
                case ScenarioTurnEnum.ScenarioTurn1:
                    return ScenarioTurn1Cell;
                case ScenarioTurnEnum.ScenarioTurn2:
                    return ScenarioTurn2Cell;
                case ScenarioTurnEnum.ScenarioTurn3:
                    return ScenarioTurn3Cell;
                case ScenarioTurnEnum.ScenarioTurn4:
                    return ScenarioTurn4Cell;
                case ScenarioTurnEnum.ScenarioTurn5:
                    return ScenarioTurn5Cell;
                case ScenarioTurnEnum.ScenarioTurn6:
                    return ScenarioTurn6Cell;
                case ScenarioTurnEnum.ScenarioTurn7:
                    return ScenarioTurn7Cell;
                case ScenarioTurnEnum.ScenarioTurn8:
                    return ScenarioTurn8Cell;
                case ScenarioTurnEnum.ScenarioTurn9:
                    return ScenarioTurn9Cell;
                case ScenarioTurnEnum.ScenarioTurn10:
                    return ScenarioTurn10Cell;
                default:
                    return null;
            }
        }

        private TableCell GetPrizePointsCell(ScenarioTurn turn)
        {
            switch (turn.TurnEnum)
            {
                case ScenarioTurnEnum.ScenarioTurn1:
                    return ScenarioTurn1PrizeCell;
                case ScenarioTurnEnum.ScenarioTurn2:
                    return ScenarioTurn2PrizeCell;
                case ScenarioTurnEnum.ScenarioTurn3:
                    return ScenarioTurn3PrizeCell;
                case ScenarioTurnEnum.ScenarioTurn4:
                    return ScenarioTurn4PrizeCell;
                case ScenarioTurnEnum.ScenarioTurn5:
                    return ScenarioTurn5PrizeCell;
                case ScenarioTurnEnum.ScenarioTurn6:
                    return ScenarioTurn6PrizeCell;
                case ScenarioTurnEnum.ScenarioTurn7:
                    return ScenarioTurn7PrizeCell;
                case ScenarioTurnEnum.ScenarioTurn8:
                    return ScenarioTurn8PrizeCell;
                case ScenarioTurnEnum.ScenarioTurn9:
                    return ScenarioTurn9PrizeCell;
                case ScenarioTurnEnum.ScenarioTurn10:
                    return ScenarioTurn10PrizeCell;
                default:
                    return null;
            }
        }

        private TableCell GetRankCell(ScenarioTurn turn)
        {
            switch (turn.TurnEnum)
            {
                case ScenarioTurnEnum.ScenarioTurn1:
                    return ScenarioTurn1RankCell;
                case ScenarioTurnEnum.ScenarioTurn2:
                    return ScenarioTurn2RankCell;
                case ScenarioTurnEnum.ScenarioTurn3:
                    return ScenarioTurn3RankCell;
                case ScenarioTurnEnum.ScenarioTurn4:
                    return ScenarioTurn4RankCell;
                case ScenarioTurnEnum.ScenarioTurn5:
                    return ScenarioTurn5RankCell;
                case ScenarioTurnEnum.ScenarioTurn6:
                    return ScenarioTurn6RankCell;
                case ScenarioTurnEnum.ScenarioTurn7:
                    return ScenarioTurn7RankCell;
                case ScenarioTurnEnum.ScenarioTurn8:
                    return ScenarioTurn8RankCell;
                case ScenarioTurnEnum.ScenarioTurn9:
                    return ScenarioTurn9RankCell;
                case ScenarioTurnEnum.ScenarioTurn10:
                    return ScenarioTurn10RankCell;
                default:
                    return null;
            }
        }
    }
}