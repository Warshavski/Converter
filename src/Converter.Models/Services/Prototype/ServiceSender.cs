using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using log4net;

using Escyug.Converter.Common.Logging;
using Escyug.Converter.Models.Utils.XmlConverter;
using Escyug.Converter.Models.Entities;


namespace Escyug.Converter.Models.Services.Prototype
{
    public abstract class ServiceSender<TEntity> : IServiceSender<TEntity>
    {
        private const int TRIAL_TOTAL = 3;
        private const int BATCH_SIZE = 100;

        private readonly ILog _log;
        private readonly IXmlConverter<TEntity> _xmlConverter;

        protected ILog Log { get { return _log; } }
        
        protected ServiceSender(ILogManager logManager, IXmlConverter<TEntity> xmlConverter)
        {
            _log = logManager.GetLog(typeof(ServiceSender<TEntity>));
            _xmlConverter = xmlConverter;
        }

        public bool SendData(IList<TEntity> entityList, StoreCredentials credentials)
        {
            // create batch list 
            // convert batch to xml
            // try to send xml
            // return operation status
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Try to send data to the side service
        /// </summary>
        /// <param name="remainRows"></param>
        private bool TryToSend(XDocument entityXmlDocument, int timeStamp)
        {
            for (int trialCount = 0; trialCount < TRIAL_TOTAL; ++trialCount)
            {
                var responseResult = CallWebService(entityXmlDocument);
                if (responseResult)
                {
                    _log.Info("Batch #" + timeStamp + " - send succeeded");
                    return true;
                }
                else
                {
                    _log.Error("Batch #" + timeStamp + " - was rejected by service");
                }
            }

            return false;
        }

        protected abstract bool CallWebService(XDocument xmlDocument);
        


        // batch create section
        //---------------------------------------------------------------------

        /// <summary>
        ///     Creates entities batch list from entity list
        /// </summary>
        /// <param name="entityList">Entity list</param>
        /// <returns>Batch list of the entities</returns>
        private IList<IList<TEntity>> CreateBatchList(IList<TEntity> entityList)
        {
            var batchList = new List<IList<TEntity>>();

            var entityTotal = entityList.Count;
            if (entityTotal > BATCH_SIZE)
            {
                var batchTotal = entityTotal / BATCH_SIZE;
                for (int batchCount = 0; batchCount < batchTotal; ++batchCount)
                {
                    var startItemIndex = batchCount * BATCH_SIZE;
                    var batch = CreateBatch(entityList, BATCH_SIZE, startItemIndex);
                    batchList.Add(batch);
                }

                var entityResidue = entityTotal % BATCH_SIZE;
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
                batch.Add(batchData[batchItemCount]);

            return batch;
        }




        /* prototype version
         * 
         *  var batchList = CreateBatchList(entityList);

           for (int batchCount = 0; batchCount < batchList.Count; ++batchCount)
           {
               var xmlDoc = _xmlConverter.ConvertToXml(batchList[batchCount]);
               xmlDoc.Save("xml/test" + batchCount + ".xml");

               var isBatchSend = false;
               for (int trial = 0; trial < TRIAL_COUNT; ++trial)
               {
                   // try to send data
                   // if send succeeded then break
                   try
                   {
                       var responseResult = false; //*** ProxyClass.RemainResponse(xmlDoc);
                       if (trial == 1)
                       {
                           responseResult = true;
                       }

                       if (responseResult)
                       {
                           _log.Info("Send succeeded");
                           isBatchSend = true;
                           break;
                       }
                       else
                       {
                           _log.Error("Batch was rejected by service");
                       }
                   }
                   catch (XmlException ex)
                   {
                       _log.Error("Xml converter : " + ex.Message);
                   }
                   catch (TimeoutException ex)
                   {
                       _log.Error("Server unavailable : " + ex.Message);
                   }
               }

               if (!isBatchSend)
               {
                   break;
               }
           }
        * 
        */
    }
}
