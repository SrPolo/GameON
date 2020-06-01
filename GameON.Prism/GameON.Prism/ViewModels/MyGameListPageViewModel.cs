using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class MyGameListPageViewModel : ViewModelBase
    {
        public MyGameListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "My Game List";
        }
    }
}
