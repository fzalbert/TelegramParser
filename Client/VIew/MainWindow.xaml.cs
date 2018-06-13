using Client.Helpers;
using Client.ViewModel;
using OpenTl.Schema;
using OpenTl.Schema.Account;
using OpenTl.Schema.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TelegramClient.Core;
using TelegramClient.Core.Network.Exceptions;

namespace Client.VIew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ITelegramClient client;
        TUser _user;
        public MainWindow()
        {
            InitializeComponent();
            ((MainViewModel)this.DataContext).ClientIsAuthorized += ShowContent;


            //Connect();
        }

        private void ShowContent(bool isAuth)
        {
            if (isAuth) contentControl.Content = new HomeControl();

            else contentControl.Content = new AuthorizationControl();
        }

        public async void Connect()
        {
            client = await ClientFactory.BuildClient(
                                            new FactorySettings
                                            {
                                                Id = AppSettings.API_ID,
                                                Hash = AppSettings.API_HASH,
                                                StoreProvider = new TelegramClient.Core.Sessions.FileSessionStoreProvider("session"),
                                                ServerAddress = AppSettings.IP,
                                                ServerPort = AppSettings.PORT
                                            });


            await client.ConnectService.ConnectAsync();

            client.UpdatesService.RecieveUpdates += CheckUpdates;

            var contacts = await client.ContactsService.GetContactsAsync();

            _user = (contacts as TContacts).Users.Items.OfType<TUser>().FirstOrDefault(x => x.Username == "BValitov");

            //await client.MessagesService.SendMessageAsync(new TInputPeerUser() { UserId = user.Id}, "ты не пидор");

        }

        public async Task CheckUpdates(IUpdates x)
        {
            if (x is TUpdateShortMessage)
            {
                var TMessage = x as TUpdateShortMessage;

                //await client.MessagesService.ForwardMessageAsync(new TInputPeerUser() { UserId = _user.Id, AccessHash = _user.AccessHash}, TMessage.Id);
                var messages = new TVector<int>();
                messages.Items.Add(TMessage.Id);

                await client.MessagesService.ForwardMessagesAsync(new TInputPeerUser() { UserId = TMessage.UserId }, new TInputPeerUser() { UserId = _user.Id }, messages, true, true);
            }
        }

        #region trash
        public async void Authorization()
        {
            var client = await ClientFactory.BuildClient(
                new FactorySettings
                {
                    Id = AppSettings.API_ID,
                    Hash = AppSettings.API_HASH,
                    StoreProvider = new TelegramClient.Core.Sessions.FileSessionStoreProvider("session"),
                    ServerAddress = AppSettings.IP,
                    ServerPort = AppSettings.PORT
                });

            await client.ConnectService.ConnectAsync();

            var hash = await client.AuthService.SendCodeRequestAsync("89872328626");


            try
            {
                var user = await client.AuthService.MakeAuthAsync("89872328626", hash.PhoneCodeHash, "26220");
            }
            catch (CloudPasswordNeededException ex)
            {
                var password = (TPassword)await client.AuthService.GetPasswordSetting();

                var user = await client.AuthService.MakeAuthWithPasswordAsync(password, "");
            }
        }
        #endregion

        private void Listen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
