using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Default : System.Web.UI.Page
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

        private void ClearCandidateImages()
        {
            for (var candidateIndex = 0; candidateIndex < PositionCandidatesNumber; candidateIndex++)
            {
                var stickManImage = GetStickManImage(candidateIndex + 1);
                stickManImage.ImageUrl = null;
                stickManImage.Visible = false;
            }
        }

        private void ShowAllRemainingCandidatesImages()
        {
            for (var candidateIndex = 0; candidateIndex < PositionCandidatesNumber; candidateIndex++)
            {
                var remainingStickManImage = GetRemainingStickManImage(candidateIndex + 1);
                remainingStickManImage.ImageUrl = "~/Images/SmallStickMan.png";
                remainingStickManImage.Visible = true;
            }
        }


        private void ClearInterviewImages()
        {
            ImageManForward.Visible = false;
            ImageInterview.Visible = false;
        }

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

        private void ShowCandidateMap(Candidate chosenCandidate)
        {
            var currentCandidateNumber = CurrentCandidateNumber;
            var positionCandidates = PositionCandidates;

            var sortedInterviewedCandidates = positionCandidates.Where(candidate => candidate.CandidateNumber <= currentCandidateNumber)
                .OrderBy(viewedCandidate => viewedCandidate.CandidateRank);

            var sortedRemainingCandidates = positionCandidates.Where(candidate => candidate.CandidateNumber > currentCandidateNumber)
                .OrderBy(viewedCandidate => viewedCandidate.CandidateRank);

            for (var index = 0; index < sortedInterviewedCandidates.Count(); index++)
            {
                var stickManImage = GetStickManImage(index + 1);
                var candidate = sortedInterviewedCandidates.ElementAt(index);
                string imageUrl;

                if (candidate == chosenCandidate)
                {
                    imageUrl = GetStickManImageUrlByRank(candidate.CandidateRank, System.Drawing.Color.Red);
                }
                else
                {
                    imageUrl = GetStickManImageUrlByRank(candidate.CandidateRank, System.Drawing.Color.Black);
                }

                stickManImage.ImageUrl = imageUrl;
            }

            for (var index = 0; index < sortedRemainingCandidates.Count(); index++)
            {
                var stickManImage = GetRemainingStickManImage(index + 1);
                string imageUrl = GetStickManImageUrlByRank(sortedRemainingCandidates.ElementAt(index).CandidateRank, System.Drawing.Color.Black);
                stickManImage.ImageUrl = imageUrl;
            }
        }

        private string GetStickManImageUrlByRank(int candidateRank, System.Drawing.Color color)
        {
            string imageColor = (color == System.Drawing.Color.Red) ? "Red" : "";
            return "~/Images/StickMan" + candidateRank + imageColor + ".png";
        }

        private void RestoreButtonSizes(ImageButton thumbsBtn1, ImageButton thumbsBtn2)
        {
            thumbsBtn1.Height = Unit.Pixel(48);
            thumbsBtn1.Width = Unit.Pixel(48);

            thumbsBtn2.Height = Unit.Pixel(48);
            thumbsBtn2.Width = Unit.Pixel(48);
        }

        private void IncreaseButtonSize(ImageButton thumbsBtn)
        {
            thumbsBtn.Height = Unit.Pixel(80);
            thumbsBtn.Width = Unit.Pixel(80);
        }
    }
}