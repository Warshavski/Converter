using Escyug.Converter.Common.Logging;
using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Utils.XmlConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Escyug.Converter.Models.RecipesServiceReference;
using System.Xml;
using System.Xml.Linq;

namespace Escyug.Converter.Models.Services.Junk
{
    public class RecipeService : BaseService<RecipeRow>
    {
         //*** private readonly RemainsProxyClass _proxyClass;
        private readonly RecipeServiceSoap _soapClient;
        

        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public RecipeService(RecipeServiceSoap soapClient, IRepository<RecipeRow> recipeRepository)
            : base(recipeRepository)
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

        protected override bool SendBatch(IList<RecipeRow> batchData, string clientId, long timeStamp, bool isFirtsBatch)
        {
            var recipesProxyArr = CreateProxyRemainsCollection(batchData);

            //*** check result
            var result = _soapClient.RecipesClient2Save(clientId, recipesProxyArr);
            

            //var rre = new RecipeClient2Out() { "errorCode, errorMessafe, id, number, seria"}

            //*** if false serialize to xml and save

            return false;
        }

        protected override async Task<bool> SendBatchAsync(IList<RecipeRow> batchData, string clientId, long timeStamp, bool isFirtsBatch)
        {
            var remainsProxyArr = CreateProxyRemainsCollection(batchData);

            //*** check result
            var result = await _soapClient.RecipesClient2SaveAsync(clientId, remainsProxyArr);
            

            //*** if false serialize to xml and save
            var xmlFile = CreateXmlFile(remainsProxyArr);
            var xmlFileName = $"batch#{timeStamp}_{DateTime.Today.ToShortDateString()}.xml";
            xmlFile.Save("rejected/" + xmlFileName);

            return false;
        }

        protected override bool CheckEntitiesInGuides(IList<RecipeRow> entityList, Entities.Guides.GuidesCollection guides)
        {
            throw new NotImplementedException();
        }

        #endregion BaseService members



        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Private methods

        private RecipeClient2[] CreateProxyRemainsCollection(IList<RecipeRow> recipesRows)
        {
            int proxyRecipesTotal = recipesRows.Count;
            var proxyRecipesArr = new RecipeClient2[proxyRecipesTotal];
            for (int proxyRecipesCount = 0; proxyRecipesCount < proxyRecipesTotal; ++proxyRecipesCount)
            {
                proxyRecipesArr[proxyRecipesCount] =
                    EntityToProxy(recipesRows[proxyRecipesCount]);
            }

            return proxyRecipesArr;
        }

        private RecipeClient2 EntityToProxy(RecipeRow recipeRow)
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


           

            /*** CHECK THIS FIELDS
             * 
             * recipeProxy.DocumnentState
             * recipeProxy.SaleDateSpecified
             * recipeProxy.QuantityUnit 
             * recipeProxy.IssueDateSpecified
             * recipeProxy.SaleDateSpecified
            */

            //recipeProxy.BestBefore
            //recipeProxy.BestBefore
            //recipeProxy.QuantitySale
            //recipeProxy.DocumnentState = 
            //recipeProxy.PatientCard
            //recipeProxy.PatientPolicy
            //recipeProxy.PatientAddress
            //recipeProxy.CredentialType
            //recipeProxy.CredentialNumber
            //recipeProxy.PrivilegeDocumentType
            //recipeProxy.PrivilegeDocumentDateStart
            //recipeProxy.PrivilegeDocumentDateFinish
            /*
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
            */



            return recipeProxy;
        }

        private XDocument CreateXmlFile(IEnumerable<RecipeClient2> recipesProxyList)
        {
            /*** with serialization */
            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                // write xml into the writer
                var serializer = new DataContractSerializer(recipesProxyList.GetType());
                serializer.WriteObject(writer, recipesProxyList);
            }
            
            return doc;

            /*** Without serialization
             * 
            var recipesDoc = new XDocument();

            var recipesRoot = new XElement("recipes");
            recipesDoc.Add(recipesRoot);

            foreach (var recipe in recipesProxyList)
            {
                var recipeRoot = new XElement("Recipe");

                recipeRoot.Add(new XElement("ProgramName", recipe.ProgramName));
                recipeRoot.Add(new XElement("Seria", recipe.ProgramName));
                recipeRoot.Add(new XElement("Number", recipe.ProgramName));
                recipeRoot.Add(new XElement("DoctorCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("LpuOgrn", recipe.ProgramName));
                recipeRoot.Add(new XElement("LpuFoms", recipe.ProgramName));
                recipeRoot.Add(new XElement("Snils", recipe.ProgramName));
                recipeRoot.Add(new XElement("PatientLastname", recipe.ProgramName));
                recipeRoot.Add(new XElement("PatientFirstname", recipe.ProgramName));
                recipeRoot.Add(new XElement("PatientMiddlename", recipe.ProgramName));
                recipeRoot.Add(new XElement("PatientBirthday", recipe.ProgramName));
                recipeRoot.Add(new XElement("PatientSex", recipe.ProgramName));
                recipeRoot.Add(new XElement("DocumnentState", recipe.ProgramName));
                recipeRoot.Add(new XElement("PrivilegeCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("FundingSourceCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("NosologyCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("MkbCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("IssueDate", recipe.ProgramName));
                recipeRoot.Add(new XElement("IsVk", recipe.ProgramName));
                recipeRoot.Add(new XElement("ValidPeriodCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("PayPercent", recipe.ProgramName));
                recipeRoot.Add(new XElement("MnnCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("TrnCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("ProductCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("Dosage", recipe.ProgramName));
                recipeRoot.Add(new XElement("CureformCode", recipe.ProgramName));
                recipeRoot.Add(new XElement("Quantity", recipe.ProgramName));
                recipeRoot.Add(new XElement("QuantityUnit", recipe.ProgramName));
                recipeRoot.Add(new XElement("SaleDate", recipe.ProgramName));
                recipeRoot.Add(new XElement("IncomeDate", recipe.ProgramName));
                recipeRoot.Add(new XElement("Price", recipe.ProgramName));
                recipeRoot.Add(new XElement("Id", recipe.ProgramName));

                recipeRoot.Add(recipeRoot);
            }

            return recipesDoc;
            */

            /*** XML-File example
             <clientId>string</clientId>
             <recipes>
               <RecipeClient2>
                 <ProgramName>string</ProgramName>
                 <Seria>string</Seria>
                 <Number>string</Number>
                 <DoctorCode>string</DoctorCode>
                 <LpuOgrn>string</LpuOgrn>
                 <LpuFoms>string</LpuFoms>
                 <Snils>string</Snils>
                 <PatientLastname>string</PatientLastname>
                 <PatientFirstname>string</PatientFirstname>
                 <PatientMiddlename>string</PatientMiddlename>
                 <PatientBirthday>dateTime</PatientBirthday>
                 <PatientSex>string</PatientSex>
                 <DocumnentState>string</DocumnentState>
                 <PrivilegeCode>string</PrivilegeCode>
                 <FundingSourceCode>int</FundingSourceCode>
                 <NosologyCode>string</NosologyCode>
                 <MkbCode>string</MkbCode>
                 <IssueDate>dateTime</IssueDate>
                 <IsVk>boolean</IsVk>
                 <ValidPeriodCode>string</ValidPeriodCode>
                 <PayPercent>int</PayPercent>
                 <MnnCode>string</MnnCode>
                 <TrnCode>string</TrnCode>
                 <ProductCode>string</ProductCode>
                 <Dosage>string</Dosage>
                 <CureformCode>string</CureformCode>
                 <Quantity>decimal</Quantity>
                 <QuantityUnit>int</QuantityUnit>
                 <SaleDate>dateTime</SaleDate>
                 <IncomeDate>dateTime</IncomeDate>
                 <Price>decimal</Price>
                 <Id>string</Id>
               </RecipeClient2>
            */

        }

        #endregion Private methods

    }
}
