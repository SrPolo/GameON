﻿using GameON.Web.Data.Entities;
using GameON.Web.Models;
using GameON.Common.Models;
using System.Collections.Generic;

namespace GameON.Web.Helpers
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(UserEntity userEntity);

        UserResponse ToUserResponseProfile(UserEntity userEntity);

        List<UserResponse> ToUserResponseProfile(List<UserEntity> userEntities);

        VideoGameEntity ToVideoGameEntity(VideoGameViewModel model, string path, bool isNew);

        VideoGameViewModel ToVideoGameViewModel(VideoGameEntity videoGameEntity);

        VideoGameResponse ToVideoGameResponse(VideoGameEntity videoGameEntity);

        List<VideoGameResponse> ToVideoGameResponse(List<VideoGameEntity> videoGameEntities);

        ReviewResponse ToReviewResponse(ReviewEntity reviewEntity);

        GameListResponse ToGameListResponse(GameListEntity gameListEntity);

        List<ReviewResponse> ToReviewResponse(List<ReviewEntity> reviewEntities);

    }
}
