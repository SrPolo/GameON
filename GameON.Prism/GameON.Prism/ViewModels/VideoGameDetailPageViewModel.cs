using GameON.Common.Models;
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
        private VideoGameResponse _videoGame;
        private List<VGGenreResponse> _genres;
        private List<VGPlatformResponse> _platforms;
        private List<VGDeveloperResponse> _developers;

        public VideoGameDetailPageViewModel(INavigationService navigationService):base (navigationService)
        {
            Title = "VideoGame";
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
            VideoGame = parameters.GetValue<VideoGameResponse>("videogame");
            Title = _videoGame.Name;
            Genres = VideoGame.Genres.ToList();
            Platforms = VideoGame.Platforms.ToList();
            Developers = VideoGame.Developers.ToList();
        }

        
    }
}
