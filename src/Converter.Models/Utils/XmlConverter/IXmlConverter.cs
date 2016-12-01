using System.Collections.Generic;
using System.Xml.Linq;

namespace Escyug.Converter.Models.Utils.XmlConverter
{
    public interface IXmlConverter<TEntity>
    {
        XDocument ConvertToXml(IList<TEntity> entityList);
    }
}
