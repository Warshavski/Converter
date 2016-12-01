using System.Data;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Data.Ado.Extensions;
using System;

namespace Escyug.Converter.Data.Ado.Repositories
{
    public class RecipeRepository : Repository<RecipeRow>
    {

        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public RecipeRepository(DbContext dbContext)
            : base(dbContext)
        {

        }



        // PRIVATE/PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Repository<RecipeRow> protected members

        /// <summary>
        ///      Map data record to the domain entity (RecipeRow)
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="entity">RecipeRow entity instance</param>
        protected override void Map(IDataRecord record, RecipeRow entity)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Snils = record.TryGetFieldValue<string>("SS");
            entity.LpuOgrn = record.TryGetFieldValue<string>("C_OGRN");
            entity.ProductCode = record.TryGetFieldValue<string>("MCOD");
        }

        #endregion Repository<RecipeRow> protected members
    }
}
