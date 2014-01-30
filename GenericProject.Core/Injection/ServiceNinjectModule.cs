using GenericProject.Core.Services;
using Ninject.Modules;

namespace GenericProject.Core.Injection
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUserService>().To<UserService>();
            Kernel.Bind<IPeepService>().To<PeepService>();
        }
    }
}
