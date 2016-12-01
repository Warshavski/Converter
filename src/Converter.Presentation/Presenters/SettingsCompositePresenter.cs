using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escyug.Converter.Presentation.Common;
using Escyug.Converter.Presentation.Views;

namespace Escyug.Converter.Presentation.Presenters
{
    public class SettingsCompositePresenter : BasePresenter<ISettingsCompositeView>
    {
        public SettingsCompositePresenter(ISettingsCompositeView view, IApplicationController appController)
            : base(view, appController)
        {
            View.InitializeView += () => OnInitializeView();
            View.InitializeViewAsync += () => OnInitializeViewAsync();
        }

        private void OnInitializeView()
        {
            AppController.Run<ConnectionsSettingsPresenter>();
            AppController.Run<FtpSettingsPresenter>();
            AppController.Run<LogsPresenter>();
            AppController.Run<SenderSettingsPresenter>();
            AppController.Run<TaskSettingsPresenter>();
        }

        private async Task OnInitializeViewAsync()
        {
            await Task.Run(() => { OnInitializeView(); });
        }
    }
}
