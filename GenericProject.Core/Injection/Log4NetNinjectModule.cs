using GenericProject.Core.Services;
using Ninject.Modules;

namespace GenericProject.Core.Injection
{
    public class Log4NetNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ILogger>().To<Log4NetLogger>();
        }
    }
}
