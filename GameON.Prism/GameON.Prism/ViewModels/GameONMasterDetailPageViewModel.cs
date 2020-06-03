using GameON.Common.Models;
using GameON.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class GameONMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _myProfileCommand;

        public GameONMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public DelegateCommand MyProfileCommand => _myProfileCommand ?? (_myProfileCommand = new DelegateCommand(MyProfileAsync));


        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        private async void MyProfileAsync()
        {
            await _navigationService.NavigateAsync(nameof(UserProfilePage));
        }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_videogame_asset",
                    PageName = nameof(VideoGamesPage),
                    Title = "Videogames"
                },
                new Menu
                {
                    Icon = "ic_local_activity",
                    PageName = nameof(GamingCentersPage),
                    Title = "Gaming centers"
                },
                new Menu
                {
                    Icon = "ic_search",
                    PageName = nameof(UsersPage),
                    Title = "Search users"
                },
                new Menu
                {
                    Icon = "ic_person",
                    PageName = nameof(ModifyUserPage),
                    Title = "Modify User",
                    IsLoginRequired = true
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
