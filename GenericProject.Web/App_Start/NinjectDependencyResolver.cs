using System;
using System.Collections.Generic;
using Ninject;

namespace GenericProject.Web.App_Start
{
    public class NinjectDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IKernel _Kernel;

        public NinjectDependencyResolver(IKernel kernel) { _Kernel = kernel; }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope() { return this; }

        public object GetService(Type serviceType) { return _Kernel.TryGet(serviceType); }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _Kernel.GetAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        public void Dispose() { }
    }
}