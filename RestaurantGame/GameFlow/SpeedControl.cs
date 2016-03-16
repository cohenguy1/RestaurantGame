using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {
        public int StartTimerInterval = 2000;
        public const int MinTimerInterval = 1000;
        public const int MaxTimerInterval = 3000;

        protected void btnFastBackwards_Click(object sender, EventArgs e)
        {
            TimerGame.Enabled = false;

            int newTimerInterval = Math.Min(TimerInterval + 500, MaxTimerInterval);

            UpdateFastPlaySpeed(newTimerInterval);
        }

        protected void btnFastForward_Click(object sender, EventArgs e)
        {
            TimerGame.Enabled = false;

            int newTimerInterval = Math.Max(TimerInterval - 500, MinTimerInterval);

            UpdateFastPlaySpeed(newTimerInterval);
        }

        private void UpdateFastPlaySpeed(int newTimerInterval)
        {
            TimerInterval = newTimerInterval;
            TimerGame.Interval = newTimerInterval;

            EnableDisableFBSpeedButton(newTimerInterval != MaxTimerInterval);
            EnableDisableFFSpeedButton(newTimerInterval != MinTimerInterval);

            string speedRate = ((double)StartTimerInterval / newTimerInterval).ToString("0.0");
            LabelSpeed.Text = " Speed: x" + speedRate;

            SetGameState(PlayPauseState.Playing);

            if (SessionState == Enums.SessionState.Running)
            {
                TimerEnabled = true;
            }
        }


        protected void btnPausePlay_Click(object sender, ImageClickEventArgs e)
        {
            if (GamePlayPauseState == PlayPauseState.Playing)
            {
                // pause
                SetGameState(PlayPauseState.Paused);

                // enable fast speed buttons
                EnableDisableFBSpeedButton(true);
                EnableDisableFFSpeedButton(true);
            }
            else // paused
            {
                SetGameState(PlayPauseState.Playing);
            }
        }

        private void EnableDisableFFSpeedButton(bool enable)
        {
            EnableDisableBtn(btnFastForward, enable);

            btnFastForward.ImageUrl = btnFastForward.Enabled ? "~/Images/ffButton.png" : "~/Images/ffButtonDisabled.png";
        }

        private void EnableDisableFBSpeedButton(bool enable)
        {
            EnableDisableBtn(btnFastBackwards, enable);

            btnFastBackwards.ImageUrl = btnFastBackwards.Enabled ? "~/Images/fbButton.png" : "~/Images/fbButtonDisabled.png";
        }

        protected void SetGameState(PlayPauseState gameState)
        {
            GamePlayPauseState = gameState;

            TimerGame.Enabled = (gameState == PlayPauseState.Playing) && (SessionState == Enums.SessionState.Running);

            if (gameState == PlayPauseState.Paused)
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