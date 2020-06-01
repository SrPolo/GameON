using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Data.Entities
{
    public class VideoGameEntity
    {
        public int Id { get; set; }


        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Synopsis")]
        [MaxLength(1500, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Synopsis { get; set; }        

        [Display(Name = "Score")]
        [Range(1, 5, ErrorMessage = "The  {0} must be between 1-5.")]
        public float Score { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        [Display(Name = "Picture")]
        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
            ? "https://gameonweb.azurewebsites.net/images/noimage.png"
            : $"https://gameonweb.azurewebsites.net{PicturePath}";

        [Display(Name = "Release date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime ReleaseDate { get; set; }

        public ICollection<VideoGameDeveloperEntity> Developers { get; set; }

        public ICollection<VideoGameGenreEntity> Genres { get; set; }

        public ICollection<VideoGamePlatformEntity> Platforms { get; set; }

        public ICollection<ReviewEntity> Reviews { get; set; }

        public ICollection<UserEntity> Users { get; set; }

    }
}
