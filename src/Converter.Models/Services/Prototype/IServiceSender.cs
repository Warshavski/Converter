using System.Collections.Generic;

namespace Escyug.Converter.Models.Services.Prototype
{
    public interface IServiceSender<TEntity>
    {
        void TryToSend(IList<TEntity> entityList);
    }
}
