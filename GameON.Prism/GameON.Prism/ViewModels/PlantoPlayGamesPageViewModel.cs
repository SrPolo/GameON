using GameON.Common.Helpers;
using GameON.Common.Enums;
using GameON.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using GameON.Prism.Helpers;

namespace GameON.Prism.ViewModels
{
    public class PlantoPlayGamesPageViewModel : ViewModelBase
    {
        private List<VideoGameResponse> _videoGames;

        public PlantoPlayGamesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.VideoGamesLabel;
            LoadGames();
        }
        public List<VideoGameResponse> VideoGames
        {
            get => _videoGames;
            set => SetProperty(ref _videoGames, value);
        }


        private void LoadGames()
        {
            List<GameListResponse> _gameList = JsonConvert.DeserializeObject<List<GameListResponse>>(Settings.GameList);
            List<GameListResponse> mylist = _gameList.Where(g => g.status == VgStatus.PlantoPlay).ToList();
            VideoGames = mylist.Select(m => m.VideoGame).ToList();
        }

    }
}
