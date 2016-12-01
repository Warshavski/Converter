using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Escyug.Converter.Models.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Get all entities from data storage
        /// </summary>
        /// <param name="tableName">Name of the table where entities are stored</param>
        /// <returns>Collection(List) of entities</returns>
        List<TEntity> GetAll(string tableName);

        /// <summary>
        ///     Async get all entities from data storage
        /// </summary>
        /// <param name="tableName">Name of the table where entities are stored</param>
        /// <returns>Collection(List) of entities</returns>
        Task<List<TEntity>> GetAllAsync(string tableName);

        /// <summary>
        ///     Async get all entities from data storage
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation</param>
        /// <param name="tableName">Name of the table where entities are stored</param>
        /// <returns>Collection(List) of entities</returns>
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, string tableName);



        /** !!! NOT USED METHODS !!!
         * no need to implement this interface methods
         * we need all data
         **
        TEntity FindById(object id);
        Task<TEntity> FindByIdAsync(object id);
        Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object id);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        */
    }
}
