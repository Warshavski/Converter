namespace Escyug.Converter.Models.Entities.Guides
{ 
    /// <summary>
    ///     Строка справочника МНН (sp_mnn.dbf)
    /// </summary>
    public class Mnn : GuideEntity
    {
        /// <summary>
        ///     Наименование русское (NAME_MNN_R)
        /// </summary>
        public string RusName { get; set; }

        /// <summary>
        ///     Наименование латинское (NAME_MNN_L)
        /// </summary>
        public string LatName { get; set; }

        /// <summary>
        ///     Примечание (MSG_TEXT)
        /// </summary>
        public string Comment { get; set; }
    }
}
