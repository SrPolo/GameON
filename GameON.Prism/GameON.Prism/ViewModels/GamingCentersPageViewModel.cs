using GameON.Prism.Helpers;
using Prism.Navigation;

namespace GameON.Prism.ViewModels
{
    public class GamingCentersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public GamingCentersPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.GameCenters;
        }
    }
}
