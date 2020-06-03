using GameON.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameON.Common.Models
{
    public class GameListResponse
    {
        public int Id { get; set; }

        public VgStatus status { get; set; }

        public VideoGameResponse VideoGame { get; set; }


        public UserResponse User { get; set; }
    }
}
