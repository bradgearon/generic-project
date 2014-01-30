using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GenericProject.Data;

namespace GenericProject.Core.Data.EntityFramework
{
	internal static class Extensions
	{
		public static IQueryable<TEntity> IncludeAll<TEntity>(this IQueryable<TEntity> queryable, params string[] includes) where TEntity : class
		{
			if ((includes == null) || (includes.Length == 0))
				return queryable;

            var query = queryable;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
		}
        public static IQueryable<TEntity> IncludeAll<TEntity, TProperty>(this IQueryable<TEntity> queryable, params Expression<Func<TEntity, TProperty>>[] includes) where TEntity : class
		{
			if ((includes == null) || (includes.Length == 0))
				return queryable;

            var query = queryable;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
		}

		public static EntityChangeType ToEntityChangeType(this EntityState entityState)
		{
			switch (entityState)
			{
				case EntityState.Detached:
					return EntityChangeType.Detached;

				case EntityState.Unchanged:
					return EntityChangeType.Unchanged;

				case EntityState.Added:
					return EntityChangeType.Added;

				case EntityState.Deleted:
					return EntityChangeType.Deleted;

				case EntityState.Modified:
					return EntityChangeType.Modified;

				default:
					throw new ArgumentOutOfRangeException("entityState");
			}
		}
	}
}