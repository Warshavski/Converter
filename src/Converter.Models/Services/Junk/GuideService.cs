using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Services.Exceptions;
using Escyug.Converter.Models.Utils;

namespace Escyug.Converter.Models.Services.Junk
{
    public class GuideServiceEventArgs : EventArgs
    {
        public object State { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    ///     Service thar loads and updates guides
    /// </summary>
    public class GuideService : IGuideService
    {

        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        #region Class private fields

        private readonly IConfigurationManager<FtpConfiguration> _ftpConfigManager;
        private readonly IConfigurationManager<GuidesConfiguration> _guidesConfigManager;

        private readonly IGuideRepository<Mnn> _mnnGuideRepository;
        private readonly IGuideRepository<TradeName> _tradeNameGuideRepository;
        private readonly IGuideRepository<Drug> _drugGuideRepository;
        private readonly IGuideRepository<Drugform> _drugformGuideRepository;

        #endregion Class private fields



        // EVENTS SECTION
        //---------------------------------------------------------------------

        public event EventHandler<GuideServiceEventArgs> StateChanged;



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public GuideService(
            IConfigurationManager<FtpConfiguration> ftpConfigManager, 
            IConfigurationManager<GuidesConfiguration> guidesConfigManager,
            IGuideRepository<Mnn> mnnGuideRepository,
            IGuideRepository<TradeName> tradeNameGuideRepository)
        {
            _ftpConfigManager = ftpConfigManager;
            _guidesConfigManager = guidesConfigManager;

            _mnnGuideRepository = mnnGuideRepository;
            _tradeNameGuideRepository = tradeNameGuideRepository;
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------
        
        #region IGuideService members

        public GuidesCollection GetGuides()
        {
            var serviceStateAgrs = new GuideServiceEventArgs();

            FtpConfiguration ftpConfiguration = _ftpConfigManager.Get();
            GuidesConfiguration guidesConfiguration = _guidesConfigManager.Get();

            var ftpClient = new FtpClient(ftpConfiguration.Uri,
                ftpConfiguration.User, ftpConfiguration.Password);


            string[] ftpFolderMetadata = ftpClient.GetFtpFolderMetadata();
            List<FileMeta> filesMetadata = ParseFtpFolderMetadata(ftpFolderMetadata);

            serviceStateAgrs.Message = "Checking guides for download.";
            OnStageChanged(serviceStateAgrs);

            var isForDownload = CheckGuidesForDownload(filesMetadata, guidesConfiguration.Guides);
            if (isForDownload)
            {
                try
                {
                    serviceStateAgrs.Message = "Downloading guides.";
                    OnStageChanged(serviceStateAgrs);

                    DownloadGuides(ftpClient, filesMetadata, guidesConfiguration.Path);
                    UpdateDates(filesMetadata, guidesConfiguration);
                }
                catch (HttpListenerException ex)
                {
                    throw new RemoteServerException(ex.Message, ex);
                }
                catch (IOException ex)
                {
                    throw new FileSaveException(ex.Message, ex);
                }
            }

            serviceStateAgrs.Message = "Loading guides.";
            OnStageChanged(serviceStateAgrs);

            var guides = LoadGuides(guidesConfiguration);

            return guides;
        }

        public GuidesIdsCollection GetGuidesIds()
        {
            GuidesConfiguration guidesConfiguration = _guidesConfigManager.Get();

            GuidesIdsCollection guides = LoadGuidesIds(guidesConfiguration);

            return guides;
        }

        public async Task<GuidesIdsCollection> GetGuidesIdsAsync()
        {
            GuidesConfiguration guidesConfiguration = _guidesConfigManager.Get();

            var guides = await LoadGuidesIdsAsync(guidesConfiguration);

            return guides;
        }

        #endregion



        // HELPER PRIVATE METHODS
        //---------------------------------------------------------------------

        #region Class helper methods

        private List<FileMeta> ParseFtpFolderMetadata(IEnumerable<string> metadataArray)
        {
            var ftpListDirectoryDetailsRegex =
                new Regex(@".*(?<month>(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\s*(?<day>[0-9]*)\s*(?<yearTime>([0-9]|:)*)\s*(?<fileName>.*)", 
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var filesMeta = new List<FileMeta>();
            foreach (var item in metadataArray)
            {
                Match match = ftpListDirectoryDetailsRegex.Match(item);

                string fileName = match.Groups["fileName"].Value;

                var dateBuilder = new StringBuilder();
                dateBuilder.AppendFormat("{0}-{1} {2}",
                    match.Groups["day"].Value,
                    match.Groups["month"].Value,
                    match.Groups["yearTime"].Value);

                var fileMeta = new FileMeta(fileName, dateBuilder.ToString());
                filesMeta.Add(fileMeta);
            }

            return filesMeta;
        }

        private bool CheckGuidesForDownload(IList<FileMeta> filesMetadata, Dictionary<string, string[]> guidesData)
        {
            var isForDownload = false;

            for (int i = 0; i < filesMetadata.Count; ++i)
            {
                var ftpFile = filesMetadata[i];

                var guide = guidesData.SingleOrDefault(x => x.Value[0] == ftpFile.Name &&
                    String.CompareOrdinal(x.Value[1], ftpFile.Date) != 0).Key;

                if (guide != null)
                {
                    ftpFile.IsForUpdate = true;
                    isForDownload = true;
                }
            }

            return isForDownload;
        }

        private void DownloadGuides(FtpClient ftpClient, IList<FileMeta> metadataList, string localPath)
        {
            
            Parallel.ForEach(metadataList, (item) =>
            {
                if (item.IsForUpdate)
                {
                    ftpClient.DownloadFile(item.Name, localPath);
                }
            });
        }

        private void UpdateDates(IList<FileMeta> filesMetaData, GuidesConfiguration guidesConfiguration)
        {
            for (int i = 0; i < filesMetaData.Count; ++i)
            {
                var ftpFile = filesMetaData[i];
                if (ftpFile.IsForUpdate)
                {
                    var guideName = guidesConfiguration.Guides.
                        SingleOrDefault(x => x.Value[0] == ftpFile.Name).Key;

                    guidesConfiguration.Guides[guideName][1] = ftpFile.Date;
                }
            }

            _guidesConfigManager.Update(guidesConfiguration);
        }

        private GuidesCollection LoadGuides(GuidesConfiguration guidesConfiguration)
        {
            var mnnFile = guidesConfiguration.Guides["mnn"][0];
            var mnnGuide = _mnnGuideRepository.GetAll(mnnFile);
            
            var tradeNameFile = guidesConfiguration.Guides["tradeName"][0];
            var tradeNameGuide = _tradeNameGuideRepository.GetAll(tradeNameFile);

            var guidesCollection = CreateGuidesCollecion(mnnGuide, tradeNameGuide);

            return guidesCollection;
        }

        private async Task<GuidesCollection> LoadGuideAsync(GuidesConfiguration guidesConfiguration)
        {
            var mnnFile = guidesConfiguration.Guides["mnn"][0];
            var mnnGuide = await _mnnGuideRepository.GetAllAsync(mnnFile);

            var tradeNameFile = guidesConfiguration.Guides["trade"][0];
            var tradeNameGuide = await _tradeNameGuideRepository.GetAllAsync(tradeNameFile);

            var guidesCollection = CreateGuidesCollecion(mnnGuide, tradeNameGuide);

            return guidesCollection;
        }

        private GuidesIdsCollection LoadGuidesIds(GuidesConfiguration guidesConfiguration)
        {
            var mnnFile = guidesConfiguration.Guides["mnn"][0];
            var mnnGuideIds = _mnnGuideRepository.GetIds(mnnFile);

            var tradeNameFile = guidesConfiguration.Guides["tradeName"][0];
            var tradeNameGuideIds = _tradeNameGuideRepository.GetIds(tradeNameFile);

            var guidesIdsCollection = CreateGuidesIdsCollecion(mnnGuideIds, tradeNameGuideIds);

            return guidesIdsCollection;
        }
        
        private async Task<GuidesIdsCollection> LoadGuidesIdsAsync(GuidesConfiguration guidesConfiguration)
        {
            var mnnFile = guidesConfiguration.Guides["mnn"][0];
            var mnnGuideIds = await _mnnGuideRepository.GetIdsAsync(mnnFile);

            var tradeNameFile = guidesConfiguration.Guides["tradeName"][0];
            var tradeNameGuideIds = await _tradeNameGuideRepository.GetIdsAsync(tradeNameFile);

            var guidesIdsCollection = CreateGuidesIdsCollecion(mnnGuideIds, tradeNameGuideIds);

            return guidesIdsCollection;
        }

        
        private GuidesCollection CreateGuidesCollecion(IEnumerable<Mnn> mnnGuide,
            IEnumerable<TradeName> tradeNameGuide)
        {
            var guidesCollection = new GuidesCollection(new HashSet<Mnn>(mnnGuide),
                new HashSet<TradeName>(tradeNameGuide));

            return guidesCollection;
        }

        private GuidesIdsCollection CreateGuidesIdsCollecion(HashSet<int> mnnGuideIds,
           HashSet<int> tradeNameGuideIds)
        {
            var guidesIdsCollection = new GuidesIdsCollection()
            {
                MnnGuideIs = mnnGuideIds,
                TradeNamesGudeIds = tradeNameGuideIds
            };

            return guidesIdsCollection;
        }

        private void OnStageChanged(GuideServiceEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        #endregion Class helper methods



        // HELPER PRIVATE NESTED CLASSES SECTION
        //---------------------------------------------------------------------

        /// <summary>
        ///     Helper class for metadata parsing
        /// </summary>
        private class FileMeta
        {
            /// <summary>
            ///     File name with extension
            /// </summary>
            public string Name { get; }

            /// <summary>
            ///     Create(update) file date
            /// </summary>
            public string Date { get; }

            /// <summary>
            ///     A flag that indicates the need to update
            /// </summary>
            public bool IsForUpdate { get; set; }

            /// <summary>
            ///     File metadata constructor
            /// </summary>
            /// <param name="name">File name</param>
            /// <param name="date">Create(update) file date</param>
            public FileMeta(string name, string date)
            {
                Name = name;
                Date = date;
                IsForUpdate = false;
            }
        }

    }
}
