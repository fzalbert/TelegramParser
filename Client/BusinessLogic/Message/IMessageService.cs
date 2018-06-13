using OpenTl.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic.Message
{
    public interface IMessageService
    {
        void FindClient(string userName);

        void StartListenUpdate();
    }
}
