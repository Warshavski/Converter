using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.Converter.App.Console.Configurations
{
    public class SenderConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("host")]
        public HostElement Host
        {
            get { return (HostElement)this["host"]; }
            set { this["host"] = value; }
        }

        [ConfigurationProperty("credentials")]
        public CredentialsElement Credentials
        {
            get { return (CredentialsElement)this["credentials"]; }
            set { this["credentials"] = value; }
        }

        [System.Configuration.ConfigurationProperty("receivers")]
        [ConfigurationCollection(typeof(ReceiversElement), AddItemName = "receiver")]
        public ReceiversElement Receivers
        {
            get { return (ReceiversElement)this["receivers"]; }
            set { this["receivers"] = value; }
        }

        public static SenderConfiguration GetConfig()
        {
            return (SenderConfiguration)ConfigurationManager.GetSection("senderSection") ?? 
                new SenderConfiguration();
        }
    }


    public class HostElement : ConfigurationElement
    {
        [ConfigurationProperty("address", DefaultValue = "smtp.mail.ru", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }

        [ConfigurationProperty("port", DefaultValue = 25, IsRequired = true)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }
    }

    public class CredentialsElement : ConfigurationElement
    {
        [ConfigurationProperty("login", DefaultValue = "escyug_sender@mail.ru", IsRequired = true)]
        public string Login
        {
            get { return (string)this["login"]; }
            set { this["login"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue = "pgpFmvk5", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
    }

    public class ReceiverElement : ConfigurationElement
    {
        [ConfigurationProperty("address", DefaultValue = "escyug@gmail.com", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }
    }

    public class ReceiversElement : ConfigurationElementCollection
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

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new ReceiverElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ReceiverElement)element).Address;
        }
    }
}
