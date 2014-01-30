using System;
using System.Linq;

namespace GenericProject.Core.Data
{
	public class EntityChange
	{
		public object Entity { get; set; }
		public EntityChangeType EntityChangeType { get; set; }
	}

	public enum EntityChangeType
	{
		Detached,
		Unchanged,
		Modified,
		Added,
		Deleted
	}
}