using GameON.Common.Models;
using GameON.Common.Services;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class VideoGamesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<VideoGameItemViewModel> _videoGames;

        public VideoGamesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = "Video Games";
            _navigationService = navigationService;
            _apiService = apiService;
            LoadVideoGamesAsync();
        }

        public List<VideoGameItemViewModel> VideoGames
        {
            get => _videoGames;
            set => SetProperty(ref _videoGames, value);
        }

        private async void LoadVideoGamesAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetListAsync<VideoGameResponse>(url, "/api", "/videogames");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            List<VideoGameResponse> videoGames = (List<VideoGameResponse>)response.Result;
            VideoGames = videoGames.Select(v => new VideoGameItemViewModel(_navigationService)
            {
                Developers = v.Developers,
                Genres = v.Genres,
                Id = v.Id,
                Name = v.Name,
                PicturePath = v.PicturePath,
                Platforms = v.Platforms,
                ReleaseDate = v.ReleaseDate,
                Score = v.Score,
                Synopsis = v.Synopsis
            }).ToList();
        }
    }
}
