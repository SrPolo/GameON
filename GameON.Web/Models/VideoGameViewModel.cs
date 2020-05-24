using GameON.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameON.Web.Models
{
    public class VideoGameViewModel : VideoGameEntity
    {
        [Display(Name = "Cover")]
        public IFormFile CoverFile { get; set; }

        [Required(ErrorMessage = "You must select at least one developer")]
        [Display(Name = "Developer")]
        public List<int> DevelopersId { get; set; }

        [Required(ErrorMessage = "You must select at least one platform")]
        [Display(Name = "Platforms")]
        public List<int> PlatformsId { get; set; }

        [Required(ErrorMessage = "You must select at least one genre")]
        [Display(Name = "Genres")]
        public List<int> GenresId { get; set; }

        public IEnumerable<SelectListItem> DeveloperList { get; set; }

        public IEnumerable<SelectListItem> PlatformList { get; set; }

        public IEnumerable<SelectListItem> GenreList { get; set; }
    }
}
