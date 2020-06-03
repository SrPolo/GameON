using System;
using System.Collections.Generic;
using System.Text;

namespace GameON.Common.Models
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public string Review { get; set; }

        public float Score { get; set; }

        public VideoGameResponse VideoGame { get; set; }

        public UserResponse User { get; set; }

    }
}
