using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;

namespace Escyug.Converter.Data.Ado.Repositories
{
    public abstract class GuideRepository<TEntity> : Repository<TEntity>, IGuideRepository<TEntity>
        where TEntity : class, new()
    {
        protected GuideRepository(DbContext dbContext)
            : base(dbContext)
        {
            
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IGuiderRepository<TEntity> members

        public HashSet<int> GetIds(string tableName)
        {
            var commandText = "SELECT * FROM [" + tableName + "]";

            using (var connection = DbContext.CreateConnection())
            {
                using (var command = DbContext.CreateCommand(connection, commandText, CommandType))
                {
                    try
                    {
                        connection.Open();

                        var idsHashSet = ToHashSet(command);
                        return idsHashSet;
                    }
                    catch (DbException ex)
                    {
                        throw new RepositoryLoadException("Repository error : " + ex.Message, ex);
                    }

                }
            }
        }

        public async Task<HashSet<int>> GetIdsAsync(string tableName)
        {
            return await GetIdsAsync(CancellationToken.None, tableName);
        }

        public async Task<HashSet<int>> GetIdsAsync(CancellationToken cancellationToken, string tableName)
        {
            var commandText = "SELECT * FROM [" + tableName + "]";

            using (var connection = DbContext.CreateConnection())
            {
                using (var command = DbContext.CreateCommand(connection, commandText, CommandType))
                {
                    try
                    {
                        await connection.OpenAsync(cancellationToken);

                        var idsHashSet = await ToHashSetAsync(command, cancellationToken);
                        return idsHashSet;
                    }
                    catch (DbException ex)
                    {
                        throw new RepositoryLoadException("Repository error : " + ex.Message, ex);
                    }
                }
            }
        }

        #endregion IGuiderRepository<TEntity> members



        // PRIVATE/PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Private members

        private HashSet<int> ToHashSet(DbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                var idsHashSet = new HashSet<int>();
                while (reader.Read())
                {
                    var entityId = GetIdFromRecord(reader);

                    idsHashSet.Add(entityId);
                }
                return idsHashSet;
            }
        }

        private async Task<HashSet<int>> ToHashSetAsync(DbCommand command, CancellationToken token)
        {
            using (var reader = await command.ExecuteReaderAsync(token))
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                var idsHashSet = new HashSet<int>();
                while (await reader.ReadAsync(token))
                {
                    var entityId = GetIdFromRecord(reader);

                    idsHashSet.Add(entityId);
                }
                return idsHashSet;
            }
        }

        #endregion Private members


        #region Protected members

        protected abstract int GetIdFromRecord(IDataRecord record);

        #endregion Protected members
    }
}
