using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPlatforms();
        IEnumerable<SelectListItem> GetComboDevelopers();
        IEnumerable<SelectListItem> GetComboGenres();

        IEnumerable<SelectListItem> GetComboVideoGames();

    }
}
