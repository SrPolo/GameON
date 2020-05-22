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
        [MaxLength(300, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Synopsis { get; set; }


        [Display(Name = "Developer")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DeveloperEntity Developer { get; set; }


        [Display(Name = "Genres")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public ICollection<GenreEntity> Genres { get; set; }

        public ICollection<ReviewEntity> Reviews { get; set; }

        [Display(Name = "Platform")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public PlatformEntity Platform { get; set; }

        public float Score { get; set; }


        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
