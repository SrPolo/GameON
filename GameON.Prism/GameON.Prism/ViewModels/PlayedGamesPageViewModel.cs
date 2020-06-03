using GameON.Common.Enums;
using GameON.Common.Helpers;
using GameON.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace GameON.Prism.ViewModels
{
    public class PlayedGamesPageViewModel : ViewModelBase
    {
        private List<VideoGameResponse> _videoGames;

        public PlayedGamesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Played";
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
            List<GameListResponse> mylist = _gameList.Where(g => g.status == VgStatus.Played).ToList();
            VideoGames = mylist.Select(m => m.VideoGame).ToList();

        }

    }
}
