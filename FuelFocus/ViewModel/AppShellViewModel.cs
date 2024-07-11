using CommunityToolkit.Mvvm.Input;
using FuelFocus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FuelFocus.ViewModel
{
    public partial class AppShellViewModel : BaseViewModel
    {

        public AppShellViewModel()
        {

        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        [RelayCommand]
        public async Task Logout()
        {
            await Shell.Current.GoToAsync("//LoginPage");
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
    }
}