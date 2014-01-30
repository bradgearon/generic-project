using GenericProject.Core.Injection;

namespace GenericProject.Data
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public UnitOfWorkFactory() { }

        public IUnitOfWork StartNew() { return Injector.Get<IUnitOfWork>(); }

        public IUnitOfWork StartNew(bool useSerializableEntities)
        {
            var unitOfWork = Injector.Get<IUnitOfWork>();
            unitOfWork.UseSerializableEntities = useSerializableEntities;
            return unitOfWork;
        }
    }
}