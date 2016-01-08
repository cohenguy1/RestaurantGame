using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
    {

        public Image GetStickManImage(int imageNum)
        {
            switch (imageNum)
            {
                case 1:
                    return StickMan1;
                case 2:
                    return StickMan2;
                case 3:
                    return StickMan3;
                case 4:
                    return StickMan4;
                case 5:
                    return StickMan5;
                case 6:
                    return StickMan6;
                case 7:
                    return StickMan7;
                case 8:
                    return StickMan8;
                case 9:
                    return StickMan9;
                case 10:
                    return StickMan10;
                case 11:
                    return StickMan11;
                case 12:
                    return StickMan12;
                case 13:
                    return StickMan13;
                case 14:
                    return StickMan14;
                case 15:
                    return StickMan15;
                case 16:
                    return StickMan16;
                case 17:
                    return StickMan17;
                case 18:
                    return StickMan18;
                case 19:
                    return StickMan19;
                case 20:
                    return StickMan20;
                default:
                    return StickMan1;
            }
        }

        public Image GetRemainingStickManImage(int imageNum)
        {
            switch (imageNum)
            {
                case 1:
                    return remainImage1;
                case 2:
                    return remainImage2;
                case 3:
                    return remainImage3;
                case 4:
                    return remainImage4;
                case 5:
                    return remainImage5;
                case 6:
                    return remainImage6;
                case 7:
                    return remainImage7;
                case 8:
                    return remainImage8;
                case 9:
                    return remainImage9;
                case 10:
                    return remainImage10;
                case 11:
                    return remainImage11;
                case 12:
                    return remainImage12;
                case 13:
                    return remainImage13;
                case 14:
                    return remainImage14;
                case 15:
                    return remainImage15;
                case 16:
                    return remainImage16;
                case 17:
                    return remainImage17;
                case 18:
                    return remainImage18;
                case 19:
                    return remainImage19;
                case 20:
                    return remainImage20;
                default:
                    return remainImage1;
            }
        }

    }
}