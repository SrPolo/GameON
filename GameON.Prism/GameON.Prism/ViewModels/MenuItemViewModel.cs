using GameON.Common.Helpers;
using GameON.Common.Models;
using Prism.Commands;
using Prism.Navigation;

namespace GameON.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;

        private DelegateCommand _selectMenuCommand;


        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {

            if (PageName == "LoginPage" && Settings.IsLogin)
            {
                Settings.IsLogin = false;
                Settings.User = null;
                Settings.Token = null;
            }

            if (IsLoginRequired && !Settings.IsLogin)
            {
                await _navigationService.NavigateAsync($"/GameONMasterDetailPage/NavigationPage/LoginPage");
            }
            else
            {
                await _navigationService.NavigateAsync($"/GameONMasterDetailPage/NavigationPage/{PageName}");
            }
        }

    }
}
