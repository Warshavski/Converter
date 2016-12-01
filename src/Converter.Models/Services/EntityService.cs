using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Services.Exceptions;

namespace Escyug.Converter.Models.Services
{
    public abstract class EntityService<TEntity>
        where TEntity : class
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private const int TrialsTotal = 3;
        protected const int BatchSizeMax = 100;

        protected readonly Dictionary<string, string> ErrorCodes = new Dictionary<string, string>()
        {
            {"ERR|AUTH", "Ошибка аутентификации пользователя"},
            {"ERR|UNKNOWN", "Неизвестная ошибка"},
            {"ERR|PRODUCT_MANY", "По заданному коду ЛС найдено несколько записей"},
            {"ERR|PRODUCT_NOT_FOUND", "По переданному коду ЛС не найдено записей в справочнике Медикаменты"},
            {"ERR|PROGRAM_NOT_FOUND", "Программа не найдена"},
            {"ERR|RETAIL_PRICE_NULL", "Нулевая розничная цена"},
            {"ERR|UNIT_NOT_FOUND", "Не удалось определить коэф. деления"},
            {"ERR|QNT_REM_NULL", "Неопределенное значение кол-ва остатка"},
            {"ERR|STORE_UNDEF", "Не заполнено обязательное поле Склад"},
            {"ERR|PROGRAM_UNDEF", "Не заполнено обязательное поле Программа"},
            {"ERR|PRODUCT_UNDEF", "Не заполнено обязательное поле Продукт"},
            {"ERR|PRODUCT_SERIA_UNDEF", "Не заполнено обязательное поле Серия"},
            {"ERR|BEST_BEFORE_UNDEF", "Не заполнено обязательное поле Срок годности"},
            {"ERR|PRICE_UNDEF", "Не заполнено обязательное поле Цена"},
            {"ERR|PEOPLE_NOT_FOUND", "Пациент не найден по СНИЛС или ФИО+Пол+Дата рождения" },
            {"ERR|PEOPLE_MANY_FOUND", "Найдено больше 1 пациента в БД" },
            {"ERR|DOCTOR_NOT_FOUND", "Врач не найден" },
            {"ERR|DOCTOR_MANY_FOUND", "Найдено больше 1 врача в БД" },
            {"ERR|NOSOLOGY_NOT_FOUND", "Код нозологии передан, но не найден в БД" },
            {"ERR|NOSOLOGY_MANY_FOUND", "Найдено больше 1 нозологии по коду" },
            {"ERR|PRIVILEGE_DOCUMENT_NOT_FOUND", "Код категории не найден" },
            {"ERR|PRIVILEGE_CATEGORY_MANY_FOUND", "Найдено больше 1 категории по коду" },
            {"ERR|PRODUCT_MANY_FOUND", "Найдено больше 1 товара" },
            {"ERR|PRODUCT_UNIT_NOT_FOUND", "Не удалось подобрать коэффициент деления" },
            {"ERR|INCOME_DATE_UNDEF", "Не задана дата обращения" },
            {"ERR|SALE_DATE_UNDEF", "Не задана дата отпуска" },
            {"ERR|EXPIRATION_DATE_TRUE", "Дата отпуска рецепта больше, чем срок его действия" },
            {"ERR|RECIPE_NOT_BE_CHANGE", "Рецепт находится в статусе, не предусматривающем редактирование" },
            {"ERR|RECIPE_MANY_IN_PACK", "В пакете присутствуют дубли рецептов по серии и номеру" },
            {"ERR|DOCUMENT_STATE_WRONG", @"Передаваемый статус документа не соответствует передаваемым данным: например, 
                                            •	статус «Выписан», а информация об отпуске заполнена,
                                            •	причину аннулирования передали, а статус отличается от «Аннулирован»" },
            {"ERR|ANNULMENT_UNDEF", "Причина аннулирование не передана, а статус рецепта - Аннулирован" },
            {"ERR|ANNULMENT_NOT_FOUND", "Причина аннулирования не найдена в БД по коду" },
            {"ERR|MNN_TRN_SET", "В информации о выписке присутствует и МНН и ТН одновременно" },
            {"ERR|PRODUCT_NOT_HAS_MNN", "Не найдено МНН по коду" },
            {"ERR|PRODUCT_NOT_HAS_TRN", "Не найдено ТН по коду" },
            {"ERR|ISSUE_DATE_UNDEF", "Не задана дата выписки" },
            {"ERR|PAY_MOD_NOT_FOUND", "Не найден процент оплаты" },
            {"ERR|MKB_NOT_FOUND", "Не найден МКБ" },
            {"ERR|VALID_PERIOD_NOT_FOUND", "Не найден период действия рецепта" },
            {"ERR|SOURCE_NOT_FOUND", "Не найден источник финансирования" },
        };

        /** Remain error codes
        * 
           ERR|UNKNOWN	             Неизвестная ошибка
         
           ERR|PRODUCT_MANY	     По заданному коду ЛС найдено несколько записей
           ERR|PRODUCT_NOT_FOUND	 По переданному коду ЛС не найдено записей в справочнике Медикаменты  
           ERR|PROGRAM_NOT_FOUND	 Программа не найдена
           ERR|RETAIL_PRICE_NULL	 Нулевая розничная цена
           ERR|UNIT_NOT_FOUND	     Не удалось определить коэф. деления
           ERR|QNT_REM_NULL	     Неопределенное значение кол-ва остатка   
           ERR|STORE_UNDEF	         Не заполнено обязательное поле Склад
           ERR|PROGRAM_UNDEF	     Не заполнено обязательное поле Программа
           ERR|PRODUCT_UNDEF	     Не заполнено обязательное поле Продукт
           ERR|PRODUCT_SERIA_UNDEF	 Не заполнено обязательное поле Серия
           ERR|BEST_BEFORE_UNDEF	 Не заполнено обязательное поле Срок годности
           ERR|PRICE_UNDEF	         Не заполнено обязательное поле Цена
        */

        /** Recipe error codes
         * 
            ERR|PEOPLE_NOT_FOUND	             Пациент не найден по СНИЛС или ФИО+Пол+Дата рождения
            ERR|PEOPLE_MANY_FOUND	             Найдено больше 1 пациента в БД
            ERR|DOCTOR_NOT_FOUND	             Врач не найден
            ERR|DOCTOR_MANY_FOUND	             Найдено больше 1 врача в БД
            ERR|NOSOLOGY_NOT_FOUND	             Код нозологии передан, но не найден в БД
            ERR|NOSOLOGY_MANY_FOUND	             Найдено больше 1 нозологии по коду
            ERR|PRIVILEGE_DOCUMENT_NOT_FOUND	 Код категории не найден
            ERR|PRIVILEGE_CATEGORY_MANY_FOUND	 Найдено больше 1 категории по коду
            ERR|PRODUCT_NOT_FOUND	             Товар не найден
            ERR|PRODUCT_MANY_FOUND	             Найдено больше 1 товара
            ERR|PRODUCT_UNIT_NOT_FOUND	         Не удалось подобрать коэффициент деления
            ERR|INCOME_DATE_UNDEF	             Не задана дата обращения
            ERR|SALE_DATE_UNDEF	                 Не задана дата отпуска
            ERR|INCOME_DATE_OVER	             Дата обращения рецепта больше даты отпуска
            ERR|EXPIRATION_DATE_TRUE	         Дата отпуска рецепта больше, чем срок его действия
            ERR|RECIPE_NOT_BE_CHANGE	         Рецепт находится в статусе, не предусматривающем редактирование
            ERR|RECIPE_MANY_IN_PACK	             В пакете присутствуют дубли рецептов по серии и номеру
            ERR|DOCUMENT_STATE_WRONG	         Передаваемый статус документа не соответствует передаваемым данным: например, 
                                                    •	статус «Выписан», а информация об отпуске заполнена,
                                                    •	причину аннулирования передали, а статус отличается от «Аннулирован»
            ERR|ANNULMENT_UNDEF	                 Причина аннулирование не передана, а статус рецепта - Аннулирован
            ERR|ANNULMENT_NOT_FOUND	             Причина аннулирования не найдена в БД по коду
            ERR|MNN_TRN_SET	                     В информации о выписке присутствует и МНН и ТН одновременно
            ERR|PRODUCT_NOT_HAS_MNN	             Не найдено МНН по коду
            ERR|PRODUCT_NOT_HAS_TRN	             Не найдено ТН по коду
            ERR|ISSUE_DATE_UNDEF	             Не задана дата выписки
            ERR|PAY_MOD_NOT_FOUND	             Не найден процент оплаты
            ERR|MKB_NOT_FOUND	                 Не найден МКБ
            ERR|VALID_PERIOD_NOT_FOUND	         Не найден период действия рецепта
            ERR|SOURCE_NOT_FOUND	             Не найден источник финансирования
            ERR|PROGRAM_NOT_FOUND	             Не найдена программа
         */

        public string BatchName { get; protected set; }

        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        //*** or return check state
        public abstract bool CheckRequiredFields(IEnumerable<TEntity> entitiesList, GuidesIdsCollection guidesIds);

        public abstract XDocument CreateXmlFile(IEnumerable<TEntity> entitiesList);

        public ServiceResponse<TEntity> SendData(IList<TEntity> entityList, string clientId)
        {
            //*** create batch list
            //*** foreach batch convert to service entities
            //*** try to send converted batch
            
            // create batch list
            var entityBatchList = CreateBatchList(entityList);

            int batchTotal = entityBatchList.Count;
            bool isFirstBatch = true;
            for (int batchCount = 0; batchCount < batchTotal; ++batchCount)
            {
                ServiceResponse<TEntity> serviceResponse = null;
                for (int trialCount = 0; trialCount < TrialsTotal; ++trialCount)
                {
                    serviceResponse = TryToSendBatch(entityBatchList[batchCount],
                        batchCount, isFirstBatch, clientId);

                    //*** if service respoise is true(null) break 
                    if (serviceResponse == null)
                    {
                        break;
                    }
                }

                //*** if send false return error state
                if (serviceResponse != null)
                {
                    return serviceResponse;
                }

                isFirstBatch = false;
            }

            return null;
        }

        public async Task<ServiceResponse<TEntity>> SendDataAsync(IList<TEntity> entityList, string clientId)
        {
            //*** create batch list
            //*** foreach batch convert to service entities
            //*** try to send converted batch

            // create batch list
            var entityBatchList = CreateBatchList(entityList);

            int batchTotal = entityBatchList.Count;
            bool isFirstBatch = true;
            for (int batchCount = 0; batchCount < batchTotal; ++batchCount)
            {
                ServiceResponse<TEntity> serviceResponse = null;
                for (int trialCount = 0; trialCount < TrialsTotal; ++trialCount)
                {
                    serviceResponse = await TryToSendBatchAsync(entityBatchList[batchCount],
                        batchCount, isFirstBatch, clientId);

                    //*** if service respoise is true(null) break 
                    if (serviceResponse == null)
                    {
                        break;
                    }
                }

                //*** if send false return error state
                if (serviceResponse != null)
                {
                    return serviceResponse;
                }

                isFirstBatch = false;
            }

            return null;
        }


        // PRIVATE/PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Private helper methods


        // *BATCH CREATE*  
        //-------------------------------------------------

        //*** batch creation for multiline recipes (mark methods as virtual)

        /// <summary>
        ///     Creates entities batch list from entity list (default implementation)
        /// </summary>
        /// <param name="entityList">Entity list</param>
        /// <returns>Batch list of the entities</returns>
        protected virtual IList<IList<TEntity>> CreateBatchList(IList<TEntity> entityList)
        {
            var batchList = new List<IList<TEntity>>();

            var entityTotal = entityList.Count;
            if (entityTotal > BatchSizeMax)
            {
                var batchTotal = entityTotal / BatchSizeMax;
                for (int batchCount = 0; batchCount < batchTotal; ++batchCount)
                {
                    var startItemIndex = batchCount * BatchSizeMax;
                    var batch = CreateBatch(entityList, BatchSizeMax, startItemIndex);
                    batchList.Add(batch);
                }

                var entityResidue = entityTotal % BatchSizeMax;
                if (entityResidue != 0)
                {
                    var startItemIndex = entityTotal - entityResidue;
                    var batch = CreateBatch(entityList, entityResidue, startItemIndex);
                    batchList.Add(batch);
                }
            }
            else
            {
                var batch = CreateBatch(entityList, entityTotal, 0);
                batchList.Add(batch);
            }

            return batchList;
        }

        /// <summary>
        ///     Creates batch
        /// </summary>
        /// <param name="batchData"></param>
        /// <param name="batchSize"></param>
        /// <param name="startItemIndex"></param>
        /// <returns></returns>
        protected IList<TEntity> CreateBatch(IList<TEntity> batchData, int batchSize, int startItemIndex)
        {
            if (batchData == null)
            {
                return null;
            }

            var batch = new List<TEntity>();
            var batchLastIndex = batchSize + startItemIndex;
            for (int batchItemCount = startItemIndex; batchItemCount < batchLastIndex; ++batchItemCount)
            {
                batch.Add(batchData[batchItemCount]);
            }

            return batch;
        }

        #endregion Private helper methods


        #region Protected helper methods

        protected ServiceResponse<TEntity> CreateErrorResponse(Exception ex, IEnumerable<TEntity> entityList)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            var errorResponse = CreateErrorResponse(message, entityList);

            return errorResponse;
        }

        protected ServiceResponse<TEntity> CreateErrorResponse(string message, IEnumerable<TEntity> entityList)
        {
            //*** return error state   
            var webServiceError = new WebServiceResponse()
            {
                Id = string.Empty,
                Message = message
            };

            return CreateErrorResponse(new[] { webServiceError }, entityList);
        }

        protected ServiceResponse<TEntity> CreateErrorResponse(IEnumerable<WebServiceResponse> webServiceErrors,
            IEnumerable<TEntity> entityList)
        {
            return new ServiceResponse<TEntity>()
            {
                WebServiceMessages = webServiceErrors,
                RejectedBatchName = BatchName + "_" + DateTime.Today.ToShortDateString(),
                RejectedBatch = entityList
            };
        }

        protected abstract ServiceResponse<TEntity> TryToSendBatch(IList<TEntity> entityList,
           long timestamp, bool isFirstBatch, string clientId);

        protected abstract Task<ServiceResponse<TEntity>> TryToSendBatchAsync(IList<TEntity> entityList,
          long timestamp, bool isFirstBatch, string clientId);

        #endregion Protected helper methods
    }
}
