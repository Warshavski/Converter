using System;
using System.Data;

using Escyug.Converter.Data.Ado.Extensions;
using Escyug.Converter.Models.Entities.Guides;

namespace Escyug.Converter.Data.Ado.Repositories
{
    public class TradeNameRepository : GuideRepository<TradeName>
    {
        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public TradeNameRepository(DbContext dbContext)
            : base(dbContext)
        {

        }



        // * OVERRIDED * PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Repository<TradeName> protected members

        /// <summary>
        ///      Map data record to the domain entity (TradeName)
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="entity">TradeName entity instance</param>
        protected override void Map(IDataRecord record, TradeName entity)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Code = (int)record.GetRequedFieldValue<double>("C_TRN");
                        //(int)(double)record["C_TRN"];
            entity.RusName = record.TryGetFieldValue<string>("NAME_TRN_R");
            entity.LatName = record.TryGetFieldValue<string>("NAME_TRN_L");
            entity.Comment = record.TryGetFieldValue<string>("MSG_TEXT");
            entity.ExternalCode = record.TryGetFieldValue<string>("EXT_CODE");
        }

        #endregion Repository<TradeName> protected members


        #region GuideRepository<TradeName> protected members

        protected override int GetIdFromRecord(IDataRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            var tradeNameId = (int)record.GetRequedFieldValue<double>("C_TRN");
            return tradeNameId;
        }

        #endregion GuideRepository<TradeName> protected members
    }
}
