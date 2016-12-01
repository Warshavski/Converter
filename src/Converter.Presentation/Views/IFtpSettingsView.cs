using System;

using Escyug.Converter.Presentation.Common;

namespace Escyug.Converter.Presentation.Views
{
    public interface IFtpSettingsView : ISettingsView
    {
        string FtpHost { get; set; }
        int FtpPort { get; set; }

        string FtpUser { get; set; }
        string FtpPassword { get; set; }
    }
}
