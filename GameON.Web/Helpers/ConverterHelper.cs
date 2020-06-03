using GameON.Common.Models;
using GameON.Web.Data;
using GameON.Web.Data.Entities;
using GameON.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public UserResponse ToUserResponse(UserEntity user)
        {
            return new UserResponse
            {
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                UserType = user.UserType
            };
        }

        public UserResponse ToUserResponseProfile(UserEntity user)
        {
            UserResponse userResponse = new UserResponse
            {
                FirstName = user.FirstName,
                Id = user.Id,
                PicturePath = user.PicturePath,
                VideoGame = null
            };

            if (user.VideoGame != null)
            {
                userResponse.VideoGame = new VideoGameResponse
                {
                    Id = user.VideoGame.Id,
                    Name = user.VideoGame.Name,
                    PicturePath = user.VideoGame.PicturePath
                };
            }

            return userResponse;
        }

        public List<UserResponse> ToUserResponseProfile(List<UserEntity> userEntities)
        {
            List<UserResponse> list = new List<UserResponse>();
            foreach (UserEntity userEntity in userEntities)
            {
                list.Add(ToUserResponseProfile(userEntity));
            }

            return list;
        }

        public List<VideoGameResponse> ToVideoGameResponse(List<VideoGameEntity> videoGameEntities)
        {
            List<VideoGameResponse> list = new List<VideoGameResponse>();
            foreach (VideoGameEntity videoGameEntity in videoGameEntities)
            {
                list.Add(ToVideoGameResponse(videoGameEntity));
            }

            return list;
        }

        public VideoGameResponse ToVideoGameResponse(VideoGameEntity videoGameEntity)
        {
            return new VideoGameResponse
            {
                Id = videoGameEntity.Id,
                Name = videoGameEntity.Name,
                PicturePath = videoGameEntity.PicturePath,
                Score = videoGameEntity.Score,
                Synopsis = videoGameEntity.Synopsis,
                ReleaseDate = videoGameEntity.ReleaseDate,
                Developers = videoGameEntity.Developers.Select(d=> new VGDeveloperResponse
                { 
                    Id = d.Id,
                    Developer = new DeveloperResponse { 
                        Id = d.Developer.Id,
                        Name = d.Developer.Name
                    }
                }).ToList(),
                Genres = videoGameEntity.Genres.Select(d => new VGGenreResponse
                {
                    Id = d.Id,
                    Genre = new GenreResponse
                    {
                        Id = d.Genre.Id,
                        Genre = d.Genre.Genre
                    }
                }).ToList(),
                Platforms = videoGameEntity.Platforms.Select(d => new VGPlatformResponse
                {
                    Id = d.Id,
                    Platform = new PlatformResponse
                    {
                        Id = d.Platform.Id,
                        Name = d.Platform.Name
                    }
                }).ToList()
            };
        }

        public VideoGameEntity ToVideoGameEntity(VideoGameViewModel model, string path, bool isNew)
        {

            VideoGameEntity videogame = new VideoGameEntity
            {
                Id = isNew ? 0 : model.Id,
                PicturePath = path,
                Name = model.Name,
                ReleaseDate = model.ReleaseDate,
                Reviews = model.Reviews,
                Score = model.Score,
                Synopsis = model.Synopsis
            };            

            return videogame;
        }


        public VideoGameViewModel ToVideoGameViewModel(VideoGameEntity videoGameEntity)
        {
            return new VideoGameViewModel
            {
                Id = videoGameEntity.Id,
                PicturePath = videoGameEntity.PicturePath,
                Name = videoGameEntity.Name,
                Developers = videoGameEntity.Developers,
                ReleaseDate = videoGameEntity.ReleaseDate,
                Genres = videoGameEntity.Genres,
                Platforms = videoGameEntity.Platforms,
                Reviews = videoGameEntity.Reviews,
                Score = videoGameEntity.Score,
                Synopsis = videoGameEntity.Synopsis,
                DeveloperList = _combosHelper.GetComboDevelopers(),
                GenreList = _combosHelper.GetComboGenres(),
                PlatformList = _combosHelper.GetComboPlatforms(),
                
            };
        }

        public ReviewResponse ToReviewResponse(ReviewEntity reviewEntity)
        {
            return new ReviewResponse
            {
                Id = reviewEntity.Id,
                Review = reviewEntity.Review,
                VideoGame = new VideoGameResponse
                {
                    Id=reviewEntity.VideoGame.Id,
                    Name = reviewEntity.VideoGame.Name,
                    PicturePath = reviewEntity.VideoGame.PicturePath,
                    ReleaseDate =reviewEntity.VideoGame.ReleaseDate,
                    Score = reviewEntity.VideoGame.Score,
                    Synopsis= reviewEntity.VideoGame.Synopsis
                },
                Score = reviewEntity.Score,
                User = new UserResponse
                {
                    FirstName=reviewEntity.User.FirstName,
                    LastName = reviewEntity.User.LastName,
                    PicturePath = reviewEntity.User.PicturePath                    
                }
            };
        }

        public List<ReviewResponse> ToReviewResponse(List<ReviewEntity> reviewEntities)
        {
            List<ReviewResponse> list = new List<ReviewResponse>();
            foreach (ReviewEntity reviewEntity in reviewEntities)
            {
                list.Add(ToReviewResponse(reviewEntity));
            }

            return list;
        }

        public GameListResponse ToGameListResponse(GameListEntity gameListEntity)
        {
            return new GameListResponse
            {
                Id=gameListEntity.Id,
                status=gameListEntity.status,
                VideoGame = ToVideoGameResponse(gameListEntity.VideoGame),
                User = ToUserResponse(gameListEntity.User)                
            };
        }
    }
}
