using System;
using System.Data;

using Escyug.Converter.Data.Ado.Extensions;
using Escyug.Converter.Models.Entities.Guides;

namespace Escyug.Converter.Data.Ado.Repositories
{
    public class DrugRepository : GuideRepository<Drug>
    {
        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public DrugRepository(DbContext dbContext)
            : base(dbContext)
        {

        }


        // * OVERRIDED * PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Repository<Drug> protected members

        /// <summary>
        ///      Map data record to the domain entity (Drug)
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="entity">Drug entity instance</param>
        protected override void Map(IDataRecord record, Drug entity)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            if (entity == null)
            {
                throw  new ArgumentNullException(nameof(entity));
            }

            entity.Code = (int) record.GetRequedFieldValue<double>("NOMK_LS");
            entity.Name = record.TryGetFieldValue<string>("NAME_MED");
            entity.TradeNameCode = (int) record.TryGetFieldValue<double>("C_TRN");
            entity.MnnCode = (int) record.TryGetFieldValue<double>("C_MNN");
            entity.DrugformCode = (int) record.TryGetFieldValue<double>("C_LF");
            entity.Dosage = record.TryGetFieldValue<string>("D_LS");
            entity.PrePacking = (int) record.TryGetFieldValue<double>("N_FV");
            entity.Manufacturer = record.TryGetFieldValue<string>("NAME_FCT");
            entity.Country = record.TryGetFieldValue<string>("NAME_CNP");
            entity.FlagVK = (int) record.TryGetFieldValue<double>("FLAG_KEK");
            entity.Flag1 = (int) record.TryGetFieldValue<double>("FLAG1");
            entity.Flag2 = (int) record.TryGetFieldValue<double>("FLAG2");
            entity.Flag3 = (int) record.TryGetFieldValue<double>("FLAG3");
            entity.ExternalCode = record.TryGetFieldValue<string>("EXT_CODE");
        }

        #endregion Repository<Drug> protected members


        #region GuideRepository<Drug> protected members

        protected override int GetIdFromRecord(IDataRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            var drugId = (int)record.GetRequedFieldValue<double>("NOMK_LS");
            return drugId;
        }

        #endregion GuideRepository<Drug> protected members
    }
}
