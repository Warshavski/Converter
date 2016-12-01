using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Linq;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.RecipesServiceReference;


namespace Escyug.Converter.Models.Services
{
    /** TODO :
     *  1. Modernize(optimaze) batch creation algorithm
     *  2. ?? Change RecipeRange class => Dictionary(int,int)
     */

    public class RecipeService : EntityService<RecipeRow>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        //*** private readonly RemainsProxyClass _proxyClass;
        private readonly RecipeServiceSoap _soapClient;



        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public RecipeService(RecipeServiceSoap soapClient)
        {
            /*** proxy class inject
             * 
             * _proxyClass = proxyClass 
             * 
             */
            _soapClient = soapClient;
            BatchName = "recipes";
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        public override bool CheckRequiredFields(IEnumerable<RecipeRow> entitiesList, GuidesIdsCollection guidesIds)
        {
            throw new NotImplementedException();
        }

        //*** ?? injecting xml converter
        public override XDocument CreateXmlFile(IEnumerable<RecipeRow> entitiesList)
        {
            var recipesDoc = new XDocument();

            var recipesRoot = new XElement("recipes");
            recipesDoc.Add(recipesRoot);

            foreach (var recipe in entitiesList)
            {
                var recipeRoot = new XElement("RecipeClient2");

                recipeRoot.Add(new XElement("ProgramName", recipe.Program));
                recipeRoot.Add(new XElement("Seria", recipe.Series));
                recipeRoot.Add(new XElement("Number", recipe.Number));
                recipeRoot.Add(new XElement("DoctorCode", recipe.DoctorCode));
                recipeRoot.Add(new XElement("LpuOgrn", recipe.LpuOgrn));
                recipeRoot.Add(new XElement("LpuFoms", recipe.LpuFoms));
                recipeRoot.Add(new XElement("Snils", recipe.Snils));
                recipeRoot.Add(new XElement("PatientLastname", recipe.PatientLastname));
                recipeRoot.Add(new XElement("PatientFirstname", recipe.PatientFirstname));
                recipeRoot.Add(new XElement("PatientMiddlename", recipe.PatientMiddlename));
                recipeRoot.Add(new XElement("PatientSex", recipe.PatientSex));
                //recipeRoot.Add(new XElement("DocumnentState", recipe.DocumnentState));
                recipeRoot.Add(new XElement("PrivilegeCode", recipe.PrivilegeCode));
                recipeRoot.Add(new XElement("FundingSourceCode", recipe.FundingSourceCode));
                recipeRoot.Add(new XElement("NosologyCode", recipe.NosologyCode));
                recipeRoot.Add(new XElement("MkbCode", recipe.MkbCode));
                recipeRoot.Add(new XElement("IssueDate", recipe.IssueDate));
                recipeRoot.Add(new XElement("IsVk", recipe.IsVk));
                recipeRoot.Add(new XElement("ValidPeriodCode", recipe.ValidPeriodCode));
                //recipeRoot.Add(new XElement("MnnCode", recipe.MnnCode));
                //recipeRoot.Add(new XElement("TrnCode", recipe.TrnCode));
                recipeRoot.Add(new XElement("Dosage", recipe.Dosage));
                recipeRoot.Add(new XElement("CureformCode", recipe.CureformCode));
                recipeRoot.Add(new XElement("Quantity", recipe.Quantity));
                //recipeRoot.Add(new XElement("QuantityUnit", recipe.QuantityUnit));
                recipeRoot.Add(new XElement("SaleDate", recipe.SaleDate));
                recipeRoot.Add(new XElement("IncomeDate", recipe.IncomeDate));
                recipeRoot.Add(new XElement("Price", recipe.Price));
                //recipeRoot.Add(new XElement("Id", recipe.Id));

                recipeRoot.Add(recipeRoot);
            }

            return recipesDoc;
        }



        // PRIVATE/PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Protected helper methods



        // BATCH SENDING SECITON
        //-------------------------------------------------

        protected override ServiceResponse<RecipeRow> TryToSendBatch(IList<RecipeRow> entityList,
           long timestamp, bool isFirstBatch, string clientId)
        {
            var proxyEntityList = CreateProxyEntityList(entityList);

            try
            {
                var webServiceResponse = _soapClient.RecipesClient2Save(clientId, proxyEntityList);
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
        protected override async Task<ServiceResponse<RecipeRow>> TryToSendBatchAsync(IList<RecipeRow> entityList,
           long timestamp, bool isFirstBatch, string clientId)
        {
            var proxyEntityList = CreateProxyEntityList(entityList);

            try
            {
                var webServiceResponse = 
                    await _soapClient.RecipesClient2SaveAsync(clientId, proxyEntityList);
                
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
        


        // BATCH CREATION SECTION
        //-------------------------------------------------

        protected override IList<IList<RecipeRow>> CreateBatchList(IList<RecipeRow> entityList)
        {
            var batchList = new List<IList<RecipeRow>>();
            
            var batchSize = 0;
            int startBatchIndex = 0;
            
            var recipesRangeMap = CreateRecipesRangeMap(entityList);
            var recipeRangeTotal = recipesRangeMap.Count;
            for (int recipeRangeCount = 0; recipeRangeCount < recipeRangeTotal; ++recipeRangeCount)
            {
                var recipeRange = recipesRangeMap[recipeRangeCount].Range;
                if (batchSize + recipeRange <= BatchSizeMax)
                {
                    batchSize += recipesRangeMap[recipeRangeCount].Range;
                }
                else
                {
                    batchList.Add(CreateBatch(entityList, batchSize, startBatchIndex));
                    startBatchIndex = recipesRangeMap[recipeRangeCount].StartIndex;
                    batchSize = recipesRangeMap[recipeRangeCount].Range;

                }

                if (recipeRangeCount == recipeRangeTotal - 1)
                {
                    batchList.Add(CreateBatch(entityList, batchSize, startBatchIndex));
                }
            }

            return batchList;
        }
        
        #endregion Protected helper methods


        #region Pivate helper methods

        /// <summary>
        ///     Counts recipes. Single line(1) and multiline(> 1)
        /// </summary>
        /// <param name="recipesRowsList">List of the recipes rows</param>
        /// <returns>Range collection. Recipe collection index and total lines</returns>
        private IList<RecipeRange> CreateRecipesRangeMap(IList<RecipeRow> recipesRowsList)
        {
            var recipesRange = new List<RecipeRange>();

            int recipesTotal = recipesRowsList.Count;
            RecipeRow previousRecipe = null;
            for (int recipeCount = 0; recipeCount < recipesTotal; ++recipeCount)
            {
                //*** compare values (GetHashCode())
                if (recipesRowsList[recipeCount].Equals(previousRecipe))
                {
                    continue;
                }

                int recipeRange = 1;
                if (recipeCount != recipesTotal - 1)
                {
                    var recipeLineCount = recipeCount + 1;
                    while (recipeLineCount < recipesTotal &&
                        //*** compare values (GetHashCode())
                        recipesRowsList[recipeCount].Equals(recipesRowsList[recipeLineCount]))
                    {
                        ++recipeRange;
                        ++recipeLineCount;
                    }

                    previousRecipe = recipesRowsList[recipeCount];
                }

                recipesRange.Add(new RecipeRange()
                {
                    StartIndex = recipeCount,
                    Range = recipeRange
                });
            }

            return recipesRange;
        }

        

        // PROXY CLASS CREATION SECTION
        //-------------------------------------------------

        //*** create batch for multiline recipes
        private RecipeClient2[] CreateProxyEntityList(IList<RecipeRow> recipeRowsList)
        {
            int proxyRecipesTotal = recipeRowsList.Count;
            var proxyRecipesArr = new RecipeClient2[proxyRecipesTotal];
            for (int proxyRecipesCount = 0; proxyRecipesCount < proxyRecipesTotal; ++proxyRecipesCount)
            {
                proxyRecipesArr[proxyRecipesCount] =
                    CreateProxyEntity(recipeRowsList[proxyRecipesCount]);
            }

            return proxyRecipesArr;
        }

        private RecipeClient2 CreateProxyEntity(RecipeRow recipeRow)
        {
            //*** map every field

            var recipeProxy = new RecipeClient2();

            recipeProxy.Seria = recipeRow.Seria;
            recipeProxy.Number = recipeRow.Number;
            recipeProxy.Snils = recipeProxy.Snils;
            recipeProxy.LpuOgrn = recipeRow.LpuOgrn;
            recipeProxy.LpuFoms = recipeRow.LpuFoms;
            recipeProxy.DoctorCode = recipeRow.DoctorCode;
            recipeProxy.MkbCode = recipeRow.MkbCode;
            recipeProxy.FundingSourceCode = int.Parse(recipeRow.FundingSourceCode);
            recipeProxy.NosologyCode = recipeRow.NosologyCode;
            recipeProxy.PrivilegeCode = recipeRow.PrivilegeCode;
            recipeProxy.ProgramName = recipeRow.Program;
            recipeProxy.ValidPeriodCode = recipeRow.ValidPeriodCode;
            recipeProxy.PayPercent = int.Parse(recipeRow.PayPercent);
            recipeProxy.MnnCode = recipeRow.MnnTrnCode;
            recipeProxy.TrnCode = recipeRow.MnnTrnCode;
            recipeProxy.CureformCode = recipeRow.CureformCode;
            recipeProxy.IsVk = recipeRow.IsVk;
            recipeProxy.Dosage = recipeRow.Dosage;
            recipeProxy.Quantity = recipeRow.Quantity;
            recipeProxy.IssueDate = recipeRow.IssueDate;
            recipeProxy.IncomeDate = recipeRow.IncomeDate;
            recipeProxy.SaleDate = recipeRow.SaleDate;
            recipeProxy.ProductCode = recipeRow.ProductCode;
            recipeProxy.Price = recipeRow.Price;
            recipeProxy.PatientLastname = recipeRow.PatientLastname;
            recipeProxy.PatientFirstname = recipeRow.PatientFirstname;
            recipeProxy.PatientMiddlename = recipeRow.PatientMiddlename;
            recipeProxy.PatientBirthday = recipeRow.PatientBirthday;
            recipeProxy.PatientSex = recipeRow.PatientSex;

            return recipeProxy;

            /** DATA FIELDS LIST 
            * 
               Seria
               Number
               Snils
               LpuOgrn
               LpuFoms
               DoctorCode
               MkbCode
               FundingSourceCode
               NosologyCode
               PrivilegeCode
               Program
               ValidPeriodCode
               PayPercent
               MnnCode
               TrnCode
               CureformCode
               IsVk
               Dosage
               Quantity
               IssueDate
               IncomeDate
               SaleDate
               DocumentState
               ProductCode
               ProductSeria
               BestBefore
               QuantitySale
               Price
               BestBefore
               PatientLastname
               PatientFirstname
               PatientMiddlename
               PatientBirthday
               PatientSex
               PatientCard
               PatientPolicy
               PatientAddress
               CredentialType
               CredentialNumber
               PrivilegeDocumentType
               PrivilegeDocumentDateStart
               PrivilegeDocumentDateFinish
            **/

            /** *** CHECK THIS FIELDS
             * 
             * recipeProxy.DocumnentState
             * recipeProxy.SaleDateSpecified
             * recipeProxy.QuantityUnit 
             * recipeProxy.IssueDateSpecified
             * recipeProxy.SaleDateSpecified
             */
        }



        // SERVICE RESPONSE CREATION SECTION
        //-------------------------------------------------

        /** Old version without error codes
        private ServiceResponse<RecipeRow> ConvertWebServiceResponce(RecipeClient2Out[] webServiceResponse,
            IEnumerable<RecipeRow> recipesBatch)
        {
            if (webServiceResponse == null || webServiceResponse.Length == 0)
            {
                return null;
            }
                
            var webServiceErrors = GetWebServiceErrors(webServiceResponse);
            var serviceResponse = new ServiceResponse<RecipeRow>()
            {
                RejectedBatch = recipesBatch,
                WebServiceErrors = webServiceErrors
            };

            return serviceResponse;
        }

        private IEnumerable<WebServiceResponse> GetWebServiceErrors(RecipeClient2Out[] webServiceResponse)
        {
            var webServiceErrors = new List<WebServiceResponse>();
            foreach (var error in webServiceResponse)
            {
                webServiceErrors.Add(
                    new WebServiceResponse()
                    {
                        Id = error.Id,
                        ErrorCode = error.ErrorCode,
                        ErrorMessage = error.ErrorMsg
                    });
            }

            return webServiceErrors;
        }

        private ServiceResponse<RecipeRow> CreateErrorResponse(Exception ex, IEnumerable<RecipeRow> entityList)
        {
            //*** return error state   
            var webServiceError = new WebServiceResponse()
            {
                ErrorMessage = ex.InnerException?.Message ?? ex.Message,
                ErrorCode = string.Empty,
                Id = string.Empty
            };
            return new ServiceResponse<RecipeRow>()
            {
                RejectedBatchName = "recipes_" + DateTime.Today.ToShortDateString(),
                RejectedBatch = entityList,
                WebServiceErrors = new[] { webServiceError }
            };
        }
        */

        //*** sipmlify this method (try to replace to the base class) same method in RemainService.cs
        private ServiceResponse<RecipeRow> ConvertWebServiceResponce(RecipeClient2Out[] webServiceResponse,
            IEnumerable<RecipeRow> recipesBatch)
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
            foreach (var responseEntity in webServiceResponse)
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

            return new ServiceResponse<RecipeRow>()
            {
                WebServiceMessages = webServiceMessages,
                RejectedBatch = null,
                RejectedBatchName = string.Empty
            };
        }

        #endregion Pivate helper methods


        #region Private helper classes

        //*** try Dictionary<int,int> instead of this class
        private class RecipeRange
        {
            public int StartIndex { get; set; }
            public int Range { get; set; }
        }

        #endregion Private helper classes
    }
}
