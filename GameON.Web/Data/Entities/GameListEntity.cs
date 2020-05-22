using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Data.Entities
{
    public class GameListEntity
    {
        public int Id { get; set; }

        public ICollection<VideoGameEntity> Playing { get; set; }

        public ICollection<VideoGameEntity> Played { get; set; }

        public ICollection<VideoGameEntity> PlantoPlay { get; set; }
    }
}
