using GenericProject.Core.Injection;

namespace GenericProject.Data
{
    public class RepositoryFactory<T> : IRepositoryFactory<T>
        where T : class
    {
        public IRepository<T> GetRepository()
        {
            return Injector.Get<IRepository<T>>();
        }

        public IRepository<T> GetRepository(IUnitOfWork unitOfWork)
        {
            return Injector.Get<IRepository<T>>(new ConstructorParameter("unitOfWork", unitOfWork));
        }
    }
}
