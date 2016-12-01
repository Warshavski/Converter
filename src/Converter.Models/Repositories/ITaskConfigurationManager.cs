using Escyug.Converter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.Converter.Models.Repositories
{
    public interface ITaskConfigurationManager : IConfigurationManager<ConverterTask>
    {
        void Set(ConverterTask task);
    }
}
