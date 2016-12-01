using System;

using Escyug.Converter.Presentation.Common;

namespace Escyug.Converter.Presentation.Views
{
    public interface ISenderSettingsView : ISettingsView
    {
        string Host { get; set; }
        int Port { get; set; }

        string Login { get; set; }
        string Password { get; set; }
        
        string Receiver { get; set; }
    }
}
