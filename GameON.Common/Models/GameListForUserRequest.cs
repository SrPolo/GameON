using System;
using System.ComponentModel.DataAnnotations;

namespace GameON.Common.Models
{
    public class GameListForUserRequest
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid UserId { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
