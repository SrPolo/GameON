using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class RecoverPasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _goBackCommand;

        public RecoverPasswordPageViewModel(INavigationService navigationService):base(navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(GoBackAsync));

        private async void GoBackAsync()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
