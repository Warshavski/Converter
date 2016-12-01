using System;

namespace Escyug.Converter.Models.Entities
{
    public class RecipeRow
    {

        #region Properties

        /// <summary>
        ///    Серия рецепта 
        /// </summary>
        public string Series { get; set; }

        /// <summary>
        ///   Номер рецепта  
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        ///     Снилс льготника (необязательное)
        /// </summary>
        public string Snils { get; set; }

        /// <summary>
        ///     Код ОГРН МО  
        /// </summary>
        public string LpuOgrn { get; set; }

        /// <summary>
        ///     Код МКОД МО
        /// </summary>
        public string LpuFoms { get; set; }

        /// <summary>
        ///     Личный код врача в МО
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        ///     Код МКБ   
        /// </summary>
        public string MkbCode { get; set; }

        /// <summary>
        ///     Код источника финансирования  
        /// </summary>
        public string FundingSourceCode { get; set; }

        /// <summary>
        ///     Код нозологии (необязательное)
        /// </summary>
        public string NosologyCode { get; set; }

        /// <summary>
        ///     Код категории льготы 
        /// </summary>
        public string PrivilegeCode { get; set; }

        /// <summary>
        ///     Программа льгот   
        /// </summary>
        public string Program { get; set; }

        /// <summary>
        ///     Код срока действия рецепта  
        /// </summary>
        public string ValidPeriodCode { get; set; }

        /// <summary>
        ///     Процент оплаты по рецепту  
        /// </summary>
        public string PayPercent { get; set; }

        /// <summary>
        ///     Способ выписки: true – по ТН, false – по МНН  
        /// </summary>
        public bool IsTrn { get; set; }

        /// <summary>
        ///     Код МНН или ТН, в зависимости от способа выписки
        /// </summary>
        public string MnnTrnCode { get; set; }

        /// <summary>
        ///     Код ЛФ  
        /// </summary>
        public string CureformCode { get; set; }

        /// <summary>
        ///     Признак ВК рецепта.   
        /// </summary>
        public bool IsVk { get; set; }

        /// <summary>
        ///     Дозировка в текстовом виде (поле DOSAGE справочника ЛП)
        /// </summary>
        public string Dosage { get; set; }

        /// <summary>
        ///     Количество выписанных единиц товара  
        /// </summary>
        public decimal Quantity { get; set; }


        private DateTime _issueDate;
        
        /// <summary>
        ///    Дата выписки 
        /// </summary>
        public DateTime IssueDate 
        {
            get { return _issueDate.Date; }
            set { _issueDate = value; }
        }


        private DateTime _incomeDate;

        /// <summary>
        ///    Дата обращения в АО 
        /// </summary>
        public DateTime IncomeDate
        {
            get { return _incomeDate.Date; }
            set { _incomeDate = value; }
        }


        private DateTime _saleDate;

        /// <summary>
        ///     Дата отпуска
        /// </summary>
        public DateTime SaleDate 
        {
            get { return _saleDate.Date; }
            set { _saleDate = value; } 
        }

        /// <summary>
        ///     Статус рецепта (возможные значения описаны ниже)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     Код ЕС или код КФ, в зависимости от настроек контрагента в ПЦ  
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        ///     Количество отпущенного товара в упаковках 
        /// </summary>
        public decimal QuantitySale { get; set; }

        /// <summary>
        ///    Цена за упаковку отпущенного товара 
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Серия производителя отпущенного товара
        /// </summary>
        public string Seria { get; set; }


        private DateTime _bestBefore;

        /// <summary>
        ///     Срок годности отпущенного товара (если известен только месяц и год, 
        ///     то датой срока годности считать 01 число месяца)  
        /// </summary>
        public DateTime BestBefore 
        {
            get { return _bestBefore.Date; }
            set { _bestBefore = value; }
        }

        /// <summary>
        ///    Фамилия пациента 
        /// </summary>
        public string PatientLastname { get; set; }

        /// <summary>
        ///     Имя пациента
        /// </summary>
        public string PatientFirstname { get; set; }

        /// <summary>
        ///     Отчество пациента
        /// </summary>
        public string PatientMiddlename { get; set; }


        private DateTime _patientBirthday;
        /// <summary>
        ///     Дата рождения пациента
        /// </summary>
        public DateTime PatientBirthday 
        {
            get { return _patientBirthday.Date; }
            set { _patientBirthday = value; }
        }

        /// <summary>
        ///     Пол пациента (W – женский, M – мужской)
        /// </summary>
        public string PatientSex { get; set; }

        /// <summary>
        ///     Номер амбулаторной карты пациента (необязательное)
        /// </summary>
        public string PatientCard { get; set; }

        /// <summary>
        ///     Полис ОМС (необязательное)  
        /// </summary>
        public string PatientPolicy { get; set; }

        /// <summary>
        ///     Адрес пациента (необязательное) 
        /// </summary>
        public string PatientAddress { get; set; }

        /// <summary>
        ///     Вид документа, удостоверяющего личность (необязательное)
        /// </summary>
        public string CredentialType { get; set; }

        /// <summary>
        ///     Серия и номер документа, удостоверяющего личность (необязательное)
        /// </summary>
        public string CredentialNumber { get; set; }

        /// <summary>
        ///     Тип документа льготы (необязательное)
        /// </summary>
        public string PrivilegeDocumentType { get; set; }


        private DateTime _privilegeDocumentDateStar;

        /// <summary>
        ///     Дата начала действия документа льготы (необязательное)
        /// </summary>
        public DateTime PrivilegeDocumentDateStart 
        {
            get { return _privilegeDocumentDateStar.Date; }
            set { _privilegeDocumentDateStar = value; }
        }


        private DateTime _privilegeDocumentDateFinish;

        /// <summary>
        ///     Дата окончания действия документа льготы (необязательное)
        /// </summary>
        public DateTime PrivilegeDocumentDateFinish 
        {
            get { return _privilegeDocumentDateFinish.Date; }
            set { _privilegeDocumentDateFinish = value; }
        }

        #endregion Properties

        

        #region Public overrided methods

        // EQUALITY SECTION
        //---------------------------------------------------------------------

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var recipe = obj as RecipeRow;
            if ((object)recipe == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Number == recipe.Number) && (Series == recipe.Series);
        }

        public bool Equals(RecipeRow recipe)
        {
            // If parameter is null return false:
            if ((object)recipe == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Number == recipe.Number) && (Series == recipe.Series);
        }


        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;

                hash = hash * 23 + (string.IsNullOrEmpty(Number) ?
                    0 : Number.GetHashCode());

                hash = hash * 23 + (string.IsNullOrEmpty(Series) ?
                    0 : Series.GetHashCode());

                return hash;
            }
        }

        #endregion Public overrided methods


        /** Fields and datatypes specification
         * 
            Seria	String
            Number	String
            Snils	String
            LpuOgrn	String
            LpuFoms	String
            DoctorCode	String
            MkbCode	String
            FundingSourceCode	String
            NosologyCode	String
            PrivilegeCode	String
            Program	String
            ValidPeriodCode	String
            PayPercent	String
            IsTrn	Bool
            MnnTrnCode	String
            CureformCode	String
            IsVk	Bool
            Dosage	String
            Quantity	Decimal
            IssueDate	Date
            IncomeDate	Date
            SaleDate	Date
            Status	Int
            ProductCode	String
            QuantitySale	Decimal
            Price	Decimal
            Seria	String
            BestBefore	Date
            PatientLastname	String
            PatientFirstname	String
            PatientMiddlename	String
            PatientBirthday	Date
            PatientSex	String
            PatientCard	String
            PatientPolicy	String
            PatientAddress	String
            CredentialType	String
            CredentialNumber	String
            PrivilegeDocumentType	String
            PrivilegeDocumentDateStart	Date
            PrivilegeDocumentDateFinish	Date
         * 
         */
    }

}
