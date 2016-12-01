using System;
using System.Collections.Generic;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Presentation.Common;

namespace Escyug.Converter.Presentation.Views
{
    public interface ILogsView : IView
    {
        ICollection<LogRow> LogsList { set; }
    }
}
