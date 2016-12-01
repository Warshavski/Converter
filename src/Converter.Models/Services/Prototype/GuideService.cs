using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Utils;

namespace Escyug.Converter.Models.Services.Prototype
{
    public class GuideService : IGuideService
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private const int GuideNameIndex = 0;
        private const int GuideDateIndex = 1;


        #region IGuideRepository instances

        private readonly IGuideRepository<Mnn> _mnnGuideRepository;
        private readonly IGuideRepository<TradeName> _tradeNameGuideRepository;
        private readonly IGuideRepository<Drug> _drugGuideRepository;
        private readonly IGuideRepository<Drugform> _drugformGuideRepository;

        #endregion IGuideRepository instances



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public GuideService(IGuideRepository<Mnn> mnnGuideRepository,
            IGuideRepository<TradeName> tradeNameGuideRepository,
            IGuideRepository<Drug> drugGuideRepository,
            IGuideRepository<Drugform> drugformGuideRepository)
        {
            _mnnGuideRepository = mnnGuideRepository;
            _tradeNameGuideRepository = tradeNameGuideRepository;
            _drugGuideRepository = drugGuideRepository;
            _drugformGuideRepository = drugformGuideRepository;
        }



        // PUBLIC INTERFACE METHIODS SECTION
        //---------------------------------------------------------------------

        #region Guides update methods

        public IEnumerable<FileMetadata> GetMetadata(FtpClient ftpClient, string[] guidesNames)
        {
            var ftpFilesMetadataList = GetFilesMetadata(ftpClient);

            var filesMetadataTotal = ftpFilesMetadataList.Count();
            for (int fileMetadataCount = 0; fileMetadataCount < filesMetadataTotal; ++fileMetadataCount)
            {
                var name = guidesNames.FirstOrDefault(guideName => 
                    string.Equals(guideName, ftpFilesMetadataList[fileMetadataCount].Name));

                if (name == null)
                {
                    ftpFilesMetadataList.RemoveAt(fileMetadataCount);
                }
            }

            return ftpFilesMetadataList;
        }

        public IEnumerable<FileMetadata> GetMetadataForDownload(FtpClient ftpClient, 
            IDictionary<string, string[]> guidesInformation)
        {
            var ftpFilesMetadataList = GetFilesMetadata(ftpClient);

            var filesMetadataTotal = ftpFilesMetadataList.Count();
            for (int fileMetadataCount = 0; fileMetadataCount < filesMetadataTotal; ++fileMetadataCount)
            {
                var fileMetadata = ftpFilesMetadataList[fileMetadataCount];

                var guideInfo = guidesInformation.SingleOrDefault(g =>
                    g.Value[GuideNameIndex] == fileMetadata.Name &&
                    !string.Equals(g.Value[GuideDateIndex], fileMetadata.Date)).Key;

                if (guideInfo == null)
                {
                    ftpFilesMetadataList.RemoveAt(fileMetadataCount);
                }
            }

            return ftpFilesMetadataList;
        }

        public void DownloadGuides(FtpClient ftpClient, string localPath,
           IEnumerable<FileMetadata> guidesMetadataList)
        {
            Parallel.ForEach(guidesMetadataList, (item) =>
            {
                if (item.IsForUpdate)
                {
                    ftpClient.DownloadFile(item.Name, localPath);
                }
            });
        }

        #endregion Guides update methods


        #region Repositories methods wrappers

        public HashSet<int> GetMnnIds(string guideFileName)
        {
            var mnnGuideIds = _mnnGuideRepository.GetIds(guideFileName);
            return mnnGuideIds;
        }

        public HashSet<int> GetTradeNameIds(string guideFileName)
        {
            var tradeNameIds = _tradeNameGuideRepository.GetIds(guideFileName);
            return tradeNameIds;
        }

        public HashSet<int> GetDrugIds(string guideFileName)
        {
            var drugIds = _drugGuideRepository.GetIds(guideFileName);
            return drugIds;
        }

        public HashSet<int> GetDrugformIds(string guideFileName)
        {
            var drugformIds = _drugformGuideRepository.GetIds(guideFileName);
            return drugformIds;
        }

        #endregion Repositories methods wrappers


        #region Repositories async methods wrappers

        public async Task<HashSet<int>> GetMnnIdsAsync(string guideFileName)
        {
            var mnnGuideIds = await _mnnGuideRepository.GetIdsAsync(guideFileName);
            return mnnGuideIds;
        }

        public async Task<HashSet<int>> GetTradeNameIdsAsync(string guideFileName)
        {
            var tradeNameIds = await _tradeNameGuideRepository.GetIdsAsync(guideFileName);
            return tradeNameIds;
        }

        public async Task<HashSet<int>> GetDrugIdsAsync(string guideFileName)
        {
            var drugIds = await _drugGuideRepository.GetIdsAsync(guideFileName);
            return drugIds;
        }

        public async Task<HashSet<int>> GetDrugformIdsAsync(string guideFileName)
        {
            var drugformIds = await _drugformGuideRepository.GetIdsAsync(guideFileName);
            return drugformIds;
        }

        #endregion Repositories async methods wrappers



        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        private IList<FileMetadata> GetFilesMetadata(FtpClient ftpClient)
        {
            string[] ftpFolderMetadata = ftpClient.GetFtpFolderMetadata();

            IList<FileMetadata> ftpFilesMetadataList = ParseFolderMetadata(ftpFolderMetadata);

            return ftpFilesMetadataList;
        }

        private List<FileMetadata> ParseFolderMetadata(IEnumerable<string> folderMetadata)
        {
            var ftpListDirectoryDetailsRegex =
                new Regex(@".*(?<month>(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\s*(?<day>[0-9]*)\s*(?<yearTime>([0-9]|:)*)\s*(?<fileName>.*)",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var filesMetadataList = new List<FileMetadata>();
            foreach (var item in folderMetadata)
            {
                Match match = ftpListDirectoryDetailsRegex.Match(item);

                string fileName = match.Groups["fileName"].Value;

                var dateBuilder = new StringBuilder();
                dateBuilder.AppendFormat("{0}-{1} {2}",
                    match.Groups["day"].Value,
                    match.Groups["month"].Value,
                    match.Groups["yearTime"].Value);

                var fileMeta = new FileMetadata(fileName, dateBuilder.ToString());
                filesMetadataList.Add(fileMeta);
            }

            return filesMetadataList;
        } 
    }
}
