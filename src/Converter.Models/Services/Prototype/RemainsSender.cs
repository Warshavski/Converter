using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using log4net;

using Escyug.Converter.Common.Logging;
using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Utils.XmlConverter;

namespace Escyug.Converter.Models.Services.Prototype
{
    public class RemainsSender : ServiceSender<RemainRow>
    {
        public RemainsSender(ILogManager logManager, IXmlConverter<RemainRow> xmlConverter)
            : base(logManager, xmlConverter)
        {
             //*** proxy class inject      
        }


        protected override bool CallWebService(XDocument xmlDocument)
        {
            //*** ProxyClass.RemainResponse(xmlDoc);
            throw new NotImplementedException();
        }
       

        /*** Create separate class for xml convertation 
        public XDocument ConvertToXml(IList<RemainRow> remainRows)
        {
            var remainsDoc = new XDocument();

            var remainsRoot = new XElement("remains");
            remainsDoc.Add(remainsRoot);

            foreach (var remain in remainRows)
            {
                var remainRoot = new XElement("Remain");

                remainRoot.Add(new XElement("GUID", remain.Guid));
                remainRoot.Add(new XElement("CONTRACTOR_ID", remain.ContractorId));
                remainRoot.Add(new XElement("DRUGSTORE_LOT_GUID", remain.DrugstoreLotGuid));
                remainRoot.Add(new XElement("UNIFY_GUID", remain.UnifyGuid));
                remainRoot.Add(new XElement("PRODUCT_CODE", remain.ProductCode));
                remainRoot.Add(new XElement("CONTRACT_ITEM_GUID", remain.ContractItemGuid));
                remainRoot.Add(new XElement("STORE", remain.Store));
                remainRoot.Add(new XElement("STORE_ID", remain.StoreId));
                remainRoot.Add(new XElement("SUPPLIER", remain.Supplier));
                remainRoot.Add(new XElement("PRODUCT", remain.ProductName));

                remainRoot.Add(new XElement("UNIT", remain.Unit));
                remainRoot.Add(new XElement("SERIA", remain.Series));
                remainRoot.Add(new XElement("DOCUMENT_TYPE", remain.DocumentType));
                remainRoot.Add(new XElement("DOCUMENT_ID", remain.DocumentId));
                remainRoot.Add(new XElement("DOCUMENT_NUMBER", remain.DocumentNumber));
                remainRoot.Add(new XElement("DOCUMENT_ITEM_ID", remain.DocumentItemId));
                remainRoot.Add(new XElement("DOCUMENT_DATE", remain.DocumentDate));
                remainRoot.Add(new XElement("INVOICE_DATE", remain.InvoiceDate));
                remainRoot.Add(new XElement("INVOICE_NUMBER", remain.InvoiceNumber));
                remainRoot.Add(new XElement("BARCODE", remain.Barcode));
                remainRoot.Add(new XElement("NUMERATOR", remain.Numerator));
                remainRoot.Add(new XElement("DENOMINATOR", remain.Denominator));

                remainsRoot.Add(remainRoot);
            }

            return remainsDoc;
        }
        */

        //*** make this method virtual and replace to the base class
        /// <summary>
        ///     Serialize to xml
        /// </summary>
        /// <param name="remainRows"></param>
        /// <returns></returns>
        /*
        private XmlDocument ConvertToXml(IList<RemainRow> remainRows)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<RemainRow>), new Type[] { typeof(RemainRow) });
            //var subReq = remainRows;
            
            XmlDocument xmlDoc = null;
            using (StringWriter sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, remainRows);
                    var xml = sww.ToString();

                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);
                }
            }
            xmlDoc.Save("test.xml");

            return xmlDoc;
        }
        */
    }
}
