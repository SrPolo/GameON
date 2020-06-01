using GameON.Common.Models;
using GameON.Common.Services;
using GameON.Prism.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class VideoGameDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private VideoGameResponse _videoGame;
        private List<VGGenreResponse> _genres;
        private List<VGPlatformResponse> _platforms;
        private List<VGDeveloperResponse> _developers;
        private DelegateCommand _reviewCommand;
        private DelegateCommand _refreshCommand;
        private bool _isRefreshing;

        public VideoGameDetailPageViewModel(INavigationService navigationService,IApiService apiService):base (navigationService)
        {
            Title = "VideoGame";
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand ReviewsCommand => _reviewCommand ?? (_reviewCommand = new DelegateCommand(ViewReviewsAsync));
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(RefreshAsync));

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }


        public VideoGameResponse VideoGame
        {
            get => _videoGame;
            set => SetProperty(ref _videoGame, value);
        }
        public List<VGGenreResponse> Genres { 
            get => _genres; 
            set => SetProperty(ref _genres, value);
        }

        public List<VGPlatformResponse> Platforms
        {
            get => _platforms;
            set => SetProperty(ref _platforms, value);
        }

        public List<VGDeveloperResponse> Developers
        {
            get => _developers;
            set => SetProperty(ref _developers, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetValue<VideoGameResponse>("videogame")!=null)
            {
                VideoGame = parameters.GetValue<VideoGameResponse>("videogame");
                Title = VideoGame.Name;
                Genres = VideoGame.Genres.ToList();
                Platforms = VideoGame.Platforms.ToList();
                Developers = VideoGame.Developers.ToList();
            }
        }

        private async void RefreshAsync()
        {
            IsRefreshing = true;
            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetVideoGame(url, "/api", $"/videogames/{VideoGame.Id}");
            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }


            VideoGame = (VideoGameResponse)response.Result;
        }

        private async void ViewReviewsAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "videogame", VideoGame }
            };

            await _navigationService.NavigateAsync(nameof(GameReviewsPage),parameters);
        }



    }
}
