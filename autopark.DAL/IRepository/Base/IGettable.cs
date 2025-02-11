namespace autopark.DAL.IRepository.Base;

public interface IGettableById<T> where T : class
{
    Task<T> GetByIdAsync(int id);
}