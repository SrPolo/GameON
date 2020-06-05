using GameON.Common.Models;
using GameON.Common.Services;
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
    public class UsersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<UserItemViewModel> _users;
        private List<UserItemViewModel> usersList;
        private bool _isRunning;
        private bool _isRefreshing;
        private DelegateCommand _refreshCommand;
        private DelegateCommand _searchCommand;
        private string _filter;

        public UsersPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Users;
            LoadUsersAsync();
        }

        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(LoadUsersAsync));

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(Search));



        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public string Filter
        {
            get => _filter;
            set
            {
                SetProperty(ref _filter, value);
                Search();
            }
        }

        public List<UserItemViewModel> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private void Search()
        {
            IsRunning = true;
            Users = usersList.Where(v => v.FirstName.ToLower().Contains(Filter.ToLower())).OrderBy(v => v.FirstName).ToList();
            IsRunning = false;
        }

        private async void LoadUsersAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetListAsync<UserResponse>(url, "/api", "/UsersProfile");
            IsRunning = false;
            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }


            List<UserResponse> users = (List<UserResponse>)response.Result;
            usersList = users.Select(v => new UserItemViewModel(_navigationService)
            {
                FirstName = v.FirstName,
                LastName = v.LastName,
                PicturePath = v.PicturePath,
                VideoGame = v.VideoGame,
                Id = v.Id
            }).OrderBy(v => v.FirstName).ToList();
            Users = usersList;
        }
    }
}

