using GameON.Common.Models;
using GameON.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameON.Prism.ViewModels
{
    public class UserItemViewModel : UserResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectUserCommand;

        public UserItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectUserCommand => _selectUserCommand ?? (_selectUserCommand = new DelegateCommand(SelectUser));

        private async void SelectUser()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "user", this }
            };
            await _navigationService.NavigateAsync(nameof(UserProfilePage), parameters);
        }
    }
}
