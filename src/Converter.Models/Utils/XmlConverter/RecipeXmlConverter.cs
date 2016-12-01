using System.Collections.Generic;
using System.Xml.Linq;

using Escyug.Converter.Models.Entities;

namespace Escyug.Converter.Models.Utils.XmlConverter
{
    public class RecipeXmlConverter : IXmlConverter<RecipeRow>
    {
        public XDocument ConvertToXml(IList<RecipeRow> entityList)
        {
            var recipesDoc = new XDocument();

            var recipesRoot = new XElement("recipes");
            recipesDoc.Add(recipesRoot);

            foreach (var recipe in entityList)
            {
                var recipeRoot = new XElement("Recipe");

                recipeRoot.Add(new XElement("Seria", recipe.Seria));
                recipeRoot.Add(new XElement("Number", recipe.Number));
                recipeRoot.Add(new XElement("Snils", recipe.Snils));
                recipeRoot.Add(new XElement("LpuOgrn", recipe.LpuOgrn));
                recipeRoot.Add(new XElement("LpuFoms", recipe.LpuFoms));
                recipeRoot.Add(new XElement("DoctorCode", recipe.DoctorCode));
                recipeRoot.Add(new XElement("MkbCode", recipe.MkbCode));
                recipeRoot.Add(new XElement("FundingSourceCode", recipe.FundingSourceCode));
                recipeRoot.Add(new XElement("NosologyCode", recipe.NosologyCode));
                recipeRoot.Add(new XElement("PrivilegeCode", recipe.PrivilegeCode));

                recipeRoot.Add(new XElement("Program", recipe.Program));
                recipeRoot.Add(new XElement("ValidPeriodCode", recipe.Series));
                recipeRoot.Add(new XElement("PayPercent", recipe.PayPercent));
                recipeRoot.Add(new XElement("IsTrn", recipe.IsTrn));
                recipeRoot.Add(new XElement("MnnTrnCode", recipe.MnnTrnCode));
                recipeRoot.Add(new XElement("CureformCode", recipe.CureformCode));
                recipeRoot.Add(new XElement("IsVk", recipe.IsVk));
                recipeRoot.Add(new XElement("Dosage", recipe.Dosage));
                recipeRoot.Add(new XElement("Quantity", recipe.Quantity));
                recipeRoot.Add(new XElement("IssueDate", recipe.IssueDate));
                recipeRoot.Add(new XElement("IncomeDate", recipe.IncomeDate));
                recipeRoot.Add(new XElement("SaleDate", recipe.SaleDate));

                recipeRoot.Add(new XElement("Status", recipe.Status));
                recipeRoot.Add(new XElement("ProductCode", recipe.ProductCode));
                recipeRoot.Add(new XElement("QuantitySale", recipe.QuantitySale));
                recipeRoot.Add(new XElement("Price", recipe.Price));
                recipeRoot.Add(new XElement("Seria", recipe.Series));
                recipeRoot.Add(new XElement("BestBefore", recipe.BestBefore));
                recipeRoot.Add(new XElement("PatientLastname", recipe.PatientLastname));
                recipeRoot.Add(new XElement("PatientFirstname", recipe.PatientFirstname));
                recipeRoot.Add(new XElement("PatientMiddlename", recipe.PatientMiddlename));
                recipeRoot.Add(new XElement("PatientBirthday", recipe.PatientBirthday));
                recipeRoot.Add(new XElement("PatientSex", recipe.PatientSex));
                recipeRoot.Add(new XElement("PatientCard", recipe.PatientCard));
                recipeRoot.Add(new XElement("PatientPolicy", recipe.PatientPolicy));
                recipeRoot.Add(new XElement("PatientAddress", recipe.PatientAddress));
                recipeRoot.Add(new XElement("CredentialType", recipe.CredentialType));
                recipeRoot.Add(new XElement("CredentialNumber", recipe.CredentialNumber));
                recipeRoot.Add(new XElement("PrivilegeDocumentType", recipe.PrivilegeDocumentType));
                recipeRoot.Add(new XElement("PrivilegeDocumentDateStart", recipe.PrivilegeDocumentDateStart));
                recipeRoot.Add(new XElement("PrivilegeDocumentDateFinish", recipe.PrivilegeDocumentDateFinish));
                
                recipeRoot.Add(recipeRoot);
            }

            return recipesDoc;
        }

        /**
         *  Seria
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
            IsTrn
            MnnTrnCode
            CureformCode
            IsVk
            Dosage
            Quantity
            IssueDate
            IncomeDate
            SaleDate
            Status
            ProductCode
            QuantitySale
            Price
            Seria
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
    }
}
