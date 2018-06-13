using Client.BusinessLogic.Authorization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class AuthViewModel : ViewModelBase
    {

        public event Action AuthIsStarted;
        public event Action CodeIsSended;
        public event Action AuthIsSuccessful;
        public event Action<string> ErrorHasOccurred;
        IAuthService _authSerivce;

        private string _phoneNumber;
        private string _code;

        public AuthViewModel(IAuthService authService)
        {
            _authSerivce = authService;

            _authSerivce.ErrorOccurder += ShowError;
        }
        public string PhoneNumber {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged();
            }
        }

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _startCommand;
        public RelayCommand StartCommand
        {
            get
            {
                if (_startCommand == null)
                    _startCommand = new RelayCommand(StartAuth);

                return _startCommand;
            }
        }

        private RelayCommand _sendCodeCommand;
        public RelayCommand SendCodeCommand
        {
            get
            {
                if (_sendCodeCommand == null)
                    _sendCodeCommand = new RelayCommand(SendCode);

                return _sendCodeCommand;

            }
        }

        private RelayCommand _checkCodeCommand;
        public RelayCommand CheckCodeCommand
        {
            get
            {
                if (_checkCodeCommand == null)
                    _checkCodeCommand = new RelayCommand(CheckCode);

                return _checkCodeCommand;
            }
        }

        private async void StartAuth()
        {
            await _authSerivce.CreateClient();

            AuthIsStarted?.Invoke();
        }

        private async void SendCode()
        {
            await _authSerivce.SendCode(PhoneNumber);

            CodeIsSended?.Invoke();
        }

        private async void CheckCode()
        {
            await _authSerivce.SignUp(PhoneNumber, Code);
            AuthIsSuccessful?.Invoke();
        }

        private void ShowError(string error)
        {
            ErrorHasOccurred?.Invoke(error);
        }
    }
}
