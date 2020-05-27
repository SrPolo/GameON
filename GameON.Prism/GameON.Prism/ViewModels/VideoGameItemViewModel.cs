using GameON.Common.Models;
using GameON.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace GameON.Prism.ViewModels
{
    public class VideoGameItemViewModel : VideoGameResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectVideoGameCommand;

        public VideoGameItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectVideoGameCommand => _selectVideoGameCommand ?? (_selectVideoGameCommand = new DelegateCommand(SelectVideoGame));

        private async void SelectVideoGame()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "videogame", this }
            };
            await _navigationService.NavigateAsync(nameof(VideoGameDetailPage),parameters);
        }

    }
}
