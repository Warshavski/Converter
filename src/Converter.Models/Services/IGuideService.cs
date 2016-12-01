using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Utils;

namespace Escyug.Converter.Models.Services
{
    public interface IGuideService
    {
        FileMetadata TryGetGuideFileMetadata(FtpClient ftpClient);
        void UpdateGuides(FtpClient ftpClient, FileMetadata fileMetadata);
        Task UpdateGuidesAsync(FtpClient ftpClient, FileMetadata fileMetadata);

        GuidesIdsCollection GetGuidesIds();
        Task<GuidesIdsCollection> GetGuidesIdsAsync();
    }
}
