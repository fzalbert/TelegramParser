using Client.Helpers;
using OpenTl.Schema;
using OpenTl.Schema.Contacts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client.BusinessLogic.Message
{
    public class MessageService : BaseTelegramService, IMessageService
    {
        List<string> keys;
        TUser _user;

        public async void FindClient(string userName)
        {
            telegramClient = await ClientProvider.GetInstance();

            var contacts = await telegramClient.ContactsService.GetContactsAsync();

            _user = (contacts as TContacts).Users.Items.OfType<TUser>().FirstOrDefault(x => x.Username == userName);
            if (_user == null) OnErrorOccurded($"Пользователь с именем: {userName} не найден");
        }

        public async void StartListenUpdate()
        {
            telegramClient = await ClientProvider.GetInstance();

            using (StreamReader sr = new StreamReader(File.Open(AppSettings.KEYS_PATH, FileMode.OpenOrCreate), Encoding.Default))
            {

                string file = sr.ReadToEnd();

                keys = new List<string>(file.Split());
                keys = keys.Select(x => x = x.Replace("*", @"(\w*)")).ToList();

                keys.RemoveAll(x => x == string.Empty);
            }

            telegramClient.UpdatesService.RecieveUpdates += CheckUpdate;

            await telegramClient.ConnectService.ConnectAsync();
        }

        public async Task ForwardMessage(TUpdateShortMessage message)
        {
            var messages = new TVector<int>();
            messages.Items.Add(message.Id);

            await telegramClient.MessagesService.ForwardMessagesAsync(new TInputPeerUser() { UserId = message.UserId }, new TInputPeerUser() { UserId = _user.Id }, messages, true, true);
        }

        private async Task CheckUpdate(IUpdates update)
        {
            if (!(update is TUpdateShortMessage)) return;

            var message = update as TUpdateShortMessage;

            if (IsMessageRight(message.Message)) await ForwardMessage(message);
        }

        private bool IsMessageRight(string message)
        {
            string pattern = string.Empty;
            for (int i = 0; i < keys.Count - 1; i++)
                pattern = pattern + keys[i] + "|";
            pattern = pattern + keys.Last();

            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(message);
            if (matches.Count > 0)
                return true;
            return false;
        }
    }
}
