using System.Collections.Generic;
using System.Data.Common;

namespace GenericProject.Core.Data.EntityFramework
{
    public interface IQueryExecutor
    {
        IEnumerable<T> ExecuteQuery<T>(string query, IEnumerable<DbParameter> parameters);
    }
}
