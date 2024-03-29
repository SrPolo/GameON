﻿using GameON.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Data.Entities
{
    public class GameListEntity
    {
        public int Id { get; set; }

        public VgStatus status { get; set; }

        public VideoGameEntity VideoGame { get; set; }

        public UserEntity User { get; set; }


    }
}
