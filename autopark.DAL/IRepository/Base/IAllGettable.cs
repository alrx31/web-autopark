namespace autopark.DAL.IRepository.Base;

public interface IAllGettable<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();   
}