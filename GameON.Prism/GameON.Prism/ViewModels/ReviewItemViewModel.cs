using GameON.Common.Models;
using GameON.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameON.Prism.ViewModels
{
    public class ReviewItemViewModel : ReviewResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectReviewCommand;

        public ReviewItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectReviewCommand => _selectReviewCommand ?? (_selectReviewCommand = new DelegateCommand(SelectReview));

        private async void SelectReview()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "review", this }
            };

            await _navigationService.NavigateAsync(nameof(ReviewDetailsPage), parameters);
        }
    }
}
