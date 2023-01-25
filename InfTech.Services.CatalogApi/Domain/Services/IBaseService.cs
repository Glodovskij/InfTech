namespace InfTech.Services.CatalogApi.Domain.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> Get();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
