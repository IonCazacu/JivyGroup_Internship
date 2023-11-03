namespace Contracts
{
    public interface IEntityOperation<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<T?> DeleteAsync(T entity);
    }
}
