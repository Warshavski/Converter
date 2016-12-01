
namespace Escyug.Converter.Models.Repositories
{
    public interface IConfigurationManager<TEntity>
    {
        TEntity Get();
        TEntity Get(string name);
        void Set(TEntity entity);
        void Update(TEntity entity);
    }
}
