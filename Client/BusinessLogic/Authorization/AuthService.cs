using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic.Authorization
{
    public class AuthService : BaseTelegramService, IAuthService
    {
        private string _hash;
        
        public AuthService()
        {
            _hash = "";
        }
        public async Task SendCode(string phoneNumber)
        {
            if (phoneNumber.Any(x => char.IsLetter(x)))
                throw new Exception("phone number can contain only numbers and +");

            try
            {
                var hash = await telegramClient.AuthService.SendCodeRequestAsync(phoneNumber);

                _hash = hash.PhoneCodeHash;
            }
            catch(Exception ex)
            {
                OnErrorOccurded(ex.ToString());
            }
        }

        public async Task SignUp(string phoneNumber, string code)
        {
            if (phoneNumber.Any(x => char.IsLetter(x)))
                throw new Exception("phone number can contain only numbers and +");
            if (_hash == "")
                OnErrorOccurded("при отправке возникла ошибка, введите номер телефона ещё раз");
            try
            {
                await telegramClient.AuthService.MakeAuthAsync(phoneNumber, _hash, code);
            }
            catch(Exception ex)
            {
                OnErrorOccurded(ex.ToString());
            }
        }

        public async Task CreateClient()
        {
            await ConnectClient();
        }

        public bool IsClientAuthorized()
        {
            return telegramClient.AuthService.IsUserAuthorized();
        }
    }
}
