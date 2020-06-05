using GameON.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class MyReviewsPageViewModel : ViewModelBase
    {
        public MyReviewsPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = Languages.MyReviews;
        }
    }
}
