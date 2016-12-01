namespace Escyug.Converter.Models.Entities.Guides
{
    public class GuideEntity
    {
        public int Code { get; set; }

        /// <summary>
        ///     Код КФ (EXT_CODE)
        /// </summary>
        public string ExternalCode { get; set; }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;

                hash = hash * 23 + Code.GetHashCode();

                hash = hash * 23 + (string.IsNullOrEmpty(ExternalCode) ? 
                    0 : ExternalCode.GetHashCode());

                return hash;
            }
        }
    }
}
