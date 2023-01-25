namespace InfTech.Services.CatalogApi.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {  
        Task<T> Get(int id);
        Task<IEnumerable<T>> Get();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
