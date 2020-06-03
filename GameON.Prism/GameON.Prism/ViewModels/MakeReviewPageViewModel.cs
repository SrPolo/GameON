using GameON.Common.Helpers;
using GameON.Common.Models;
using GameON.Common.Services;
using GameON.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GameON.Prism.ViewModels
{
    public class MakeReviewPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private VideoGameResponse _videoGameResponse;
        private DelegateCommand _saveCommand;
        private DelegateCommand _cancelCommand;
        private string _review;
        private int _score;

        public MakeReviewPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = "Make a review";
            _navigationService = navigationService;
            _apiService = apiService;
            Score = 0;
        }

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(CancelAsync));

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveReviewAsync));


        public string Review
        {
            get => _review;
            set => SetProperty(ref _review, value);
        }

        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        public VideoGameResponse VideoGame
        {
            get => _videoGameResponse;
            set => SetProperty(ref _videoGameResponse, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            VideoGame = parameters.GetValue<VideoGameResponse>("videogame");
        }

        private async void CancelAsync()
        {
            await _navigationService.GoBackAsync();
        }

        private async void SaveReviewAsync()
        {
            if (await ValidateDataAsync())
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    //IsRunning = false;
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                    return;
                }

                string url = App.Current.Resources["UrlAPI"].ToString();
                UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

                ReviewRequest reviewRequest = new ReviewRequest
                {
                    Score = Score,
                    Review = Review,
                    VideoGameId = VideoGame.Id,
                    UserId = user.Id
                };

                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

                Response response = await _apiService.MakeReviewAsync(url, "/api", "/Review", reviewRequest, "bearer", token.Token);

                if (!response.IsSuccess)
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                }
            }
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (Score == 0)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ScoreError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Review))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ReviewError, Languages.Accept);
                return false;
            }

            return true;
        }

    }
}
