using GameON.Common.Enums;
using GameON.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class PlantoPlayGamesPageViewModel : ViewModelBase
    {
        private List<VideoGameResponse> _videoGames;

        public PlantoPlayGamesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Playing";
        }

        public List<VideoGameResponse> VideoGames
        {
            get => _videoGames;
            set => SetProperty(ref _videoGames, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("gamelist"))
            {
                List<GameListResponse> _gameList = parameters.GetValue<List<GameListResponse>>("gamelist");

                List<GameListResponse> mylist = _gameList.Where(g => g.status == VgStatus.PlantoPlay).ToList();
                VideoGames = mylist.Select(m => m.VideoGame).ToList();
            }
        }
    }
}
