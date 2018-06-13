using Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClient.Core;

namespace Client.BusinessLogic
{
    class ClientProvider
    {
        private static ITelegramClient client;

        public async static Task<ITelegramClient> GetInstance()
        {
            if (client == null)
            {
                await ConnectClient();
            }

            return client;
        }

        public ClientProvider() { }

        private async static Task ConnectClient()
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
        }
    }
}
