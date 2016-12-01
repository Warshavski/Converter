using System.Configuration;

namespace Escyug.Converter.Configuration.Sections
{
    internal class SenderSectionConfiguration : ConfigurationSection
    {
        /// <summary>
        ///     Host name(ip address) and port number of the SMTP server
        /// </summary>
        [ConfigurationProperty("host")]
        public HostElement Host
        {
            get { return (HostElement)this["host"]; }
            set { this["host"] = value; }
        }

        /// <summary>
        ///     sender e-mail credentials(login and password)
        /// </summary>
        [ConfigurationProperty("credentials")]
        public CredentialsElement Credentials
        {
            get { return (CredentialsElement)this["credentials"]; }
            set { this["credentials"] = value; }
        }

        /// <summary>
        ///     E-mail addresses of message recipients
        /// </summary>
        [ConfigurationProperty("receivers")]
        [ConfigurationCollection(typeof(ReceiversElement), AddItemName = "receiver")]
        public ReceiversElement Receivers
        {
            get { return (ReceiversElement)this["receivers"]; }
            set { this["receivers"] = value; }
        }

        /*
        public static SenderConfiguration GetConfig()
        {
            return (SenderConfiguration)ConfigurationManager.GetSection("senderSection") ?? 
                new SenderConfiguration();
        }
        */
    }

    /// <summary>
    ///     SMTP host configuration element
    /// </summary>
    internal class HostElement : ConfigurationElement
    {
        /// <summary>
        ///     Name or ip address of the SMTP server
        /// </summary>
        [ConfigurationProperty("address", DefaultValue = "smtp.mail.ru", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }

        /// <summary>
        ///  Port number of the SMTP server
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = 25, IsRequired = true)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }
    }

    /// <summary>
    ///     E-mail sender client configuration element
    /// </summary>
    internal class CredentialsElement : ConfigurationElement
    {
        /// <summary>
        ///     The address from which e-mails should be sent
        /// </summary>
        [ConfigurationProperty("login", DefaultValue = "escyug_sender@mail.ru", IsRequired = true)]
        public string Login
        {
            get { return (string)this["login"]; }
            set { this["login"] = value; }
        }

        /// <summary>
        ///     The password of the e-mail box
        /// </summary>
        [ConfigurationProperty("password", DefaultValue = "pgpFmvk5", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
    }

    /// <summary>
    ///     E-mail message receiver configuration element
    /// </summary>
    internal class ReceiverElement : ConfigurationElement
    {
        /// <summary>
        ///     E-mail address of the receiver of the e-mail message
        /// </summary>
        [ConfigurationProperty("address", DefaultValue = "escyug@gmail.com", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }
    }

    /// <summary>
    ///     The collection of the receivers  of the email message
    /// </summary>
    internal class ReceiversElement : ConfigurationElementCollection
    {

        public ReceiverElement this[int index]
        {
            get { return base.BaseGet(index) as ReceiverElement; }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new ReceiverElement this[string responseString]
        {
            get { return (ReceiverElement)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ReceiverElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ReceiverElement)element).Address;
        }
    }
}
