using GameON.Common.Enums;
using GameON.Common.Models;
using GameON.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GameON.Prism.ViewModels
{
    public class PlayingGamesPageViewModel : ViewModelBase
    {

        private List<VideoGameResponse> _videoGames;

        public PlayingGamesPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = Languages.Playing;
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

                List<GameListResponse> mylist = _gameList.Where(g => g.status == VgStatus.Playing).ToList();
                VideoGames = mylist.Select(m=>m.VideoGame).ToList();
                /*foreach (GameListResponse game in mylist)
                {
                    VideoGames.Add(game.VideoGame);
                }*/
            }
        }

    }
}
