using System.Collections.Generic;

namespace Escyug.Converter.Models.Services
{
    public class ServiceResponse<TEntity>
    {
        public IEnumerable<WebServiceResponse> WebServiceMessages { get; set; }

        public string RejectedBatchName { get; set; }
        public IEnumerable<TEntity> RejectedBatch { get; set; }   
    }
}
