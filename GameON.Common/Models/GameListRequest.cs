using GameON.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameON.Common.Models
{
    public class GameListRequest
    {
        [Required]
        public VgStatus status { get; set; }


        [Required]
        public int VideoGameId { get; set; }


        [Required]
        public Guid Userid { get; set; }


        [Required]
        public string CultureInfo { get; set; }
    }
}
