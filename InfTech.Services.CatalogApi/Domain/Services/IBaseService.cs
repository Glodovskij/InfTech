namespace InfTech.Services.CatalogApi.Domain.Services
{
    public interface IBaseService<T> where T : class
    {
        T Get(int id);
        List<T> Get();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
