using GameON.Web.Data.Entities;
using GameON.Web.Models;
using GameON.Common.Models;
using System.Collections.Generic;

namespace GameON.Web.Helpers
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(UserEntity userEntity);

        VideoGameEntity ToVideoGameEntity(VideoGameViewModel model, string path, bool isNew);

        VideoGameViewModel ToVideoGameViewModel(VideoGameEntity videoGameEntity);

        VideoGameResponse ToVideoGameResponse(VideoGameEntity videoGameEntity);

        List<VideoGameResponse> ToVideoGameResponse(List<VideoGameEntity> videoGameEntities);

        ReviewResponse ToReviewResponse(ReviewEntity reviewEntity);

        List<ReviewResponse> ToReviewResponse(List<ReviewEntity> reviewEntities);

    }
}
