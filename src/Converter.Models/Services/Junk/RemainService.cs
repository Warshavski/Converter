using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Escyug.Converter.Models.RemainsServiceReference;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Repositories;
using System.Runtime.Serialization;

namespace Escyug.Converter.Models.Services.Junk
{
    //*** check DrugstoreServiceSoap for Dispose()
    public class RemainService : BaseService<RemainRow>
    {
        //*** private readonly RemainsProxyClass _proxyClass;
        private readonly DrugstoreServiceSoap _soapClient;


        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public RemainService(DrugstoreServiceSoap soapClient, 
            IRepository<RemainRow> remainRepository)
            : base(remainRepository)
        {
            /*** proxy class inject
             * 
             * _proxyClass = proxyClass 
             * 
             */
            
            _soapClient = soapClient;
        }



        // BASE CLASS ABSTRACT METHODS
        //---------------------------------------------------------------------

        #region BaseService members

        // *OVERRIDED* PROTECTED METHODS SECTION
        //-------------------------------------------------

        protected override bool SendBatch(IList<RemainRow> batchData, string clientId, long timeStamp, bool isFirtsBatch)
        {
            var remainsProxyArr = CreateProxyRemainsCollection(batchData);

            //*** check result
            //var result = _soapClient.RemainsClient2Save(clientId, timeStamp, isFirtsBatch, remainsProxyArr);

            var xmlFile = CreateXmlFile(remainsProxyArr);
            var xmlFileName = $"batch#{timeStamp}_{DateTime.Today.ToShortDateString()}.xml";
            xmlFile.Save("rejected/" + xmlFileName);

            //var RemainClient2Out = new RemainClient2Out() {"ErrorCode", "errorMEssage", "id"};

            return false;
        }

        protected override async Task<bool> SendBatchAsync(IList<RemainRow> batchData, string clientId, long timeStamp, bool isFirtsBatch)
        {
            var remainsProxyArr = CreateProxyRemainsCollection(batchData);
            
            //*** check result
            var result = await _soapClient.RemainsClient2SaveAsync(clientId, timeStamp, isFirtsBatch, remainsProxyArr);

            //*** if false serialize to xml and save
            var xmlFile = CreateXmlFile(remainsProxyArr);
            var xmlFileName = $"batch#{timeStamp}_{DateTime.Today.ToShortDateString()}.xml";
            xmlFile.Save("rejected/" + xmlFileName);

            return false;
        }

        protected override bool CheckEntitiesInGuides(IList<RemainRow> entityList, Entities.Guides.GuidesCollection guides)
        {
            throw new NotImplementedException();
        }

        #endregion BaseService members



        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Private methods

        private RemainClient2[] CreateProxyRemainsCollection(IList<RemainRow> remainsRow)
        {
            int proxyRemainsTotal = remainsRow.Count;
            var proxyRemainsArr = new RemainClient2[proxyRemainsTotal];
            for (int proxyRemainsCount = 0; proxyRemainsCount < proxyRemainsTotal; ++proxyRemainsCount)
            {
                proxyRemainsArr[proxyRemainsCount] = 
                    EntityToProxy(remainsRow[proxyRemainsCount]);
            }

            return proxyRemainsArr;
        }

        private RemainClient2 EntityToProxy(RemainRow remainRow)
        {
            //*** map every field

            var remainProxy = new RemainClient2();
            
            remainProxy.Id = remainRow.Guid;

            //*** check in guide
            remainProxy.ProductCode = long.Parse(remainRow.ProductCode);
            remainProxy.Store = remainRow.Store;
            remainProxy.StoreId = remainRow.StoreId;
            remainProxy.ProductName = remainRow.ProductName;
            remainProxy.Seria = remainRow.Series;
            remainProxy.QuantityRem = remainRow.QuantityRem;
            remainProxy.ProducerPrice = remainProxy.ProducerPrice;
            remainProxy.ProducerPriceVat = remainRow.ProducerPriceVat;
            remainProxy.ProducerVat = remainProxy.ProducerVat;
            remainProxy.SupplierVat = remainRow.SupplierVat; 
            remainProxy.SupplierPrice = remainRow.SupplierPrice; 
            remainProxy.ProducerPriceVat = remainRow.SupplierPriceVat; 
            remainProxy.RetailVat = remainRow.RetailVat; 
            remainProxy.RetailPrice = remainRow.RetailPrice; 
            remainProxy.RetailPriceVat = remainRow.RetailPriceVat; 
            remainProxy.ProgramName = remainRow.ProgramName;


            //remainRow.Unit; 
            //remainProxy remainRow.Supplier;
            //remainRow.ContractorId;
            //remainRow.DrugstoreLotGuid;
            //remainRow.UnifyGuid;
            //remainRow.ContractItemGuid;
            //remainRow.DocumentType; 
            //remainRow.DocumentId; 
            //remainRow.DocumentNumber; 
            //remainRow.DocumentItemId; 
            //remainRow.DocumentDate; 
            //remainRow.InvoiceDate; 
            //remainRow.InvoiceNumber; 
            //remainRow.Barcode; 
            //remainRow.Numerator; 
            //remainRow.Denominator; 
            //remainRow.QuantityAdd; 
            //remainRow.QuantitySub; 
            //remainRow.QuantityRes; 
            //remainRow.QuantityInvoice; 
            //remainRow.QuantityReturnSupplier;
            //remainRow.InternalInvoiceDate; 
            //remainRow.InternalInvoiceNumber; 
            //remainRow.QuantityMovementAdd; 
            //remainRow.QuantityMovementSub; 

            return remainProxy;
        }


        private XDocument CreateXmlFile(IEnumerable<RemainClient2> remiansProxyList)
        {
            /*** with serialization 
             * 
            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                // write xml into the writer
                var remainClient2s = remiansProxyList as RemainClient2[] ?? remiansProxyList.ToArray();
                var serializer = new DataContractSerializer(remainClient2s.ElementAt(0).GetType());
                serializer.WriteObject(writer, remainClient2s.ElementAt(0));
            }

            return doc;
            */

            /*** Without serialization */
           var remainsDoc = new XDocument();

            var remainsRoot = new XElement("remains");
            remainsDoc.Add(remainsRoot);

            foreach (var remain in remiansProxyList)
            {
                var remainRoot = new XElement("Recipe");

                remainRoot.Add(new XElement("Id", remain.Id));
                remainRoot.Add(new XElement("ProductCode", remain.Id));
                remainRoot.Add(new XElement("ProductName", remain.Id));
                remainRoot.Add(new XElement("Store", remain.Id));
                remainRoot.Add(new XElement("StoreId", remain.Id));
                remainRoot.Add(new XElement("Seria", remain.Id));
                remainRoot.Add(new XElement("QuantityRem", remain.Id));
                remainRoot.Add(new XElement("ProgramName", remain.Id));
                remainRoot.Add(new XElement("RetailVat", remain.Id));
                remainRoot.Add(new XElement("RetailPrice", remain.Id));
                remainRoot.Add(new XElement("RetailPriceVat", remain.Id));
                remainRoot.Add(new XElement("ProducerVat", remain.Id));
                remainRoot.Add(new XElement("ProducerPrice", remain.Id));
                remainRoot.Add(new XElement("ProducerPriceVat", remain.Id));
                remainRoot.Add(new XElement("SupplierVat", remain.Id));
                remainRoot.Add(new XElement("SupplierPrice", remain.Id));
                remainRoot.Add(new XElement("SupplierPriceVat", remain.Id));
                
                remainsRoot.Add(remainRoot);
            }

            return remainsDoc;
            

            /*
             *  
              <Id>string</Id>
              <ProductCode>long</ProductCode>
              <ProductName>string</ProductName>
              <Store>string</Store>
              <StoreId>long</StoreId>
              <Seria>string</Seria>
              <QuantityRem>decimal</QuantityRem>
              <ProgramName>string</ProgramName>
              <RetailVat>decimal</RetailVat>
              <RetailPrice>decimal</RetailPrice>
              <RetailPriceVat>decimal</RetailPriceVat>
              <ProducerVat>decimal</ProducerVat>
              <ProducerPrice>decimal</ProducerPrice>
              <ProducerPriceVat>decimal</ProducerPriceVat>
              <SupplierVat>decimal</SupplierVat>
              <SupplierPrice>decimal</SupplierPrice>
              <SupplierPriceVat>decimal</SupplierPriceVat>
             */
        }
        #endregion Private methods

    }
}
