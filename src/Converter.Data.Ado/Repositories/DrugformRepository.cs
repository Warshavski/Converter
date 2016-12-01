using System;
using System.Data;

using Escyug.Converter.Data.Ado.Extensions;
using Escyug.Converter.Models.Entities.Guides;

namespace Escyug.Converter.Data.Ado.Repositories
{
    public class DrugformRepository : GuideRepository<Drugform>
    {
        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public DrugformRepository(DbContext dbContext)
            : base(dbContext)
        {

        }


        // * OVERRIDED * PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Repository<MnnGuideRow> protected members

        /// <summary>
        ///      Map data record to the domain entity (RecipeRow)
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="entity">RecipeRow entity instance</param>
        protected override void Map(IDataRecord record, Drugform entity)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            if (entity == null)
            {
                throw  new ArgumentNullException(nameof(entity));
            }

            entity.Code = (int) record.GetRequedFieldValue<double>("C_LF");
            entity.Name = record.TryGetFieldValue<string>("NAME_LF");
            entity.Comment = record.TryGetFieldValue<string>("MSG_TEXT");
            entity.ExternalCode = record.TryGetFieldValue<string>("EXT_CODE");
        }

        #endregion Repository<MnnGuideRow> protected members


        #region GuideRepository<MnnGuideRow> protected members

        protected override int GetIdFromRecord(IDataRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            var drugformId = (int)record.GetRequedFieldValue<double>("C_LF");
            return drugformId;
        }

        #endregion GuideRepository<MnnGuideRow> protected members
    }
}
