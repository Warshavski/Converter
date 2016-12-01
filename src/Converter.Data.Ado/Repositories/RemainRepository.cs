using System;
using System.Data;

using Escyug.Converter.Data.Ado.Extensions;
using Escyug.Converter.Models.Entities;

namespace Escyug.Converter.Data.Ado.Repositories
{
    public class RemainRepository : Repository<RemainRow>
    {

        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public RemainRepository(DbContext dbContext)
            : base(dbContext)
        {

        }



        // PRIVATE/PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Repository<RemainRow> protected members

        /// <summary>
        ///      Map data record to domain entity (RemainRow)
        /// </summary>
        /// <param name="record">Data record</param>
        /// <param name="entity">RemainRow entity instance</param>
        protected override void Map(IDataRecord record, RemainRow entity)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Guid = record.TryGetFieldValue<string>("GUID");
            entity.ContractorId = (int)record.TryGetFieldValue<double>("CONT_ID");
            entity.DrugstoreLotGuid = record.TryGetFieldValue<string>("DRST_GUID");
            entity.UnifyGuid = record.TryGetFieldValue<string>("UN_GUID");
            entity.ProductCode = record.GetRequedFieldValue<string>("PROD_CODE");
            entity.ContractItemGuid = record.TryGetFieldValue<string>("CONTR_GUID");
            entity.Store = record.GetRequedFieldValue<string>("STORE");
            entity.StoreId = (int)record.GetRequedFieldValue<double>("STORE_ID");
            entity.Supplier = record.TryGetFieldValue<string>("SUPPLIER");
            entity.ProductName = record.GetRequedFieldValue<string>("PRODUCT");

            entity.Unit = record.TryGetFieldValue<string>("UNIT");

            //entity.Series = record.GetRequedFieldValue<string>("SERIA");
            entity.Series = record.TryGetFieldValue<string>("SERIA");
            entity.DocumentType = record.TryGetFieldValue<string>("DOC_TYPE");
            entity.DocumentId = record.TryGetFieldValue<string>("DOC_ID");
            entity.DocumentNumber = record.TryGetFieldValue<string>("DOC_NUM");
            entity.DocumentItemId = record.TryGetFieldValue<string>("DOC_IT_ID");
            entity.DocumentDate = record.TryGetFieldValue<DateTime>("DOC_DATE");
            entity.InvoiceDate = record.TryGetFieldValue<DateTime>("INV_DATE");
            entity.InvoiceNumber = record.TryGetFieldValue<string>("INV_NUM");
            entity.Barcode = record.TryGetFieldValue<string>("BARCODE");
            entity.Numerator = (int)record.TryGetFieldValue<double>("NUMERATOR");
            entity.Denominator = (int)record.TryGetFieldValue<double>("DENO");


            /*** create test dbf
            entity.QuantityAdd = (int)record.TryGetFieldValue<double>("QNT_ADD"); 
            entity.QuantitySub = (int)record.TryGetFieldValue<double>("QNT_SUB"); 
            entity.QuantityRes = (int)record.TryGetFieldValue<double>("QNT_RES"); 
            entity.QuantityRem = record.GetRequedFieldValue<decimal>("QNT_REM");
            entity.QuantityInvoice = (int)record.TryGetFieldValue<double>("QNT_INV"); 
            entity.QuantityReturnSupplier = (int)record.TryGetFieldValue<double>("QNT_RET_SUP");
            entity.ProducerVat = record.TryGetFieldValue<decimal>("PROD_VAT");
            entity.ProducerPrice = record.TryGetFieldValue<decimal>("PROD_PR");
            entity.ProducerPriceVat = record.TryGetFieldValue<decimal>("PROD_PR_VAT");
            entity.SupplierVat = record.TryGetFieldValue<decimal>("SUP_VAT");
            entity.SupplierPrice = record.TryGetFieldValue<decimal>("SUP_PR");
            entity.SupplierPriceVat = record.TryGetFieldValue<decimal>("SUP_PR_VAT");
            entity.RetailVat = record.TryGetFieldValue<decimal>("RET_VAT");
            entity.RetailPrice = record.GetRequedFieldValue<decimal>("RET_PR");
            entity.RetailPriceVat = record.GetRequedFieldValue<decimal>("RETL_PR_VAT");
            entity.InternalInvoiceDate = record.TryGetFieldValue<DateTime>("INT_INV_DATE"); 
            entity.InternalInvoiceNumber = record.TryGetFieldValue<string>("INT_INV_NUMBER"); 
            entity.QuantityMovementAdd = record.TryGetFieldValue<decimal>("QNT_MOV_ADD"); 
            entity.QuantityMovementSub = record.TryGetFieldValue<decimal>("QNT_MOV_SUB"); 
            entity.ProgramName = record.GetRequedFieldValue<string>("PROG_NAME"); 
            */
        }

        #endregion Repository<RemainRow> protected members
    }
}




/*** prototype
// map data record to the domain entity
entity.GUID = (string)record["GUID"];
entity.CONTRACTOR_ID = (int)record["CONTRACTOR_ID"];
entity.DRUGSTORE_LOT_GUID = (string)record["DRUGSTORE_LOT_GUID"];
entity.UNIFY_GUID = (string)record["UNIFY_GUID"];
entity.PRODUCT_CODE = (string)record["PRODUCT_CODE"];
entity.CONTRACT_ITEM_GUID = (string)record["CONTRACT_ITEM_GUID"];
entity.STORE = (string)record["STORE"];
entity.STORE_ID = (int)record["STORE_ID"];
entity.SUPPLIER = (string)record["SUPPLIER"];
entity.PRODUCT = (string)record["PRODUCT"];
* 
entity.UNIT = (string)record["UNIT"];
entity.SERIA = (string)record["SERIA"];
entity.DOCUMENT_TYPE = (string)record["DOCUMENT_TYPE"];
entity.DOCUMENT_ID = (string)record["DOCUMENT_ID"];
entity.DOCUMENT_NUMBER = (string)record["DOCUMENT_NUMBER"];
entity.DOCUMENT_ITEM_ID = (string)record["DOCUMENT_ITEM_ID"];
entity.DOCUMENT_DATE = (DateTime)record["DOCUMENT_DATE"];
entity.INVOICE_DATE = (DateTime)record["INVOICE_DATE"];
entity.INVOICE_NUMBER = (string)record["INVOICE_NUMBER"];
entity.BARCODE = (string)record["BARCODE"];
entity.NUMERATOR = (int)record["NUMERATOR"];
entity.DENOMINATOR = (int)record["DENOMINATOR"];
* 
entity.QUANTITY_ADD = (int)record["QUANTITY_ADD"];
entity.QUANTITY_SUB = (int)record["QUANTITY_SUB"];
entity.QUANTITY_RES = (int)record["QUANTITY_RES"];
entity.QUANTITY_REM = (decimal)record["QUANTITY_REM"];
entity.QUANTITY_INVOICE = (int)record["QUANTITY_INVOICE"];
entity.QUANTITY_RETURN_SUPPLIER = (int)record["QUANTITY_RETURN_SUPPLIER"];
entity.PRODUCER_VAT = (decimal)record["PRODUCER_VAT"];
entity.PRODUCER_PRICE = (decimal)record["PRODUCER_PRICE"];
entity.PRODUCER_PRICE_VAT = (decimal)record["PRODUCER_PRICE_VAT"];
entity.SUPPLIER_VAT = (decimal)record["SUPPLIER_VAT"];
entity.SUPPLIER_PRICE = (decimal)record["SUPPLIER_PRICE"];
entity.SUPPLIER_PRICE_VAT = (decimal)record["SUPPLIER_PRICE_VAT"];
entity.RETAIL_VAT = (decimal)record["RETAIL_VAT"];
entity.RETAIL_PRICE = (decimal)record["RETAIL_PRICE"];
entity.RETAIL_PRICE_VAT = (decimal)record["RETAIL_PRICE_VAT"];
entity.INTERNAL_INVOICE_DATE = (DateTime)record["INTERNAL_INVOICE_DATE"];
entity.INTERNAL_INVOICE_NUMBER = (string)record["INTERNAL_INVOICE_NUMBER"];
entity.QUANTITY_MOVEMENT_ADD = (decimal)record["QUANTITY_MOVEMENT_ADD"];
entity.QUANTITY_MOVEMENT_SUB = (decimal)record["QUANTITY_MOVEMENT_SUB"];
entity.PROGRAM_NAME = (string)record["PROGRAM_NAME"]; 
*/