using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.Converter.Models.Services.Prototype
{
    /// <summary>
    ///     Helper class for metadata parsing
    /// </summary>
    public class FileMetadata
    {
        /// <summary>
        ///     File name with extension
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Create(update) file date
        /// </summary>
        public string Date { get; }

        /// <summary>
        ///     A flag that indicates the need to update
        /// </summary>
        public bool IsForUpdate { get; private set; }

        /// <summary>
        ///     File metadata constructor
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="date">Create(update) file date</param>
        public FileMetadata(string name, string date)
        {
            Name = name;
            Date = date;
            IsForUpdate = false;
        }

        public void SetForUpdate()
        {
            IsForUpdate = true;
        }
    }
}
