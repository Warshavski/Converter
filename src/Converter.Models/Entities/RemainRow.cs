using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.Converter.Models.Entities
{
    [Serializable]
    public class RemainRow
    {
        // consignment section
        //---------------------------------------------------------------------
        
        /// <summary>
        ///     Идентификатор контрагента-владельца партии (необязательное)
        /// </summary>
        public int ContractorId { get; set; }

        /// <summary>
        ///     Идентификатор партии (необязательное)
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        ///     Идентификатор партии (необязательное)
        /// </summary>
        public string DrugstoreLotGuid { get; set; }

        /// <summary>
        ///     Ссылка на позицию контракта, к которому привязана партия (необязательное)
        /// </summary>
        public string ContractItemGuid { get; set; }



        // store section
        //---------------------------------------------------------------------
        
        /// <summary>
        ///     Наименование склада (отдела АО) на котором хранится партия
        /// </summary>
        public string Store { get; set; }

        /// <summary>
        ///     Идентификатор склада (отдела АО), на котором хранится партия
        /// </summary>
        public int StoreId { get; set; }



        // product section
        //---------------------------------------------------------------------

        /// <summary>
        ///     Идентификатор товара ЕС (необязательное)
        /// </summary>
        public string UnifyGuid { get; set; }

        /// <summary>
        ///     Код товара ЕС или Код товара в кодировке Кубаньфармации 
        ///      (определяется согласно настройкам контрагента в ПЦ)
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        ///     Наименование товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///     Наименование единицы измерения и коэффициента деления (необязательное)
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        ///     Серия и срок годности в виде «Серия ххх до дд.мм.гггг»
        /// </summary>
        public string Series { get; set; }

        /// <summary>
        ///     Наименование поставщика (необязательное)
        /// </summary>
        public string Supplier { get; set; }



        // document section
        //---------------------------------------------------------------------

        /// <summary>
        ///     Идентификатор документа-родителя(необязательное)
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        ///     Тип документа-родителя партии(необязательное)
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        ///     Номер документа-родителя (необязательное)
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        ///     Дата документа-родителя (необязательное)
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        ///     Идентификатор позиции документа-родителя (необязательное)
        /// </summary>
        public string DocumentItemId { get; set; }
        
        /// <summary>
        ///     Дата поставщика документа прихода (необязательное)
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        ///     Номер поставщика документа прихода (необязательное)
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        ///     Штрихкод внутренний (необязательное)
        /// </summary>
        public string Barcode { get; set; }



        // system section
        //---------------------------------------------------------------------

        /// <summary>
        ///     Всегда = 1 (необязательное)
        /// </summary>
        public int Numerator { get; set; }

        /// <summary>
        ///     Делитель упаковки (для целых = 1) (необязательное)
        /// </summary>
        public int Denominator { get; set; }



        // quantity section
        //---------------------------------------------------------------------

        /// <summary>
        ///     Кол-во прихода по партии (необязательное)
        /// </summary>
        public int QuantityAdd { get; set; }

        /// <summary>
        ///     Кол-во расхода по партии (необязательное)
        /// </summary>
        public int QuantitySub { get; set; }

        /// <summary>
        ///     Кол-во резерва по партии (необязательное)
        /// </summary>
        public int QuantityRes { get; set; }

        /// <summary>
        ///     Кол-во остатка по партии
        /// </summary>
        public decimal QuantityRem { get; set; }

        /// <summary>
        ///     Кол-во по приходной накладной (используется для расчёта факта поставок по ГК) (необязательное)
        /// </summary>
        public int QuantityInvoice { get; set; }

        /// <summary>
        ///     Кол-во по возвратам поставщику (используется для расчёта факта поставок по ГК) (необязательное)
        /// </summary>
        public int QuantityReturnSupplier { get; set; }



        // price section
        //---------------------------------------------------------------------

        /// <summary>
        ///     Ставка НДС изготовителя (необязательное)
        /// </summary>
        public decimal ProducerVat { get; set; }

        /// <summary>
        ///     Цена изготовителя (необязательное)
        /// </summary>
        public decimal ProducerPrice { get; set; }

        /// <summary>
        ///     НДС изготовителя (необязательное)
        /// </summary>
        public decimal ProducerPriceVat { get; set; }

        /// <summary>
        ///     Ставка НДС поставщика (необязательное)
        /// </summary>
        public decimal SupplierVat { get; set; }

        /// <summary>
        ///     Цена поставщика (необязательное)
        /// </summary>
        public decimal SupplierPrice { get; set; }

        /// <summary>
        ///     НДС поставщика (необязательное)
        /// </summary>
        public decimal SupplierPriceVat { get; set; }

        /// <summary>
        ///     Ставка НДС розничная (необязательное)
        /// </summary>
        public decimal RetailVat { get; set; }

        /// <summary>
        ///     Цена розничная
        /// </summary>
        public decimal RetailPrice { get; set; }

        /// <summary>
        ///     НДС розничная
        /// </summary>
        public decimal RetailPriceVat { get; set; }



        //*** section
        //---------------------------------------------------------------------

        /// <summary>
        ///     Дата изначальной приходной накладной (необязательное)
        /// </summary>
        public DateTime InternalInvoiceDate { get; set; }

        /// <summary>
        ///     Внутренний номер изначальной приходной накладной (необязательное)
        /// </summary>
        public string InternalInvoiceNumber { get; set; }

        /// <summary>
        ///     Количество прихода по партии на основании перемещений (необязательное)
        /// </summary>
        public decimal QuantityMovementAdd { get; set; }

        /// <summary>
        ///     Количество расхода по партии на основании перемещений (необязательное)
        /// </summary>
        public decimal QuantityMovementSub { get; set; }

        /// <summary>
        ///     Краткое наименование программы льгот, к которой относится партия
        /// </summary>
        public string ProgramName { get; set; }


        /* First variant (like in xml)
         * 
        public string GUID { get; set; }
        public int CONTRACTOR_ID { get; set; }
        public string DRUGSTORE_LOT_GUID { get; set; }
        public string UNIFY_GUID { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string CONTRACT_ITEM_GUID { get; set; }
        public string STORE { get; set; }
        public int STORE_ID { get; set; }
        public string SUPPLIER { get; set; }
        public string PRODUCT { get; set; }
        public string UNIT { get; set; }
        public string SERIA { get; set; }
        public string DOCUMENT_TYPE { get; set; }
        public string DOCUMENT_ID { get; set; }
        public string DOCUMENT_NUMBER { get; set; }
        public string DOCUMENT_ITEM_ID { get; set; }
        public DateTime DOCUMENT_DATE { get; set; }
        public DateTime INVOICE_DATE { get; set; }
        public string INVOICE_NUMBER { get; set; }
        public string BARCODE { get; set; }
        public int NUMERATOR { get; set; }
        public int DENOMINATOR { get; set; }
        public int QUANTITY_ADD { get; set; }
        public int QUANTITY_SUB { get; set; }
        public int QUANTITY_RES { get; set; }
        public decimal QUANTITY_REM { get; set; }
        public int QUANTITY_INVOICE { get; set; }
        public int QUANTITY_RETURN_SUPPLIER { get; set; }
        public decimal PRODUCER_VAT { get; set; }
        public decimal PRODUCER_PRICE { get; set; }
        public decimal PRODUCER_PRICE_VAT { get; set; }
        public decimal SUPPLIER_VAT { get; set; }
        public decimal SUPPLIER_PRICE { get; set; }
        public decimal SUPPLIER_PRICE_VAT { get; set; }
        public decimal RETAIL_VAT { get; set; }
        public decimal RETAIL_PRICE { get; set; }
        public decimal RETAIL_PRICE_VAT { get; set; }
        public DateTime INTERNAL_INVOICE_DATE { get; set; }
        public string INTERNAL_INVOICE_NUMBER { get; set; }
        public decimal QUANTITY_MOVEMENT_ADD { get; set; }
        public decimal QUANTITY_MOVEMENT_SUB { get; set; }
        public string PROGRAM_NAME { get; set; }
        */

        /** Fields : name and type
         * 
        GUID String 
        CONTRACTOR_ID	Int
        DRUGSTORE_LOT_GUID	String
        UNIFY_GUID	String
        PRODUCT_CODE	String
        CONTRACT_ITEM_GUID	String
        STORE	String
        STORE_ID	Int
        SUPPLIER	String
        PRODUCT	String
        UNIT	String
        SERIA	String
        DOCUMENT_TYPE	String
        DOCUMENT_ID	String
        DOCUMENT_NUMBER	String
        DOCUMENT_ITEM_ID	String
        DOCUMENT_DATE	DateTime
        INVOICE_DATE	DateTime
        INVOICE_NUMBER	String
        BARCODE	String
        NUMERATOR	Int
        DENOMINATOR	Int
        QUANTITY_ADD	Int
        QUANTITY_SUB	Int
        QUANTITY_RES	Int
        QUANTITY_REM	Decimal
        QUANTITY_INVOICE	Int
        QUANTITY_RETURN_SUPPLIER	Int
        PRODUCER_VAT	Decimal
        PRODUCER_PRICE	Decimal
        PRODUCER_PRICE_VAT	Decimal
        SUPPLIER_VAT	Decimal
        SUPPLIER_PRICE	Decimal
        SUPPLIER_PRICE_VAT	Decimal
        RETAIL_VAT	Decimal
        RETAIL_PRICE	Decimal
        RETAIL_PRICE_VAT	Decimal
        INTERNAL_INVOICE_DATE	DateTime
        INTERNAL_INVOICE_NUMBER	String
        QUANTITY_MOVEMENT_ADD	Decimal
        QUANTITY_MOVEMENT_SUB	Decimal
        PROGRAM_NAME	String
        */
    }
}
