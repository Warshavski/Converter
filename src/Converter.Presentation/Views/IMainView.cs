using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Escyug.Converter.Presentation.Common;

namespace Escyug.Converter.Presentation.Views
{
    public interface IMainView : IView
    {
        event Func<Task> InitializeAsync;

        bool IsBusy { get; set; }
    }
}
