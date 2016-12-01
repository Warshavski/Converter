namespace Escyug.Converter.Models.Entities.Guides
{
    /// <summary>
    ///     Строка справочника лекарственных препаратов (sp_tov.dbf)
    /// </summary>
    public class Drug : GuideEntity
    {
        /// <summary>
        ///     Наименование ЛП (NAME_MED)   
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Код ТН (C_TRN)   
        /// </summary>
        public int TradeNameCode { get; set; }

        /// <summary>
        ///     Код МНН (C_MNN)  
        /// </summary>
        public int MnnCode { get; set; }

        /// <summary>
        ///     Код ЛФ (C_LF)
        /// </summary>
        public int DrugformCode { get; set; }

        /// <summary>
        ///     Текстовое поле с дозировкой (D_LS)
        /// </summary>
        public string Dosage { get; set; }

        /// <summary>
        ///     Фасовка (N_FV)
        /// </summary>
        public int PrePacking { get; set; }

        /// <summary>
        ///     Производитель (NAME_FCT)
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        ///     Страна производителя (NAME_CNP)
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     Флаг ВК (FLAG_KEK)
        /// </summary>
        public int FlagVK { get; set; }

        /// <summary>
        ///     Признак привязки товара к ОНЛС (FLAG1)
        /// </summary>
        public int Flag1 { get; set; }

        /// <summary>
        ///     Признак привязки товара к программе РЛО, КЦП, Орфанные заболевания (FLAG2)
        /// </summary>
        public int Flag2 { get; set; }

        /// <summary>
        ///     Товар привязан к ВЗН (FLAG3)
        /// </summary>
        public int Flag3 { get; set; }

    }
}
