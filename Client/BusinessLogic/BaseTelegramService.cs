using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClient.Core;

namespace Client.BusinessLogic
{
    public class BaseTelegramService : INotifyErrorOccurred
    {
        protected ITelegramClient telegramClient;

        public event Action<string> ErrorOccurder;

        protected async Task ConnectClient()
        {
            try
            {
                telegramClient = await ClientProvider.GetInstance();
            }
            catch(Exception ex)
            {
                ErrorOccurder?.Invoke(ex.ToString());
            }
        }

        protected void OnErrorOccurded(string error)
        {
            if (ErrorOccurder != null)
                ErrorOccurder.Invoke(error);
        }
    }
}
