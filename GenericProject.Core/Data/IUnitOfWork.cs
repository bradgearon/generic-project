using System;
using System.Linq;

namespace GenericProject.Data
{
	public interface IUnitOfWork : IDisposable
	{
		bool UseSerializableEntities { get; set; }
		void SubmitChanges();
	}
}