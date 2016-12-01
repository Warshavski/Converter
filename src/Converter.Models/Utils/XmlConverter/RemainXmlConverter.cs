using System.Collections.Generic;
using System.Xml.Linq;

using Escyug.Converter.Models.Entities;

namespace Escyug.Converter.Models.Utils.XmlConverter
{
    public class RemainXmlConverter : IXmlConverter<RemainRow>
    {
        public XDocument ConvertToXml(IList<RemainRow> entityList)
        {
            var remainsDoc = new XDocument();

            var remainsRoot = new XElement("remains");
            remainsDoc.Add(remainsRoot);

            foreach (var remain in entityList)
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

                remainRoot.Add(new XElement("QUANTITY_ADD", remain.QuantityAdd));
                remainRoot.Add(new XElement("QUANTITY_SUB", remain.QuantitySub));
                remainRoot.Add(new XElement("QUANTITY_RES", remain.QuantityRes));
                remainRoot.Add(new XElement("QUANTITY_REM", remain.QuantityRem));
                remainRoot.Add(new XElement("QUANTITY_INVOICE", remain.QuantityInvoice));
                remainRoot.Add(new XElement("QUANTITY_RETURN_SUPPLIER", remain.QuantityReturnSupplier));
                remainRoot.Add(new XElement("PRODUCER_VAT", remain.ProducerVat));
                remainRoot.Add(new XElement("PRODUCER_PRICE", remain.ProducerPrice));
                remainRoot.Add(new XElement("PRODUCER_PRICE_VAT", remain.ProducerPriceVat));
                remainRoot.Add(new XElement("SUPPLIER_VAT", remain.SupplierVat));
                remainRoot.Add(new XElement("SUPPLIER_PRICE", remain.SupplierPrice));
                remainRoot.Add(new XElement("SUPPLIER_PRICE_VAT", remain.SupplierPriceVat));
                remainRoot.Add(new XElement("RETAIL_VAT", remain.RetailVat));
                remainRoot.Add(new XElement("RETAIL_PRICE", remain.RetailPrice));
                remainRoot.Add(new XElement("RETAIL_PRICE_VAT", remain.RetailPriceVat));
                remainRoot.Add(new XElement("INTERNAL_INVOICE_DATE", remain.InternalInvoiceDate));
                remainRoot.Add(new XElement("INTERNAL_INVOICE_NUMBER", remain.InternalInvoiceNumber));
                remainRoot.Add(new XElement("QUANTITY_MOVEMENT_ADD", remain.QuantityMovementAdd));
                remainRoot.Add(new XElement("QUANTITY_MOVEMENT_SUB", remain.QuantityMovementSub));
                remainRoot.Add(new XElement("PROGRAM_NAME", remain.ProgramName));

                remainsRoot.Add(remainRoot);
            }

            return remainsDoc;
        }


        /*
            GUID
            CONTRACTOR_ID
            DRUGSTORE_LOT_GUID
            UNIFY_GUID
            PRODUCT_CODE
            CONTRACT_ITEM_GUID
            STORE
            STORE_ID
            SUPPLIER
            PRODUCT
            UNIT
            SERIA
            DOCUMENT_TYPE
            DOCUMENT_ID
            DOCUMENT_NUMBER
            DOCUMENT_ITEM_ID
            DOCUMENT_DATE
            INVOICE_DATE
            INVOICE_NUMBER
            BARCODE
            NUMERATOR
            DENOMINATOR
         * 
            QUANTITY_ADD
            QUANTITY_SUB
            QUANTITY_RES
            QUANTITY_REM
            QUANTITY_INVOICE
            QUANTITY_RETURN_SUPPLIER
            PRODUCER_VAT
            PRODUCER_PRICE
            PRODUCER_PRICE_VAT
            SUPPLIER_VAT
            SUPPLIER_PRICE
            SUPPLIER_PRICE_VAT
            RETAIL_VAT
            RETAIL_PRICE
            RETAIL_PRICE_VAT
            INTERNAL_INVOICE_DATE
            INTERNAL_INVOICE_NUMBER
            QUANTITY_MOVEMENT_ADD
            QUANTITY_MOVEMENT_SUB
            PROGRAM_NAME

         */

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


            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                // write xml into the writer
                var serializer = new DataContractSerializer(objectToSerialize.GetType());
                serializer.WriteObject(writer, objectToSerialize);
            }
            Console.WriteLine(doc.ToString());


            return xmlDoc;
        }
        */
    }
}
