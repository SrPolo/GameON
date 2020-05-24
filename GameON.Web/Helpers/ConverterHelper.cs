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


    }
}
