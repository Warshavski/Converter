using System;

using log4net;

namespace Escyug.Converter.Common.Logging
{
    public class LogManagerAdapter : ILogManager
    {
        public ILog GetLog(Type typeAssociatedWithRequestedLog)
        {
            var log = LogManager.GetLogger(typeAssociatedWithRequestedLog);
            return log;
        }
    }
}
