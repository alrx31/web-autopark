namespace autopark.DAL.IRepository.Base;

public interface ICreatable<T> where T : class
{
    Task<int> AddAsync(T entity);
}