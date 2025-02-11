namespace autopark.DAL.IRepository.Base;

public interface IDeletable
{
    Task DeleteAsync(int id);
}