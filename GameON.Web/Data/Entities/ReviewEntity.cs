using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Data.Entities
{
    public class ReviewEntity
    {
        public int Id { get; set; }


        [Display(Name = "Review")]
        [MaxLength(1500, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Review { get; set; }

        [Display(Name = "Score")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public float Score { get; set; }

        public VideoGameEntity VideoGame { get; set; }

        public UserEntity User { get; set; }
    }
}
