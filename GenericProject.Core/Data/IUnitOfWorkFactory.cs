
namespace GenericProject.Data
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork StartNew();
        IUnitOfWork StartNew(bool useSerializableEntities);
    }
}
