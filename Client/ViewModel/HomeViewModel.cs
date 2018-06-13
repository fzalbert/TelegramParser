using Client.BusinessLogic.Message;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private IMessageService _messageService;

        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;

            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        public HomeViewModel(IMessageService messageService)
        {
            _messageService = messageService;
        }

        private RelayCommand _listeningUpdatesCommand;
        public RelayCommand ListeningUpdatesCommand
        {
            get
            {
                if (_listeningUpdatesCommand == null)
                    _listeningUpdatesCommand = new RelayCommand(StartListening);

                return _listeningUpdatesCommand;
            }
        }

        private async void StartListening()
        {
            _messageService.FindClient(UserName);

            _messageService.StartListenUpdate();
        }
    }
}
