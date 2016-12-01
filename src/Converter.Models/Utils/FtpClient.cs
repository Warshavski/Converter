using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Escyug.Converter.Models.Utils
{
    /*** TODO :
     *      1. Create common methods for DownloadFile and DownloadFileAsync
     *      2. Check exceptions handling
     */

    /// <summary>
    ///  Provides access to ftp server
    /// </summary>
    public class FtpClient
    {

        // CONSTANTS SECTION
        private const int DefaultPort = 21;



        // PROPERTIES SECTION
        //---------------------------------------------------------------------

        #region FtpClient properties

        private string _remoteUri;
        public string RemoteUri
        {
            get { return _remoteUri; }
            private set { _remoteUri = value; }
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _port = value;
            }
        }

        #endregion FtpClient properties



        // CONSTRUCTOR SECTION
        //---------------------------------------------------------------------


        public FtpClient(string remoteUri, int port, string user, string password)
            : this(remoteUri, user, password)
        {
            Port = port;
        }

        public FtpClient(string remoteUri, string user, string password)
        {
            RemoteUri = remoteUri;
            User = user;
            Password = password;
            Port = DefaultPort;
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region FtpClient public methods

        public void DownloadFile(string fileName, string localPath)
        {
            var buffer = new byte[2048];

            FtpWebRequest request = CreateDownloadFtpRequest(RemoteUri + fileName, Port, User, Password);
            using (WebResponse response = request.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                if (responseStream == null)
                {
                    throw new ArgumentNullException(nameof(fileName));
                }

                using (var fileStream = new FileStream(localPath + fileName, FileMode.Create))
                {
                    while (true)
                    {
                        int bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                        {
                            break;
                        }

                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        public async Task DownloadFileAsync(string fileName, string localPath)
        {
            var buffer = new byte[2048];

            FtpWebRequest request = CreateDownloadFtpRequest(RemoteUri + fileName, Port, User, Password);
            using (WebResponse response = await request.GetResponseAsync())
            {
                Stream responseStream = response.GetResponseStream();
                if (responseStream == null)
                {
                    throw new ArgumentNullException(nameof(responseStream));
                }

                using (var fileStream = new FileStream(localPath + fileName, FileMode.Create))
                {
                    while (true)
                    {
                        int bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                        {
                            break;
                        }
                        
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                    }
                }
            }
        }

        public string[] GetFtpFolderMetadata()
        {
            return GetFtpFolderMetadata(RemoteUri, Port, User, Password);
        }

        public string[] GetFtpFolderMetadata(string uri, int port, string userName, string userPassword)
        {
            var request = CreateFtpRequest(uri, port, userName, userPassword);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            string[] metadata;
            using (var response = request.GetResponse())
            {
                var responseStream = response.GetResponseStream();

                if (responseStream == null)
                {
                    throw new ArgumentNullException(nameof(uri));
                }

                using (var reader = new StreamReader(responseStream))
                {
                    metadata = reader.ReadToEnd().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
            }

            return metadata;
        }

        #endregion FtpClient public methods



        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        #region Private helper methods

        private string CheckString(string checkingString)
        {
            if (string.IsNullOrEmpty(checkingString))
            {
                throw new ArgumentNullException(nameof(checkingString));
            }

            return checkingString.Trim();
        }

        private FtpWebRequest CreateDownloadFtpRequest(string uri, int port, string userName, string userPassword)
        {
            var request = CreateFtpRequest(uri, port, userName, userPassword);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            return request;
        }

        private FtpWebRequest CreateFtpRequest(string uri, int port, string userName, string userPassword)
        {
            var requestUri = CheckString(uri);

            if (port != DefaultPort)
            {
                requestUri += ":" + Port;
            }

            var request = (FtpWebRequest)WebRequest.Create(requestUri);
            request.Credentials = new NetworkCredential(userName, userPassword);

            return request;
        }

        #endregion Private helper methods
    }
}
