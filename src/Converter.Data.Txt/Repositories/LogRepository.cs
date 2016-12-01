using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Threading;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;

namespace Escyug.Converter.Data.Txt.Repositories
{
    public class LogRepository : IRepository<LogRow>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly string _filePath;



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public LogRepository(string filePath)
        {
            _filePath = filePath;
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IRepository<LogRow> members

        public List<LogRow> GetAll(string tableName)
        {
            try
            {
                var logsList = new List<LogRow>();
                using (var reader = new StreamReader(
                    Path.Combine(_filePath, tableName), Encoding.Default, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var textLine = reader.ReadLine();
                        var logRow = ParseLogRow(textLine);

                        logsList.Add(logRow);
                    }
                }

                return logsList;
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new RepositoryLoadException("Wrong file format", ex);
            }
            catch (IOException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }

        public async Task<List<LogRow>> GetAllAsync(string tableName)
        {
            return await GetAllAsync(CancellationToken.None, tableName);
        }

        public async Task<List<LogRow>> GetAllAsync(CancellationToken cancellationToken, string tableName)
        {
            try
            {
                var logsList = new List<LogRow>();
                using (var reader = new StreamReader(
                    Path.Combine(_filePath, tableName), Encoding.Default, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var textLine = await reader.ReadLineAsync();
                        var logRow = ParseLogRow(textLine);

                        logsList.Add(logRow);
                    }
                }

                return logsList;
            }
            catch (IOException ex)
            {
                throw  new RepositoryLoadException(ex.Message, ex);
            }
        }

        #endregion IRepository<LogRow> members
        


        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        private LogRow ParseLogRow(string textLine)
        {
            string[] logFields = textLine.Split(';');
            var logRow = new LogRow(logFields[0], logFields[1]);

            return logRow;
        }
    }
}
