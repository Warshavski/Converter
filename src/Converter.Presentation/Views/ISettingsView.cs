using System;

using Escyug.Converter.Presentation.Common;

namespace Escyug.Converter.Presentation.Views
{
    public interface ISettingsView : IView
    {
        event Action Save;
    }
}
