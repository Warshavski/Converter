namespace Escyug.Converter.Models.Services
{
    public class WebServiceResponse
    {
        public string Id { get; set; }
        public string Message { get; set; }

        public WebServiceResponse()
        {
            Id = string.Empty;
            Message = string.Empty;
        }
    }
}
