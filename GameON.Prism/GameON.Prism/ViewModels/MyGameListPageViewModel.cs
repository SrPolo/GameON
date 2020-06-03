using GameON.Common.Models;
using Prism.Navigation;
using System.Collections.Generic;

namespace GameON.Prism.ViewModels
{
    public class MyGameListPageViewModel : ViewModelBase
    {
        private List<GameListResponse> _gameList;
        private bool _isRunning;

        public MyGameListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "My Game List";
        }

        public List<GameListResponse> GameList
        {
            get => _gameList;
            set => SetProperty(ref _gameList, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("gamelist"))
            {
                GameList = parameters.GetValue<List<GameListResponse>>("gamelist");
            }
        }


    }
}
