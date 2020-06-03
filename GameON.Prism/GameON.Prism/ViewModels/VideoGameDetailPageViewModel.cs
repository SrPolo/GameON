using GameON.Common.Enums;
using GameON.Common.Helpers;
using GameON.Common.Models;
using GameON.Common.Services;
using GameON.Prism.Helpers;
using GameON.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

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
        private bool _isOpen;
        private DelegateCommand _mySelectedItem;
        private DelegateCommand _addPlaying;
        private DelegateCommand _planToPlayCommand;
        private DelegateCommand _addPlayedCommand;



        public VideoGameDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = "VideoGame";
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand ReviewsCommand => _reviewCommand ?? (_reviewCommand = new DelegateCommand(ViewReviewsAsync));
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(RefreshAsync));
        public DelegateCommand AddPlayingCommand => _addPlaying ?? (_addPlaying = new DelegateCommand(AddToPlay));
        public DelegateCommand PlantoPlayCommand => _planToPlayCommand ?? (_planToPlayCommand = new DelegateCommand(PlantoPlay));
        public DelegateCommand AddPlayedCommand => _addPlayedCommand ?? (_addPlayedCommand = new DelegateCommand(AddPlayed));
        public DelegateCommand OpenPopupCommand => _mySelectedItem ?? (_mySelectedItem = new DelegateCommand(Open));



        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        public bool IsOpen
        {
            get => _isOpen;
            set => SetProperty(ref _isOpen, value);
        }

        public VideoGameResponse VideoGame
        {
            get => _videoGame;
            set => SetProperty(ref _videoGame, value);
        }
        public List<VGGenreResponse> Genres
        {
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
            if (parameters.GetValue<VideoGameResponse>("videogame") != null)
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

            await _navigationService.NavigateAsync(nameof(GameReviewsPage), parameters);
        }

        private async void AddToList(VgStatus status)
        {
            string url = App.Current.Resources["UrlAPI"].ToString();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                //IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            GameListRequest request = new GameListRequest
            {
                status = status,
                VideoGameId = VideoGame.Id,
                Userid = user.Id,
                CultureInfo = Languages.Culture
            };

            Response response = await _apiService.AddEditGameList(url, "/api", "/GameList", request, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
            }
        }


        private void AddToPlay()
        {
            AddToList(VgStatus.Playing);
        }

        private void PlantoPlay()
        {
            AddToList(VgStatus.PlantoPlay);
        }

        private void AddPlayed()
        {
            AddToList(VgStatus.Played);
        }

        private void Open()
        {
            IsOpen = true;
        }

    }
}
