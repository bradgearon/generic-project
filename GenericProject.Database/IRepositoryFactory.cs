using GenericProject.Data;

namespace GenericProject.Database
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class;
        IRepository<T> GetRepository<T>(IUnitOfWork unitOfWork) where T : class;
    }
}
