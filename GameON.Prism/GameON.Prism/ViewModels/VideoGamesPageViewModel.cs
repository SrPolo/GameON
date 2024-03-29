﻿using GameON.Common.Models;
using GameON.Common.Services;
using GameON.Prism.Helpers;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class VideoGamesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<VideoGameItemViewModel> _videoGames;
        private List<VideoGameItemViewModel> videoGamesList;
        private bool _isRunning;
        private bool _isRefreshing;
        private DelegateCommand _refreshCommand;
        private DelegateCommand _searchCommand;
        private string _filter;

        public VideoGamesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = Languages.VideoGames;
            _navigationService = navigationService;
            _apiService = apiService;
            LoadVideoGamesAsync();
        }

        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(LoadVideoGamesAsync));

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(Search));

        

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public string Filter
        {
            get => _filter;
            set {
                SetProperty(ref _filter, value);
                Search();
            }
        }

        public List<VideoGameItemViewModel> VideoGames
        {
            get => _videoGames;
            set => SetProperty(ref _videoGames, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private void Search()
        {
            IsRunning = true;
            VideoGames = videoGamesList.Where(v => v.Name.ToLower().Contains(Filter.ToLower())).OrderBy(v => v.Name).ToList();
            IsRunning = false;
        }

        private async void LoadVideoGamesAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetListAsync<VideoGameResponse>(url, "/api", "/videogames");
            IsRunning = false;
            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            
            List<VideoGameResponse> videoGames = (List<VideoGameResponse>)response.Result;
            videoGamesList = videoGames.Select(v => new VideoGameItemViewModel(_navigationService)
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
            }).OrderBy(v=>v.Name).ToList();
            VideoGames = videoGamesList;
        }
    }
}
