using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Linq;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.RemainsServiceReference;

namespace Escyug.Converter.Models.Services
{
    public class RemainService : EntityService<RemainRow>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        //*** private readonly RemainsProxyClass _proxyClass;
        private readonly DrugstoreServiceSoap _soapClient;



        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public RemainService(DrugstoreServiceSoap soapClient)
        {
            _soapClient = soapClient;
            BatchName = "remains";
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        public override bool CheckRequiredFields(IEnumerable<RemainRow> entitiesList, GuidesIdsCollection guidesIds)
        {
            /** LINQ equvalent
             * 
             * return entitiesList.All(entity => 
             *     guidesIds.TradeNamesGudeIds.Contains(int.Parse(entity.ProductCode)));
             */

            foreach (var entity in entitiesList)
            {
                if (!guidesIds.TradeNamesGudeIds.Contains(int.Parse(entity.ProductCode)))
                {
                    return false;
                }
            }

            return true;
        }

        public override XDocument CreateXmlFile(IEnumerable<RemainRow> entitiesList)
        {
            var remainsDoc = new XDocument();

            var remainsRoot = new XElement("remains");
            remainsDoc.Add(remainsRoot);

            foreach (var remain in entitiesList)
            {
                var remainRoot = new XElement("RemainClient2");

                remainRoot.Add(new XElement("Id", remain.Guid));
                remainRoot.Add(new XElement("ProductCode", remain.ProductCode));
                remainRoot.Add(new XElement("ProductName", remain.ProductName));
                remainRoot.Add(new XElement("Store", remain.Store));
                remainRoot.Add(new XElement("StoreId", remain.StoreId));
                remainRoot.Add(new XElement("Seria", remain.Series));
                remainRoot.Add(new XElement("QuantityRem", remain.QuantityRem));
                remainRoot.Add(new XElement("ProgramName", remain.ProgramName));
                remainRoot.Add(new XElement("RetailVat", remain.RetailVat));
                remainRoot.Add(new XElement("RetailPrice", remain.RetailPrice));
                remainRoot.Add(new XElement("RetailPriceVat", remain.RetailPriceVat));
                remainRoot.Add(new XElement("ProducerVat", remain.ProducerVat));
                remainRoot.Add(new XElement("ProducerPrice", remain.ProducerPrice));
                remainRoot.Add(new XElement("ProducerPriceVat", remain.ProducerPriceVat));
                remainRoot.Add(new XElement("SupplierVat", remain.SupplierVat));
                remainRoot.Add(new XElement("SupplierPrice", remain.SupplierPrice));
                remainRoot.Add(new XElement("SupplierPriceVat", remain.SupplierPriceVat));

                remainsRoot.Add(remainRoot);
            }

            return remainsDoc;
        }



        // PRIVATE/PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Protected helper methods



        // BATCH SENDING SECITON
        //-------------------------------------------------

        protected override ServiceResponse<RemainRow> TryToSendBatch(IList<RemainRow> entityList,
           long timestamp, bool isFirstBatch, string clientId)
        {
            //*** check for null and check for valid conversion
            var proxyEntityList = CreateProxyEntityList(entityList);

            try
            {
                var webServiceResponse = _soapClient.RemainsClient2Save(clientId, timestamp,
                    isFirstBatch, proxyEntityList);
                //*** convert server result to client class
                var clientResult = ConvertWebServiceResponce(webServiceResponse, entityList);

                //*** return result
                return clientResult;
            }
            catch (EndpointNotFoundException ex)
            {
                //*** return error state   
                return CreateErrorResponse(ex, entityList);
            }
        }

        /** ASYNC DATA SEND */
        protected override async Task<ServiceResponse<RemainRow>> TryToSendBatchAsync(IList<RemainRow> entityList,
           long timestamp, bool isFirstBatch, string clientId)
        {
            var proxyEntityList = CreateProxyEntityList(entityList);

            try
            {
                var webServiceResponse = await _soapClient.RemainsClient2SaveAsync(clientId, timestamp,
                    isFirstBatch, proxyEntityList);
                //*** convert server result to client class
                var clientResult = ConvertWebServiceResponce(webServiceResponse, entityList);

                //*** return result
                return clientResult;
            }
            catch (EndpointNotFoundException ex)
            {
                //*** return error state   
                return CreateErrorResponse(ex, entityList);
            }
        }


        #endregion Protected helper methods


        #region Pivate helper methods
        


        // PROXY CLASS CREATION SECTION
        //-------------------------------------------------

        //*** create batch for multiline recipes
        private RemainClient2[] CreateProxyEntityList(IList<RemainRow> remainRowsList)
        {
            int proxyRemainsTotal = remainRowsList.Count;
            var proxyRemainsArr = new RemainClient2[proxyRemainsTotal];
            for (int proxyRemainsCount = 0; proxyRemainsCount < proxyRemainsTotal; ++proxyRemainsCount)
            {
                proxyRemainsArr[proxyRemainsCount] =
                    CreateProxyEntity(remainRowsList[proxyRemainsCount]);
            }

            return proxyRemainsArr;
        }

        private RemainClient2 CreateProxyEntity(RemainRow remainRow)
        {
            //*** map every field

            var remainProxy = new RemainClient2();

            remainProxy.Id = remainRow.Guid; //*** id check
            remainProxy.ProductCode = long.Parse(remainRow.ProductCode);
            remainProxy.ProductName = remainProxy.ProductName;
            remainProxy.Store = remainRow.Store;
            remainProxy.StoreId = remainRow.StoreId;
            remainProxy.Seria = remainRow.Series;
            remainProxy.QuantityRem = remainRow.QuantityRem;
            remainProxy.ProgramName = remainRow.ProgramName;
            remainProxy.RetailVat = remainRow.RetailVat;
            remainProxy.RetailPrice = remainRow.RetailPrice;
            remainProxy.RetailPriceVat = remainRow.RetailPriceVat;
            remainProxy.ProducerVat = remainRow.ProducerVat;
            remainProxy.ProducerPrice = remainRow.ProducerPrice;
            remainProxy.ProducerPriceVat = remainRow.ProducerPriceVat;
            remainProxy.SupplierVat = remainRow.SupplierVat;
            remainProxy.SupplierPrice = remainRow.SupplierPrice;
            remainProxy.SupplierPriceVat = remainRow.SupplierPriceVat;

            return remainProxy;
        }



        // SERVICE RESPONSE CREATION SECTION
        //-------------------------------------------------

        //*** sipmlify this method (try to replace to the base class) same method in RecipeService.cs
        private ServiceResponse<RemainRow> ConvertWebServiceResponce(RemainClient2Out[] webServiceResponse,
            IEnumerable<RemainRow> recipesBatch)
        {
            //*** create auth error
            if (webServiceResponse == null || webServiceResponse.Length == 0)
            {
                var errorMessage = ErrorCodes["ERR|AUTH"];
                var authErrorResponse = CreateErrorResponse(errorMessage, recipesBatch);

                return authErrorResponse;
            }

            bool isError = false;
            var webServiceMessages = new List<WebServiceResponse>();
            foreach(var responseEntity in webServiceResponse)
            {
                if (!isError && !string.IsNullOrEmpty(responseEntity.ErrorMsg))
                {
                    isError = true;
                }

                var errorMessage = string.Empty;
                ErrorCodes.TryGetValue(responseEntity.ErrorCode, out errorMessage);
                webServiceMessages.Add(new WebServiceResponse
                {
                    Id = responseEntity.Id,
                    Message = errorMessage
                });
            }
            
            if (isError)
            {
                return CreateErrorResponse(webServiceMessages, recipesBatch);
            }

            return new ServiceResponse<RemainRow>()
            {
                WebServiceMessages = webServiceMessages,
                RejectedBatch = null,
                RejectedBatchName = string.Empty
            };
        }


        /**
        private IEnumerable<WebServiceResponse> GetWebServiceErrors(RemainClient2Out[] webServiceResponse)
        {
            var webServiceErrors = new List<WebServiceResponse>();
            foreach (var error in webServiceResponse)
            {
                //*** in new version should be id and error code and get error msg from dictionary
                webServiceErrors.Add(
                    new WebServiceResponse()
                    {
                        Id = error.Id,
                        ErrorCode = error.ErrorCode,
                        ErrorMessage = error.ErrorMsg, //ErrorCodes[error.ErrorCode];
                    });
            }

            return webServiceErrors;
        }
        */
       


        #endregion Pivate helper methods
    }
}
