using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Entities.Guides;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Services.Exceptions;
using Escyug.Converter.Models.Utils;

namespace Escyug.Converter.Models.Services
{
    /// <summary>
    ///     Helper class for metadata parsing
    /// </summary>
    public class FileMetadata
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
        internal FileMetadata(string name, string date)
        {
            Name = name;
            Date = date;
            IsForUpdate = false;
        }
    }


    public class GuideService : IGuideService
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        //*** NOT USED
        private const int GuideNameIndex = 0;

        //*** NOT USED
        private const int GuideDateIndex = 1;


        private readonly IConfigurationManager<GuidesConfiguration> _guidesConfigurationManager;

        #region IGuideRepositories

        private readonly IGuideRepository<Mnn> _mnnGuideRepository;
        private readonly IGuideRepository<TradeName> _tradeNameGuideRepository;
        private readonly IGuideRepository<Drug> _drugGuideRepository;
        private readonly IGuideRepository<Drugform> _drugformGuideRepository;

        #endregion IGuideRepositories


        private GuidesConfiguration _guidesConfiguration;



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------

        public GuideService(IGuideServiceDependencyBlock guideServiceDependency)
        {
            _guidesConfigurationManager = guideServiceDependency.GuidesConfigurationManager;

            _mnnGuideRepository = guideServiceDependency.MnnGuideRepository;
            _tradeNameGuideRepository = guideServiceDependency.TradeNameGuideRepository;
            _drugGuideRepository = guideServiceDependency.DrugGuideRepository;
            _drugformGuideRepository = guideServiceDependency.DrugformGuideRepository;

            // guide config initialization
            _guidesConfiguration = _guidesConfigurationManager.Get();
        }



        // PUBLIC INTERFACE METHIODS SECTION
        //---------------------------------------------------------------------

        public FileMetadata TryGetGuideFileMetadata(FtpClient ftpClient)
        {
            if (ftpClient == null)
            {
                throw new ArgumentNullException(nameof(ftpClient));
            }

            var latestFtpFileMetadata = GetLatestFileMetadata(ftpClient);

            var lastUpdateDate = _guidesConfiguration.LastUpdateDate;
            if (latestFtpFileMetadata != null &&
                !string.Equals(latestFtpFileMetadata.Date, lastUpdateDate))
            {
                return latestFtpFileMetadata;
            }

            return null;
        }

        public void UpdateGuides(FtpClient ftpClient, FileMetadata fileMetadata)
        {
            if (ftpClient == null)
            {
                throw new ArgumentNullException(nameof(ftpClient));
            }

            CheckMetadataParameter(fileMetadata);

            try
            {
                var fileName = fileMetadata.Name;
                var localPath = _guidesConfiguration.Path;
                
                ftpClient.DownloadFile(fileName, localPath);

                UnZip(fileName, localPath, true);
                DeleteFile(localPath, fileName);

                _guidesConfiguration.LastUpdateDate = fileMetadata.Date;
                _guidesConfigurationManager.Update(_guidesConfiguration);
            }
            catch (WebException ex)
            {
                throw new RemoteServerException(
                    "Guides service not respond" + Environment.NewLine + ex.Message, ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new FileSaveException(
                    "Guides folder not found" + Environment.NewLine + ex.Message, ex);
            }
            catch (IOException ex)
            {
                throw new FileSaveException(ex.Message, ex);
            }

            /**
            catch (AggregateException ae)
            {
                // This is where you can choose which exceptions to handle.
                foreach (var ex in ae.InnerExceptions)
                {
                    if (ex is System.Net.WebException)
                    {
                        throw new RemoteServerException(
                            "Guides service not respond" + Environment.NewLine + ex.Message, ex);
                    }

                    if (ex is System.IO.DirectoryNotFoundException)
                    {
                        throw new FileSaveException(
                            "Guides folder not found" + Environment.NewLine + ex.Message, ex);
                    }

                    throw ex;
                }
            }
            */
        }


        public async Task UpdateGuidesAsync(FtpClient ftpClient, FileMetadata fileMetadata)
        {
            if (ftpClient == null)
            {
                throw new ArgumentNullException(nameof(ftpClient));
            }

            CheckMetadataParameter(fileMetadata);
            
            try
            {
                var localPath = _guidesConfiguration.Path;
                var fileName = fileMetadata.Name;

                await  ftpClient.DownloadFileAsync(fileName, localPath);

                UnZip(fileName, localPath, true);
                DeleteFile(localPath, fileName);

                _guidesConfiguration.LastUpdateDate = fileMetadata.Date;
                _guidesConfigurationManager.Update(_guidesConfiguration);
            }
            catch (WebException ex)
            {
                throw new RemoteServerException(
                    "Guides service not respond" + Environment.NewLine + ex.Message, ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new FileSaveException(
                    "Guides folder not found" + Environment.NewLine + ex.Message, ex);
            }
            catch (IOException ex)
            {
                throw new FileSaveException(ex.Message, ex);
            }
        }

        //*** SIMPLIFY THIS METHOD
        /**
        public void CheckGuidesForUpdate(FtpClient ftpClient)
        {
            if (ftpClient == null)
            {
                throw new ArgumentNullException(nameof(ftpClient));
            }

            var guidesConfiguration = _guidesConfigurationManager.Get();

            var latestEntity = GetLatestFileMetadata(ftpClient);

            var lastUpdateDate = guidesConfiguration.LastUpdateDate;
            if (latestEntity != null && !string.Equals(latestEntity.Date, lastUpdateDate))
            {
                try
                {
                    var localPath = guidesConfiguration.Path;
                    var fileName = latestEntity.Name;

                    ftpClient.DownloadFile(fileName, localPath);

                    UnZip(latestEntity.Name, guidesConfiguration.Path, true);
                    DeleteFile(guidesConfiguration.Path, latestEntity.Name);

                    guidesConfiguration.LastUpdateDate = latestEntity.Date;
                    _guidesConfigurationManager.Update(guidesConfiguration);
                }
                catch (IOException ex)
                {
                    throw new FileSaveException(ex.Message, ex);
                }
                catch (AggregateException ae)
                {
                    // This is where you can choose which exceptions to handle.
                    foreach (var ex in ae.InnerExceptions)
                    {
                        if (ex is System.Net.WebException)
                        {
                            throw new RemoteServerException(
                                "Guides service not respond" + Environment.NewLine + ex.Message, ex);
                        }

                        if (ex is System.IO.DirectoryNotFoundException)
                        {
                            throw new FileSaveException(
                                "Guides folder not found" + Environment.NewLine + ex.Message, ex);
                        }

                        throw ex;
                    }
                }
            }
        }
        */


        public GuidesIdsCollection GetGuidesIds()
        {
            var mnnGuideIds = _mnnGuideRepository.GetIds("sp_mnn.dbf");
            var tradeNameIds = _tradeNameGuideRepository.GetIds("sp_trn.dbf");
            var drugIds = _drugGuideRepository.GetIds("sp_tov.dbf");
            var drugformIds = _drugformGuideRepository.GetIds("sp_lf.dbf");

            var guidesIdsCollection = new GuidesIdsCollection()
            {
                MnnGuideIs = mnnGuideIds,
                TradeNamesGudeIds = tradeNameIds,
                DrugGuideIds = drugIds,
                DrugformIds = drugformIds
            };

            return guidesIdsCollection;
        }

        public async Task<GuidesIdsCollection> GetGuidesIdsAsync()
        {
            var mnnGuideIds = await _mnnGuideRepository.GetIdsAsync("sp_mnn.dbf");
            var tradeNameIds = await _tradeNameGuideRepository.GetIdsAsync("sp_trn.dbf");
            var drugIds = await _drugGuideRepository.GetIdsAsync("sp_tov.dbf");
            var drugformIds = await _drugformGuideRepository.GetIdsAsync("sp_lf.dbf");

            var guidesIdsCollection = new GuidesIdsCollection()
            {
                MnnGuideIs = mnnGuideIds,
                TradeNamesGudeIds = tradeNameIds,
                DrugGuideIds = drugIds,
                DrugformIds = drugformIds
            };

            return guidesIdsCollection;
        }


        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        private void CheckMetadataParameter(FileMetadata metadata)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }

            if (string.IsNullOrEmpty( metadata.Name))
            {
                throw new ArgumentException("File metadata : file name is empty or null");
            }

            if (string.IsNullOrEmpty(metadata.Date))
            {
                throw new ArgumentException("File metadata : file date is empty or null");
            }

            var fileExtension = Path.GetExtension(metadata.Name);
            if (!fileExtension.Equals(".zip"))
            {
                throw new ArgumentException("Wrong file format");
            }
        }

        private FileMetadata GetLatestFileMetadata(FtpClient ftpClient)
        {
            var ftpFilesMetadata = GetFilesMetadata(ftpClient);

            var latestFileMetadata =
                ftpFilesMetadata.OrderByDescending(entity => entity.Name).FirstOrDefault();

            return latestFileMetadata;
        }

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



        //*** NOT USED
        private void DownloadGuides(FtpClient ftpClient, string localPath,
            IList<FileMetadata> filesMetadataList)
        {
            Parallel.ForEach(filesMetadataList, (item) =>
            {
                if (item.IsForUpdate)
                {
                    ftpClient.DownloadFile(item.Name, localPath);
                }
            });
        }

        //*** NOT USED
        private void RenewGuidesUpdateDates(IList<FileMetadata> filesMetaDataList, GuidesConfiguration guidesConfiguration)
        {
            foreach (var fileMetadata in filesMetaDataList)
            {
                if (fileMetadata.IsForUpdate)
                {
                    var guideName = guidesConfiguration.Guides.SingleOrDefault(g =>
                        string.Equals(g.Value[GuideNameIndex], fileMetadata.Name)).Key;

                    guidesConfiguration.Guides[guideName][GuideDateIndex] = fileMetadata.Date;
                }
            }

            _guidesConfigurationManager.Update(guidesConfiguration);
        }



        private void UnZip(string fileName, string localPath, bool overwrite)
        {
            var zipFilePath = Path.Combine(localPath, fileName);

            using (var filesArchive = ZipFile.OpenRead(zipFilePath))
            {
                foreach (var zipEntry in filesArchive.Entries)
                {
                    var zipEntryPath =
                        Path.Combine(localPath, zipEntry.FullName.ToLower());
                    zipEntry.ExtractToFile(zipEntryPath, overwrite);
                }
            }
        }

        private void DeleteFile(string filePath, string fileName)
        {
            var path = Path.Combine(filePath, fileName);
            File.Delete(path);
        }



        // PRIVATE HELPER NESTED CLASSES SECTION
        //---------------------------------------------------------------------

        
    }
}
