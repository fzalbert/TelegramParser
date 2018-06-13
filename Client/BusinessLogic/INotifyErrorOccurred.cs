using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public interface INotifyErrorOccurred
    {
        event Action<string> ErrorOccurder;
    }
}
