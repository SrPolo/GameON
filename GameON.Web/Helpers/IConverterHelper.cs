using GameON.Web.Data.Entities;
using GameON.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Helpers
{
    public interface IConverterHelper
    {
       VideoGameEntity ToVideoGameEntity(VideoGameViewModel model, string path, bool isNew);
        VideoGameViewModel ToVideoGameViewModel(VideoGameEntity teamEntity);
    }
}
