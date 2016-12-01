using System;
using System.Data;

namespace Escyug.Converter.Data.Ado.Extensions
{
    public static class AdoExtensions
    {

        /// <summary>
        ///     Try to get value from IDataRecord column(check for DBNull value).
        /// </summary>
        /// <typeparam name="TValue">Data type of column.</typeparam>
        /// <param name="reader">SqlDataReader instance.</param>
        /// <param name="columnName">Column index(name) from IDataRecord.</param>
        /// <returns>SqlDataReader column value and default data type value if column is DBNull</returns>
        public static TValue TryGetFieldValue<TValue>(this IDataRecord reader, string columnName)
        {
            if (reader == null)
            {
                throw  new ArgumentNullException(nameof(reader));
            }

            if (columnName == null)
            {
                throw new ArgumentNullException(nameof(columnName));
            }
            

            var columnIndex = reader.GetOrdinal(columnName);
            return reader.TryGetFieldValue<TValue>(columnIndex);
        }

        /// <summary>
        ///     Try to get value from IDataRecord column(check for DBNull value).
        /// </summary>
        /// <typeparam name="TValue">Data type of column.</typeparam>
        /// <param name="reader">SqlDataReader instance.</param>
        /// <param name="columnIndex">Column index(name) from IDataRecord.</param>
        /// <returns>SqlDataReader column value and default data type value if column is DBNull</returns>
        public static TValue TryGetFieldValue<TValue>(this IDataRecord reader, int columnIndex)
        {
            if (reader.IsDBNull(columnIndex))
            {
                return default(TValue);
            }
            else
            {
                return (TValue)reader[columnIndex];
            }
        }

        /// <summary>
        ///     Gets required field value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static TValue GetRequedFieldValue<TValue>(this IDataRecord reader, string columnName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (columnName == null)
            {
                throw new ArgumentNullException(nameof(columnName));
            }

            var columnIndex = reader.GetOrdinal(columnName);
            return reader.GetRequedFieldValue<TValue>(columnIndex);
        }

        /// <summary>
        ///     Gets required field value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="reader"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static TValue GetRequedFieldValue<TValue>(this IDataRecord reader, int columnIndex)
        {
            if (reader.IsDBNull(columnIndex))
            {
                throw new InvalidCastException("Column #" + columnIndex + " invalid format");
            }
            else
            {
                return (TValue)reader[columnIndex];
            }
        }
    }
}
