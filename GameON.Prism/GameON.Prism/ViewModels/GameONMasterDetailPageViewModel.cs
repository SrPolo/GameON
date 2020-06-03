using GameON.Common.Helpers;
using GameON.Common.Models;
using GameON.Prism.Helpers;
using GameON.Prism.Views;
using Newtonsoft.Json;
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
        private UserResponse _user;

        public GameONMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();
        }

        public DelegateCommand MyProfileCommand => _myProfileCommand ?? (_myProfileCommand = new DelegateCommand(MyProfileAsync));

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private async void MyProfileAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "user", User }
            };
            await _navigationService.NavigateAsync($"/GameONMasterDetailPage/NavigationPage/{nameof(UserProfilePage)}", parameters);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_videogame_asset",
                    PageName = nameof(VideoGamesPage),
                    Title = Languages.VideoGamesLabel
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
                    Title = Languages.ModifyUser,
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = nameof(LoginPage),
                    Title = Settings.IsLogin ? Languages.Logout : Languages.Login
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
