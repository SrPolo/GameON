using GameON.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;
        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboDevelopers()
        {
            List<SelectListItem> list = _context.Developers.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            }).OrderBy(t => t.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Developer...]",
                Value = "0"
            });
            list.FirstOrDefault().Disabled = true;
            return list;
        }

        public IEnumerable<SelectListItem> GetComboGenres()
        {
            List<SelectListItem> list = _context.Genres.Select(t => new SelectListItem
            {
                Text = t.Genre,
                Value = $"{t.Id}"
            }).OrderBy(t => t.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select genres...]",
                Value = "0"
            });
            list.FirstOrDefault().Disabled = true;
            return list;
        }

        public IEnumerable<SelectListItem> GetComboPlatforms()
        {
            List<SelectListItem> list = _context.Platforms.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            }).OrderBy(t => t.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select platforms...]",
                Value = "0"
            });
            list.FirstOrDefault().Disabled = true;
            return list;
        }

        public IEnumerable<SelectListItem> GetComboVideoGames()
        {
            List<SelectListItem> list = _context.VideoGames.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            }).OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select videogame...]",
                Value = "0"
            });
            list.FirstOrDefault().Disabled = true;
            return list;
        }
    }
}
