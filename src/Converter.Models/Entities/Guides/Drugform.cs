namespace Escyug.Converter.Models.Entities.Guides
{
    /// <summary>
    ///     Строка справочника лекарственных форм (sp_lf.dbf)
    /// </summary>
    public class Drugform : GuideEntity
    {
        /// <summary>
        ///     Наитменование русское (NAME_LF)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///    Примечание (MSG_TEXT)
        /// </summary>
        public string Comment { get; set; }
    }
}
