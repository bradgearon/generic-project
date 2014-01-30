using GenericProject.Core.Data.EntityFramework;
using GenericProject.Data;

namespace GenericProject.Database
{
    public class EntityFrameworkRepositoryFactory : IRepositoryFactory
    {
        private IUnitOfWork _unitOfWork;

        public EntityFrameworkRepositoryFactory()
        {

        }

        public EntityFrameworkRepositoryFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_unitOfWork != null)
                return new EntityFrameworkRepository<T>(_unitOfWork);

            return new EntityFrameworkRepository<T>();
        }

        public IRepository<T> GetRepository<T>(IUnitOfWork unitOfWork) where T : class
        {
            return new EntityFrameworkRepository<T>(unitOfWork);
        }
    }
}
