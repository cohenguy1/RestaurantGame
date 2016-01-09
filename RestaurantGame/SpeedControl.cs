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
            Session[TimerEnabled] = Timer1.Enabled;
            Timer1.Enabled = false;

            int newTimerInterval = Math.Min((int)Session[TimerInterval] + 500, MaxTimerInterval);
            Session[TimerInterval] = newTimerInterval;

            UpdateFastPlaySpeed(newTimerInterval);

            EnableDisableTimer(true);
        }

        protected void btnFastForward_Click(object sender, EventArgs e)
        {
            Session[TimerEnabled] = Timer1.Enabled;
            Timer1.Enabled = false;

            int newTimerInterval = Math.Max((int)Session[TimerInterval] - 500, MinTimerInterval);
            Session[TimerInterval] = newTimerInterval;

            UpdateFastPlaySpeed(newTimerInterval);

            EnableDisableTimer(true);
        }

        private void UpdateFastPlaySpeed(int newTimerInterval)
        {
            Timer1.Interval = newTimerInterval;

            btnFastBackwards.Enabled = (newTimerInterval != MaxTimerInterval);
            btnFastForward.Enabled = (newTimerInterval != MinTimerInterval);

            btnFastBackwards.ImageUrl = btnFastBackwards.Enabled ? "~/Images/fbButton.png" : "~/Images/fbButtonDisabled.png";
            btnFastForward.ImageUrl = btnFastForward.Enabled ? "~/Images/ffButton.png" : "~/Images/ffButtonDisabled.png";

            string speedRate = ((double)StartTimerInterval / newTimerInterval).ToString("0.0");
            LabelSpeed.Text = " Speed: x" + speedRate;
        }


        protected void btnPausePlay_Click(object sender, ImageClickEventArgs e)
        {
            if ((GameState)Session[GameStateStr] == GameState.Playing)
            {
                Session[GameStateStr] = GameState.Paused;

                Timer1.Enabled = false;
                btnPausePlay.ImageUrl = "~/Images/playButton.png";
            }
            else if ((GameState)Session[GameStateStr] == GameState.Paused)
            {
                Session[GameStateStr] = GameState.Playing;

                Timer1.Enabled = true;
                btnPausePlay.ImageUrl = "~/Images/pauseButton.png";
            }
        }

    }
}