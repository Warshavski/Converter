
namespace Escyug.Converter.Presentation.Views
{
    public interface ITaskSettingsView : ISettingsView
    {
        string TaskName { get; set; }
        short TaskHours { get; set; }
        short TaskMinutes { get; set; }
    }
}
