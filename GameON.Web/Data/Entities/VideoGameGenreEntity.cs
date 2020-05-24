﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Data.Entities
{
    public class VideoGameGenreEntity
    {
        public int Id { get; set; }

        public VideoGameEntity VideoGame { get; set; }
        
        public GenreEntity Genre { get; set; }
    }
}
