using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame.Enums
{
    public enum GameState
    {
        Background,
        UserInfo,
        Instructions,
        TrainingStart,
        AfterTraining1,
        AfterTraining2,
        AfterTraining3,
        Quiz,
        GameStart,
        BeforeRate,
        Rate,
        AfterRate,
        EndGame,
        CollectedPrize
    }
}