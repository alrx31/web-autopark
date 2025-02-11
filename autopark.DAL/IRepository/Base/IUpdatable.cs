namespace autopark.DAL.IRepository.Base;

public interface IUpdatable<T> where T : class
{
    Task UpdateAsync(T entity);
}