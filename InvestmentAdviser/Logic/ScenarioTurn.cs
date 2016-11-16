using InvestmentAdviser.Enums;

namespace InvestmentAdviser.Logic
{
    public class ScenarioTurn
    {
        public ScenarioTurnEnum TurnEnum;

        public double Gain;

        public int? Profit;

        public ScenarioTurn(ScenarioTurnEnum turnEnum)
        {
            TurnEnum = turnEnum;
            Profit = null;
        }

        public string GetTurnTitle()
        {
            switch (TurnEnum)
            {
                case ScenarioTurnEnum.ScenarioTurn1:
                    return "Turn 1";
                case ScenarioTurnEnum.ScenarioTurn2:
                    return "Turn 2";
                case ScenarioTurnEnum.ScenarioTurn3:
                    return "Turn 3";
                case ScenarioTurnEnum.ScenarioTurn4:
                    return "Turn 4";
                case ScenarioTurnEnum.ScenarioTurn5:
                    return "Turn 5";
                case ScenarioTurnEnum.ScenarioTurn6:
                    return "Turn 6";
                case ScenarioTurnEnum.ScenarioTurn7:
                    return "Turn 7";
                case ScenarioTurnEnum.ScenarioTurn8:
                    return "Turn 8";
                case ScenarioTurnEnum.ScenarioTurn9:
                    return "Turn 9";
                case ScenarioTurnEnum.ScenarioTurn10:
                    return "Turn 10";
                default:
                    return string.Empty;
            }
        }
    }
}