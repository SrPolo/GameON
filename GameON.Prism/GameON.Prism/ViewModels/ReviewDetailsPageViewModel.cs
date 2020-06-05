using GameON.Common.Models;
using GameON.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class ReviewDetailsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ReviewResponse _review;
        private VideoGameResponse _videoGame;
        private UserResponse _user;

        public ReviewDetailsPageViewModel(INavigationService navigationService):base (navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.ReviewDetail;
        }

        public ReviewResponse Review
        {
            get => _review;
            set => SetProperty(ref _review, value);
        }

        public VideoGameResponse VideoGame
        {
            get => _videoGame;
            set => SetProperty(ref _videoGame, value);
        }
        public UserResponse User 
        { 
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetValue<ReviewResponse>("review") != null)
            {
                Review = parameters.GetValue<ReviewResponse>("review");
                VideoGame = Review.VideoGame;
                User = Review.User;
            }
        }


    }
}
