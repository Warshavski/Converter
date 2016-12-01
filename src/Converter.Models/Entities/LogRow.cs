namespace Escyug.Converter.Models.Entities
{
    public class LogRow
    {
        public string Date { get; }
        public string Message { get; }

        public LogRow(string date, string message)
        {
            Date = date;
            Message = message;
        }
    }
}
