using GameON.Common.Models;
using GameON.Common.Services;
using GameON.Prism.Views;
using ImTools;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GameON.Prism.ViewModels
{
    public class GameReviewsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isRefreshing;
        private DelegateCommand _refreshCommand;
        private DelegateCommand _makeReviewCommand;
        VideoGameResponse _videoGame;
        private List<ReviewItemViewModel> _reviews;

        public GameReviewsPageViewModel(INavigationService navigationService,IApiService apiService) : base(navigationService)
        {
            Title = "Reviews";
            _navigationService = navigationService;
            _apiService = apiService;            
        }

        public DelegateCommand MakeReviewCommand => _makeReviewCommand ?? (_makeReviewCommand = new DelegateCommand(MakeReviewAsync));
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(LoadReviewsAsync));

        public VideoGameResponse VideoGame
        {
            get => _videoGame;
            set => SetProperty(ref _videoGame, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public List<ReviewItemViewModel> Reviews
        {
            get => _reviews;
            set => SetProperty(ref _reviews, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetValue<VideoGameResponse>("videogame") != null)
            {
                VideoGame = parameters.GetValue<VideoGameResponse>("videogame");
                LoadReviewsAsync();
            }
        }

        private async void LoadReviewsAsync()
        {
            IsEnabled = false;
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetListAsync<ReviewResponse>(url, "/api", $"/Review/GetReviewsForGame/{VideoGame.Id}");
            IsRunning = false;
            IsEnabled = true;
            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }


            List<ReviewResponse> reviews = (List<ReviewResponse>)response.Result;
            Reviews = reviews.Select(v => new ReviewItemViewModel(_navigationService)
            {
                Review= v.Review,
                Score = v.Score,
                User = v.User,
                VideoGame = v.VideoGame,
                Id = v.Id
            }).ToList();
        }

        private async void MakeReviewAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "videogame", VideoGame }
            };

            await _navigationService.NavigateAsync(nameof(MakeReviewPage), parameters);
        }
    }
}
