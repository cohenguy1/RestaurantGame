using RestaurantGame.Enums;
using RestaurantGame.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantGame
{
    public partial class Game : System.Web.UI.Page
    {
        private void UpdateImages(CandidateState candidateState)
        {
            ImageInterview.Visible = (candidateState == CandidateState.Interview || candidateState == CandidateState.PostInterview);
            LabelInterviewing.Visible = (candidateState == CandidateState.Interview || candidateState == CandidateState.PostInterview); ;
            MovingToNextPositionLabel.Visible = false;
            MovingJobTitleLabel.Visible = false;
        }

        private void ClearInterviewImages()
        {
            ImageInterview.Visible = false;
            LabelInterviewing.Visible = false;
        }
    }
}