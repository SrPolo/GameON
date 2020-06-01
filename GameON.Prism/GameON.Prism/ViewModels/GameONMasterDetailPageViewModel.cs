using GameON.Common.Models;
using GameON.Prism.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class GameONMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public GameONMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_search",
                    PageName = nameof(VideoGamesPage),
                    Title = "Explore"
                },
                new Menu
                {
                    Icon = "ic_format_list_bulleted", //Cambiar
                    PageName = nameof(GamingCentersPage),
                    Title = "Gaming centers"
                },
                new Menu
                {
                    Icon = "ic_rate_review", //Cambiar
                    PageName = nameof(UsersPage),
                    Title = "Search users"
                },
                new Menu
                {
                    Icon = "ic_rate_review",
                    PageName = nameof(MyReviewsPage),
                    Title = "My reviews"
                },
                new Menu
                {
                    Icon = "ic_person",
                    PageName = nameof(ModifyUserPage),
                    Title = "Modify User"
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = nameof(LoginPage),
                    Title = "Login"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }

    }
}
