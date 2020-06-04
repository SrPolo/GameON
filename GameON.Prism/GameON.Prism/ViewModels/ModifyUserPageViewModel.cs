using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using GameON.Common.Helpers;
using GameON.Common.Models;
using GameON.Prism.Helpers;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using GameON.Common.Services;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using System;
using GameON.Prism.Views;

namespace GameON.Prism.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private readonly IFilesHelper _filesHelper;
        private bool _isRunning;
        private bool _isEnabled;
        private ImageSource _image;
        private UserResponse _user;
        private MediaFile _file;
        private VideoGameResponse _videoGame;
        private ObservableCollection<VideoGameResponse> _videoGames;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _changePasswordCommand;
        private DelegateCommand _saveCommand;

        public ModifyUserPageViewModel(INavigationService navigationService, IFilesHelper filesHelper, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _filesHelper = filesHelper;
            _apiService = apiService;
            Title = Languages.ModifyUser;
            IsEnabled = true;
            User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            Image = User.PictureFullPath;
            LoadTeamsAsync();
        }
        

        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ?? (_changePasswordCommand = new DelegateCommand(ChangePassword));

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public VideoGameResponse VideoGame
        {
            get => _videoGame;
            set => SetProperty(ref _videoGame, value);
        }

        public ObservableCollection<VideoGameResponse> VideoGames
        {
            get => _videoGames;
            set => SetProperty(ref _videoGames, value);
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

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void SaveAsync()
        {
            var isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            UserRequest userRequest = new UserRequest
            {
                Document = User.Document,
                Email = User.Email,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Password = "123456", 
                PictureArray = imageArray,
                VideogameId = VideoGame.Id,
                CultureInfo = Languages.Culture
            };

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.PutAsync(url, "/api", "/Account", userRequest, "bearer", token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Settings.User = JsonConvert.SerializeObject(User);
            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.UserUpdated, Languages.Accept);
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(User.Document))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DocumentError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.FirstName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.FirstNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(User.LastName))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

            return true;
        }

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.PictureSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.FromCamera);

            if (source == Languages.Cancel)
            {
                _file = null;
                return;
            }

            if (source == Languages.FromCamera)
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
            }
        }

        private async void LoadTeamsAsync()
        {
            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = true;
                IsEnabled = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            Response response = await _apiService.GetListAsync<VideoGameResponse>(url, "/api", "/VideoGames");
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<VideoGameResponse> list = (List<VideoGameResponse>)response.Result;
            VideoGames = new ObservableCollection<VideoGameResponse>(list.OrderBy(t => t.Name));
            VideoGame = VideoGames.FirstOrDefault(t => t.Id == User.VideoGame.Id);
        }


        private async void ChangePassword()
        {
            await _navigationService.NavigateAsync(nameof(ChangePasswordPage));
        }

    }


}
