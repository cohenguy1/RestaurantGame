using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantGame
{
    public class ImageHandler
    {
        public static void DrawCandidatesByNow(List<Candidate> candidatesByNow, int newCandidateIndex, Default page)
        {
            for (var candidateIndex = 0; candidateIndex < candidatesByNow.Count; candidateIndex++)
            {
                var stickManImage = page.GetStickManImage(candidateIndex + 1);

                var oldStickManImage = stickManImage.ImageUrl;
                string newStickManImage;

                if (candidateIndex == newCandidateIndex)
                {
                    newStickManImage = "~/Images/StickMan" + (newCandidateIndex + 1) + "Red.png";
                }
                else
                {
                    if ((candidateIndex == 0) || (candidateIndex == candidatesByNow.Count - 1))
                    {
                        newStickManImage = "~/Images/StickMan" + (candidateIndex + 1) + ".png";
                    }
                    else
                    {
                        newStickManImage = "~/Images/StickMan.png";
                    }
                }

                if (newStickManImage != oldStickManImage)
                {
                    stickManImage.ImageUrl = newStickManImage;
                    stickManImage.Visible = true;
                }
            }
        }
    }
}