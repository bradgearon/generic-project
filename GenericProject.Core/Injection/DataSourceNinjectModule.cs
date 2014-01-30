using System.Data.Entity;
using GenericProject.Core.Data.EntityFramework;
using GenericProject.Data;
using Ninject.Modules;
using Ninject.Web.Common;

namespace GenericProject.Core.Injection
{
    public class DataSourceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUnitOfWorkFactory>().ToConstant(new UnitOfWorkFactory());
            Kernel.Bind<IUnitOfWork>().To(typeof (EntityFrameworkUnitOfWork));
            Kernel.Bind(typeof (IRepositoryFactory<>)).To(typeof (RepositoryFactory<>));
            Kernel.Bind(typeof (IRepository<>)).To(typeof (EntityFrameworkRepository<>));
            Kernel.Bind<DbContext>().To(typeof (EntityFrameworkContext)).InRequestScope();
        }
    }
}