using System.Configuration;

namespace Escyug.Converter.Configuration.Sections
{
    internal class FtpSectionConfiguration : ConfigurationSection
    {
        /// <summary>
        ///     Host name(ip address) and port number of the FTP server
        /// </summary>
        [ConfigurationProperty("host")]
        public FtpHostElement Host
        {
            get { return (FtpHostElement)this["host"]; }
            set { this["host"] = value; }
        }

        /// <summary>
        ///     FTP server credentials(login and password)
        /// </summary>
        [ConfigurationProperty("credentials")]
        public FtpCredentialsElement Credentials
        {
            get { return (FtpCredentialsElement)this["credentials"]; }
            set { this["credentials"] = value; }
        }
    }

    /// <summary>
    ///     FTP host configuration element
    /// </summary>
    internal class FtpHostElement : ConfigurationElement
    {
        /// <summary>
        ///     Name or ip address of the FTP server
        /// </summary>
        [ConfigurationProperty("address", DefaultValue = "", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }

        /// <summary>
        ///     Port number of the FTP server
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = 21, IsRequired = true)]
        public int? Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }
    }

    internal class FtpCredentialsElement : ConfigurationElement
    {
        [ConfigurationProperty("user", DefaultValue = "", IsRequired = true)]
        public string User
        {
            get { return (string)this["user"]; }
            set { this["user"] = value; }
        }

        /// <summary>
        ///     The password of the ftp server
        /// </summary>
        [ConfigurationProperty("password", DefaultValue = "", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
    }
}
