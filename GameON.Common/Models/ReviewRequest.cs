using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameON.Common.Models
{
    public class ReviewRequest
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid UserId { get; set; }

        [Display(Name ="Review")]
        [Required(ErrorMessage =("You must enter your {0}"))]
        [MaxLength(1500, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Review { get; set; }

        [Display(Name = "Score")]
        [Required(ErrorMessage = ("You must enter and {0}"))]
        public float Score { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int VideoGameId { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
