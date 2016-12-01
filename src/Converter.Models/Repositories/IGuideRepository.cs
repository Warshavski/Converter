using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Escyug.Converter.Models.Repositories
{
    public interface IGuideRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        HashSet<int> GetIds(string tableName);
        Task<HashSet<int>> GetIdsAsync(string tableName);
        Task<HashSet<int>> GetIdsAsync(CancellationToken cancellationToken, string tableName);
    }
}
