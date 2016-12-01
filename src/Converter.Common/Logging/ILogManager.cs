using System;

using log4net;

namespace Escyug.Converter.Common.Logging
{
    /// <summary>
    ///     Used to request <see cref="ILog" /> instances.
    /// </summary>
    public interface ILogManager
    {
        ILog GetLog(Type typeAssociatedWithRequestedLog);
    }
}
