using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class GamingCentersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public GamingCentersPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Game Centers";
        }
    }
}
