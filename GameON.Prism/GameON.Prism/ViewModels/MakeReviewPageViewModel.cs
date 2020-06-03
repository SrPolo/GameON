using GameON.Common.Models;
using GameON.Prism.Helpers;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace GameON.Prism.ViewModels
{
    public class MakeReviewPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private VideoGameResponse _videoGameResponse;
        private DelegateCommand _saveCommand;
        private DelegateCommand _cancelCommand;
        private string _review;
        private int _score;

        public MakeReviewPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Make a review";
            _navigationService = navigationService;
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
                //UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

                ReviewRequest reviewRequest = new ReviewRequest
                {
                    Score = Score,
                    Review = Review,
                    VideoGameId = VideoGame.Id,
                    //User= user
                };
                await App.Current.MainPage.DisplayAlert("Melo", reviewRequest.Score + "--" + reviewRequest.Review, "Aceptar");
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
