using Client.BusinessLogic.Authorization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;

namespace Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public event Action<bool> ClientIsAuthorized;
        IAuthService _authService;

        public MainViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        private RelayCommand _checkAuthorizationCommand;
        public RelayCommand CheckAuthorizationCommand
        {
            get
            {
                if (_checkAuthorizationCommand == null)
                    _checkAuthorizationCommand = new RelayCommand(CheckAuthorization);

                return _checkAuthorizationCommand;
            }
        }

        private async void CheckAuthorization()
        {
            await _authService.CreateClient();

            ClientIsAuthorized?.Invoke(_authService.IsClientAuthorized());
        }
    }
}