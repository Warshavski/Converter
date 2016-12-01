namespace Escyug.Converter.Models.Entities.Configurations
{
    public class SenderConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string SenderLogin { get; set; }
        public string SenderPassword { get; set; }

        public string[] To { get; set; }
    }
}
