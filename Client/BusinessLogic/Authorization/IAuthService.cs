using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic.Authorization
{
    public interface IAuthService : INotifyErrorOccurred
    {
        Task SendCode(string phoneNumber);

        Task SignUp(string phoneNumber, string code);

        Task CreateClient();

        bool IsClientAuthorized();
    }
}
