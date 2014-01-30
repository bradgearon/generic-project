
namespace GenericProject.Data
{
    public interface IRepositoryFactory<T> where T : class
    {
        IRepository<T> GetRepository();
        IRepository<T> GetRepository(IUnitOfWork unitOfWork);
    }
}
