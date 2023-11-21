using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILoggerManager
    {
        void logInfo(string message);

        void logWarn(string message);

        void logDebug(string message);

        void logError(string message);
    }
}
