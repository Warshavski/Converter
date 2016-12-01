namespace Escyug.Converter.Models.Entities.Guides
{
    /// <summary>
    ///     Строка справочника торговых наименований (sp_trn.dbf)
    /// </summary>
    public class TradeName : GuideEntity
    {
        /// <summary>
        ///     Наименование русское (NAME_TRN_R)
        /// </summary>s
        public string RusName { get; set; }

        /// <summary>
        ///     Наименование латинское (NAME_TRN_L)
        /// </summary>
        public string LatName { get; set; }

        /// <summary>
        ///     Примечание (MSG_TEXT)
        /// </summary>
        public string Comment { get; set; }
    }
}
