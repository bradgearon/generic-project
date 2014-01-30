using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace GenericProject.Core.Injection
{
	public class NinjectInjectionProvider : IInjectionProvider
	{
		private readonly IKernel _Kernel;

		public NinjectInjectionProvider(IKernel kernel)
		{
			_Kernel = kernel;
		}

		public object Get(Type type)
		{
			return _Kernel.Get(type);
		}

	    public T Get<T>()
		{
			return _Kernel.Get<T>();
		}

		public T Get<T>(string name)
		{
			return _Kernel.Get<T>(name);
		}

		public T Get<T>(params ConstructorParameter[] constructorParameters)
		{
			var parameters = from cp in constructorParameters
							 select new Ninject.Parameters.ConstructorArgument(cp.Name, cp.Value);
			return _Kernel.Get<T>(parameters.ToArray());
		}
	}
}
