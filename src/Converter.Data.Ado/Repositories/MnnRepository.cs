using System;
using System.Data;

using Escyug.Converter.Data.Ado.Extensions;
using Escyug.Converter.Models.Entities.Guides;

namespace Escyug.Converter.Data.Ado.Repositories
{
    public class MnnRepository : GuideRepository<Mnn>
    {

        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public MnnRepository(DbContext dbContext)
            : base(dbContext)
        {

        }


        // * OVERRIDED * PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Repository<Mnn> protected members

        /// <summary>
        ///      Map data record to the domain entity (Mnn)
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="entity">Mnn entity instance</param>
        protected override void Map(IDataRecord record, Mnn entity)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Code = (int)record.GetRequedFieldValue<double>("C_MNN");
                        //(int)record.TryGetFieldValue<double>("C_MNN");
            entity.RusName = record.TryGetFieldValue<string>("NAME_MNN_R");
            entity.LatName = record.TryGetFieldValue<string>("NAME_MNN_L");
            entity.Comment = record.TryGetFieldValue<string>("MSG_TEXT");
            entity.ExternalCode = record.TryGetFieldValue<string>("EXT_CODE");
        }

        #endregion Repository<Mnn> protected members


        #region GuideRepository<Mnn> protected members

        protected override int GetIdFromRecord(IDataRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            var mnnGuideRowId = (int)record.GetRequedFieldValue<double>("C_MNN");
            return mnnGuideRowId;
        }

        #endregion GuideRepository<Mnn> protected members
    }
}

