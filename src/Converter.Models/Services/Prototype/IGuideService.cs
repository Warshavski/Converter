using System.Collections.Generic;
using System.Threading.Tasks;

using Escyug.Converter.Models.Utils;

namespace Escyug.Converter.Models.Services.Prototype
{
    public interface IGuideService
    {
        IEnumerable<FileMetadata> GetMetadata(FtpClient ftpClient, string[] guidesNames);

        IEnumerable<FileMetadata> GetMetadataForDownload(FtpClient ftpClient,
            IDictionary<string, string[]> guidesInformation);

        void DownloadGuides(FtpClient ftpClient, string localPath,
            IEnumerable<FileMetadata> guidesMetadataList);

        HashSet<int> GetMnnIds(string guideFileName);
        HashSet<int> GetTradeNameIds(string guideFileName);
        HashSet<int> GetDrugIds(string guideFileName);
        HashSet<int> GetDrugformIds(string guideFileName);

        Task<HashSet<int>> GetMnnIdsAsync(string guideFileName);
        Task<HashSet<int>> GetTradeNameIdsAsync(string guideFileName);
        Task<HashSet<int>> GetDrugIdsAsync(string guideFileName);
        Task<HashSet<int>> GetDrugformIdsAsync(string guideFileName);
    }
}
