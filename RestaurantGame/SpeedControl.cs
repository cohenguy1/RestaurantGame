using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        public int StartTimerInterval = 2000;
        public const int MinTimerInterval = 1000;
        public const int MaxTimerInterval = 3000;

        protected void btnFastBackwards_Click(object sender, EventArgs e)
        {
            Timer1.Enabled = false;

            int newTimerInterval = Math.Min(TimerInterval + 500, MaxTimerInterval);

            UpdateFastPlaySpeed(newTimerInterval);
        }

        protected void btnFastForward_Click(object sender, EventArgs e)
        {
            Timer1.Enabled = false;

            int newTimerInterval = Math.Max(TimerInterval - 500, MinTimerInterval);

            UpdateFastPlaySpeed(newTimerInterval);
        }

        private void UpdateFastPlaySpeed(int newTimerInterval)
        {
            TimerInterval = newTimerInterval;
            Timer1.Interval = newTimerInterval;

            btnFastBackwards.Enabled = (newTimerInterval != MaxTimerInterval);
            btnFastForward.Enabled = (newTimerInterval != MinTimerInterval);

            btnFastBackwards.ImageUrl = btnFastBackwards.Enabled ? "~/Images/fbButton.png" : "~/Images/fbButtonDisabled.png";
            btnFastForward.ImageUrl = btnFastForward.Enabled ? "~/Images/ffButton.png" : "~/Images/ffButtonDisabled.png";

            string speedRate = ((double)StartTimerInterval / newTimerInterval).ToString("0.0");
            LabelSpeed.Text = " Speed: x" + speedRate;

            SetGameState(GameState.Playing);
            TimerEnabled = true;
        }


        protected void btnPausePlay_Click(object sender, ImageClickEventArgs e)
        {
            if (GameState== GameState.Playing)
            {
                SetGameState(GameState.Paused);
            }
            else
            {
                SetGameState(GameState.Playing);
            }
        }

        protected void SetGameState(GameState gameState)
        {
            GameState = gameState;
            Timer1.Enabled = (gameState == GameState.Playing);

            if (gameState == GameState.Paused)
            {
                btnPausePlay.ImageUrl = "~/Images/playButton.png";
            }
            else
            {
                btnPausePlay.ImageUrl = "~/Images/pauseButton.png";
            }
        }

    }
}