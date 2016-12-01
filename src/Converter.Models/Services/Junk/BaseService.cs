using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Utils.XmlConverter;

namespace Escyug.Converter.Models.Services.Junk
{
    public enum ServiceState { SendComplete, SendError }

    public class ServiceEventArgs : EventArgs
    {
        public ServiceState State { get; set; }
        public string Message { get; set; }
    }

    /*** TODO :
     *  1. create events :
     *      - OnError
     *      - OnBatchSend
     *      - Wrap exceptions
     */
    public abstract class BaseService<TEntity> 
        where TEntity : class
    {

        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private const int TrialsTotal = 3;
        private const int BatchSize = 100;
        
        private readonly IRepository<TEntity> _entityRepository;
        


        // EVENTS SECTION 
        //---------------------------------------------------------------------

        public event EventHandler<ServiceEventArgs> ServiceExecuting;



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        protected BaseService(IRepository<TEntity> entityRepository)
        {
            _entityRepository = entityRepository;
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region Public methods

        /// <summary>
        ///     Send data to the processing center
        /// </summary>
        /// <param name="entityFileName">Entities data</param>
        /// <param name="storeId">Drugstore in in processing center</param>
        /// <param name="xmlFileName">xml file name if error occurs</param>
        /// <returns>Operation result(true or false)</returns>
        public bool TryToSendData(string entityFileName, int storeId, out string xmlFileName)
        {
            // get entities from the storage
            var entityList = _entityRepository.GetAll(entityFileName);

            xmlFileName = string.Empty;

            // create batch list
            var entityBatchList = CreateBatchList(entityList);

            // convert every batch to the XML-entity(create xml batch)
            int batchTotal = entityBatchList.Count;
            bool isFirtsBatch = true;
            for (int batchCount = 0; batchCount < batchTotal; ++batchCount)
            {
                //var xmlFile = _xmlConverter.ConvertToXml(entityBatchList[batchCount]);

                //*** generate timestamp
                var timeStamp = 1;
                var args = new ServiceEventArgs();

                for (int trialCount = 0; trialCount < TrialsTotal; ++trialCount)
                {
                    SendBatch(entityBatchList[batchCount], "clientId", timeStamp, isFirtsBatch);
                    //var responseResult = CallWebService(entityXmlDocument);
                    //if (responseResult)
                    //{
                    //    args.State = ServiceState.SendComplete;
                    //    args.Message = "batch #" + timeStamp + " - send succeeded";
                    //    OnServiceExecuting(args);
                    //    return true;
                    //}

                    //args.State = ServiceState.SendError;
                    //args.Message = "batch #" + timeStamp + " - send failed";
                    //OnServiceExecuting(args);
                }
                isFirtsBatch = false;
                //return false;

                //if (!sendResult)
                //{
                //    xmlFileName = $"batch#{batchCount}_{DateTime.Today.ToShortDateString()}.xml";
                //    xmlFile.Save("rejected/" + xmlFileName);

                //    return false;
                //}
            }

            return true;
        }

        #endregion Public methods



        // PRIVATE/PROTECTED HELPER METHODS SECTION
        //---------------------------------------------------------------------

        // *BATCH CREATE* 
        //-------------------------------------------------

        #region Private helper methods

        /// <summary>
        ///     Creates entities batch list from entity list
        /// </summary>
        /// <param name="entityList">Entity list</param>
        /// <returns>Batch list of the entities</returns>
        private IList<IList<TEntity>> CreateBatchList(IList<TEntity> entityList)
        {
            var batchList = new List<IList<TEntity>>();

            var entityTotal = entityList.Count;
            if (entityTotal > BatchSize)
            {
                var batchTotal = entityTotal / BatchSize;
                for (int batchCount = 0; batchCount < batchTotal; ++batchCount)
                {
                    var startItemIndex = batchCount * BatchSize;
                    var batch = CreateBatch(entityList, BatchSize, startItemIndex);
                    batchList.Add(batch);
                }

                var entityResidue = entityTotal % BatchSize;
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
        private IList<TEntity> CreateBatch(IList<TEntity> batchData, int batchSize, int startItemIndex)
        {
            var batch = new List<TEntity>();
            var batchLastIndex = batchSize + startItemIndex;
            for (int batchItemCount = startItemIndex; batchItemCount < batchLastIndex; ++batchItemCount)
            {
                batch.Add(batchData[batchItemCount]);
            }

            return batch;
        }



        // *BATCH SEND* 
        //-------------------------------------------------
        
      

        private void OnServiceExecuting(ServiceEventArgs e)
        {
            ServiceExecuting?.Invoke(this, e);
        }

        #endregion Private helper methods


        #region Proteted members

        /// <summary>
        ///     Attempts to send the XML-entity on the web service
        /// </summary>
        /// <param name="batchData"></param>
        /// <param name="clientId"></param>
        /// <param name="timestamp"></param>
        /// <param name="isFirstBatch"></param>
        /// <returns></returns>
        protected abstract bool SendBatch(IList<TEntity> batchData, string clientId, long timestamp, bool isFirstBatch);

        protected abstract Task<bool> SendBatchAsync(IList<TEntity> batchData, string clientId, long timestamp, bool isFirstBatch);

        protected abstract bool CheckEntitiesInGuides(IList<TEntity> entityList, GuidesCollection guides);

        #endregion Proteted members
    }
}






/*
  /// <summary>
        ///     Send data to the processing center
        /// </summary>
        /// <param name="entityList">Entities data</param>
        /// <returns>Operation result(true or false)</returns>
        public bool SendData(IList<TEntity> entityList, int storeId)
        {
            // create batch list
            var entityBatchList = CreateBatchList(entityList);

            // convert every batch to the XML-entity(create xml batch)
            var batchTotal = entityBatchList.Count;
            for (int batchCount = 0; batchCount < batchTotal; ++batchCount)
            {
                var xmlBatch = CreateXmlBatch(entityBatchList[batchCount]);
                
                var sendResult = TryToSendBatch(xmlBatch, batchCount);
            }

            return true;
        }
 */