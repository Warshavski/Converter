
namespace Escyug.Converter.Models.Entities.Configurations
{
    public class ConnectionStringConfiguration
    {
        public string Name { get; private set; }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }

        public ConnectionStringConfiguration(string connectionStringName)
        {
            Name = connectionStringName;
        }

        public ConnectionStringConfiguration()
        {
            
        }
    }
}
