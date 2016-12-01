using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;

namespace Escyug.Converter.Data.Ado.Repositories
{
    /// <summary>
    ///     Generic implementation of IRepository
    /// </summary>
    /// <typeparam name="TEntity">Type of entity for repository</typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {

        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        protected const CommandType CommandType = System.Data.CommandType.Text;

        private readonly DbContext _dbContext;

        protected DbContext DbContext
        {
            get { return _dbContext; }
        }


        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        protected Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IRepository<TEntity> members

        /// <summary>
        ///     Get all entities from data storage
        /// </summary>
        /// <param name="tableName">Name of the table where entities are stored</param>
        /// <returns>Collection(List) of entities</returns>
        public List<TEntity> GetAll(string tableName)
        {
            var commandText = "SELECT * FROM [" + tableName + "]";
            
            using (var connection = _dbContext.CreateConnection())
            {
                using (var command = _dbContext.CreateCommand(connection, commandText, CommandType))
                {
                    try
                    {
                        connection.Open();

                        var entityList = ToList(command);
                        return entityList;
                    }
                    catch (DbException ex)
                    {
                        throw new RepositoryLoadException("Repository error : " + ex.Message, ex);
                    }
                    
                }
            }
        }

        /// <summary>
        ///     Async get all entities from data storage
        /// </summary>
        /// <param name="tableName">Name of the table where entities are stored</param>
        /// <returns>Collection(List) of entities</returns>
        public async Task<List<TEntity>> GetAllAsync(string tableName)
        {
            return await GetAllAsync(CancellationToken.None, tableName);
        }

        /// <summary>
        ///     Async get all entities from data storage
        /// </summary>
        /// <param name="cancellationToken">Token used to cancel the operation</param>
        /// <param name="tableName">Name of the table where entities are stored</param>
        /// <returns>Collection(List) of entities</returns>
        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, string tableName)
        {
            var commandText = "SELECT * FROM [" + tableName + "]";
            
            using (var connection = _dbContext.CreateConnection())
            {
                using (var command = _dbContext.CreateCommand(connection, commandText, CommandType))
                {
                    try
                    {
                        await connection.OpenAsync(cancellationToken);

                        var entityList = await ToListAsync(command, cancellationToken);
                        return entityList;
                    }
                    catch (DbException ex)
                    {
                        throw new RepositoryLoadException("Repository error : " + ex.Message, ex);
                    }
                }
            }
        }

        #endregion IRepository<TEntity> members



        // PRIVATE/PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Private members

        private List<TEntity> ToList(DbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                var entityList = new List<TEntity>();
                while (reader.Read())
                {
                    var entity = new TEntity();
                    Map(reader, entity);

                    entityList.Add(entity);
                }
                return entityList;
            }
        }

        private async Task<List<TEntity>> ToListAsync(DbCommand command, CancellationToken token)
        {
            using (var reader = await command.ExecuteReaderAsync(token))
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                var entityList = new List<TEntity>();
                while (await reader.ReadAsync(token))
                {
                    var entity = new TEntity();
                    Map(reader, entity);

                    entityList.Add(entity);
                }
                return entityList;
            }
        }

        #endregion Private members

        #region Protected members

        /// <summary>
        ///     Map data record to domain entity
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="entity">Domain entity</param>
        protected abstract void Map(IDataRecord record, TEntity entity);

        #endregion Protected members
    }
}


/* OLD VERSION ToList 
 *  using (var reader = command.ExecuteReader())
    {
        List<TEntity> items = new List<TEntity>();
        while (reader.Read())
        {
            var item = new TEntity();
            Map(reader, item);
            items.Add(item);
        }
        return items;
    }
*/