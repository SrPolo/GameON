using GameON.Common.Models;
using GameON.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameON.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;

        private DelegateCommand _selectMenuCommand;


        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            if (PageName == nameof(MyGameListPage))
            {
                await _navigationService.NavigateAsync(nameof(MyGameListPage));
            }

            await _navigationService.NavigateAsync($"/GameONMasterDetailPage/NavigationPage/{PageName}");
        }

    }
}
