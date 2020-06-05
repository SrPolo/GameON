using GameON.Common.Helpers;
using GameON.Common.Models;
using GameON.Common.Services;
using GameON.Prism.Helpers;
using GameON.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Prism.ViewModels
{
    public class UserProfilePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private UserResponse _user;
        private List<ReviewItemViewModel> _reviews;
        private DelegateCommand _revieCommand;
        private DelegateCommand _myGameListCommand;
        private bool _isRunning;

        public UserProfilePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = Languages.UserProfile;
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand MyGameListCommand => _myGameListCommand ?? (_myGameListCommand = new DelegateCommand(MyGameListAsync));
        public DelegateCommand ReviewCommand => _revieCommand ?? (_revieCommand = new DelegateCommand(ViewReviewAsync));

        public List<ReviewItemViewModel> Reviews
        {
            get => _reviews;
            set => SetProperty(ref _reviews, value);
        }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }



        private async void ViewReviewAsync()
        {
            //navegar a review detail
            throw new NotImplementedException();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetValue<UserResponse>("user") != null)
            {
                User = parameters.GetValue<UserResponse>("user");
                LoadReviews();
            }
        }

        private async void LoadReviews()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetListAsync<ReviewResponse>(url, "/api", $"/Review/GetUserReviews/{User.Id}");
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }


            List<ReviewResponse> reviews = (List<ReviewResponse>)response.Result;
            Reviews = reviews.Select(v => new ReviewItemViewModel(_navigationService)
            {
                Review = v.Review,
                Score = v.Score,
                User = v.User,
                VideoGame = v.VideoGame,
                Id = v.Id
            }).ToList();
        }

        private async void MyGameListAsync()
        {
            List<GameListResponse> gamelist = await LoadList();

            if (gamelist != null)
            {
                NavigationParameters parameters = new NavigationParameters
                {
                    { "gamelist", gamelist }
                };
                Settings.GameList = JsonConvert.SerializeObject(gamelist);
                await _navigationService.NavigateAsync(nameof(MyGameListPage), parameters);
            }
        }

        private async Task<List<GameListResponse>> LoadList()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();


            GameListForUserRequest request = new GameListForUserRequest
            {
                UserId = User.Id,
                CultureInfo = Languages.Culture
            };

            Response response = await _apiService.GetGameListForUser(url, "/api", $"/GameList/GetGameListForUser/", request);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return null;
            }


            return (List<GameListResponse>)response.Result;
        }
    }
}
