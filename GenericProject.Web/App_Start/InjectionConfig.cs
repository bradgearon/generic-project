using GenericProject.Core.Injection;
using Ninject;

namespace GenericProject.Web.App_Start
{
    public class InjectionConfig
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Load(new Log4NetNinjectModule());
            kernel.Load(new ServiceNinjectModule());
            kernel.Load(new DataSourceNinjectModule());
            Injector.Initialize(new NinjectInjectionProvider(kernel));
        }
    }
}