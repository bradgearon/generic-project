using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using GenericProject.Core.Data.LinqKit;
using GenericProject.Core.Model;
using GenericProject.Core.Models;
using GenericProject.Data;

namespace GenericProject.Core.Data.EntityFramework
{
    /// <summary>
    /// 	Generic Repository shamelessly based on the link below (includes UoW).
    /// </summary>
    /// <see cref = "http://forums.asp.net/t/1606545.aspx/1?EF4+with+Repository+and+Unit+of+Work+patterns+Question+about+an+architecture" />
    /// <typeparam name = "T">Type of entity</typeparam>
    public class EntityFrameworkRepository<T> : IRepository<T>, IQueryExecutor where T: class
    {
        protected EntityFrameworkUnitOfWork UnitOfWork { get; private set; }

        protected DbSet<T> DbSet { get; private set; }

        private DbContext _Container;

        protected DbContext Container
        {
            get
            {
                if (_Container == null)
                    throw new InvalidOperationException("Repository must be initialized before it is used.");

                return _Container;
            }
            private set { _Container = value; }
        }

        public IQueryExecutor QueryExecutor { get; set; }

        public EntityFrameworkRepository() { this.QueryExecutor = this; }

        public EntityFrameworkRepository(IUnitOfWork unitOfWork)
            : this() { Initialize(unitOfWork); }

        private void Initialize(IUnitOfWork unitOfWork)
        {
            if (!(unitOfWork is EntityFrameworkUnitOfWork))
                throw new ArgumentException("The concrete UnitOfWork type is not supported.", "unitOfWork");

            if (_Container != null)
                throw new InvalidOperationException("Repository cannot be initialized multiple times.");

            UnitOfWork = (EntityFrameworkUnitOfWork)unitOfWork;
            Container = UnitOfWork.Container;
            DbSet = UnitOfWork.CreateDbSet<T>();
        }

        public IQueryable<T> AsQueryable() { return DbSet.IncludeAll(); }

        public IQueryable<T> AsQueryable(params string[] includes) { return DbSet.IncludeAll(includes); }

        public IQueryable<T> AsQueryable(params Expression<Func<T, object>>[] includes) { return DbSet.IncludeAll(includes); }

        public IEnumerable<T> GetAll(params string[] includes) { return DbSet.IncludeAll(includes); }

        public IEnumerable<T> GetAll(PaginationRequest paginationRequest, params string[] includes) { return DbSet.IncludeAll(includes).ApplyPaging(paginationRequest); }

        public T FindByKey(params object[] keys) { return DbSet.Find(keys); }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params string[] includes) { return DbSet.IncludeAll(includes).AsExpandable().Where(predicate); }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, PaginationRequest paginationRequest, params string[] includes) { return DbSet.IncludeAll(includes).AsExpandable().Where(predicate).ApplyPaging(paginationRequest); }

        public T Single(Expression<Func<T, bool>> predicate, params string[] includes) { return DbSet.IncludeAll(includes).AsExpandable().Single(predicate); }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate) { return DbSet.AsExpandable().SingleOrDefault(predicate); }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate, params string[] includes) { return DbSet.IncludeAll().IncludeAll(includes).AsExpandable().SingleOrDefault(predicate); }

        public T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) { return DbSet.IncludeAll(includes).AsExpandable().Single(predicate); }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) { return DbSet.IncludeAll().IncludeAll(includes).AsExpandable().SingleOrDefault(predicate); }

        public T First(Expression<Func<T, bool>> predicate, params string[] includes) { return DbSet.IncludeAll(includes).AsExpandable().First(predicate); }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includes) { return DbSet.IncludeAll(includes).AsExpandable().FirstOrDefault(predicate); }

        public T First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) { return DbSet.IncludeAll(includes).AsExpandable().First(predicate); }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) { return DbSet.IncludeAll(includes).AsExpandable().FirstOrDefault(predicate); }

        public int Count() { return DbSet.Count(); }

        public int Count(Expression<Func<T, bool>> predicate) { return DbSet.AsExpandable().Count(predicate); }

        public bool Any(Expression<Func<T, bool>> predicate) { return DbSet.AsExpandable().Any(predicate); }

        public virtual void Add(T entity) { DbSet.Add(entity); }

        public virtual void Detach(T entity) { Container.Entry(entity).State = EntityState.Detached; }

        public virtual void Attach(T entity) { DbSet.Attach(entity); }

        private void AttachAs(T entity, EntityState entityState)
        {
            DbSet.Attach(entity);
            Container.Entry(entity).State = entityState;
        }

        public virtual void AttachAsModified(T entity) { AttachAs(entity, EntityState.Modified); }

        public virtual void AttachAsAdded(T entity) { AttachAs(entity, EntityState.Added); }

        public virtual void Remove(T entity) { DbSet.Remove(entity); }

        public virtual void Remove(Expression<Func<T, bool>> predicate)
        {
            var records = DbSet.Where(predicate);

            foreach (var record in records)
                DbSet.Remove(record);
        }

        public IEnumerable<T> ExecuteQuery(string query, params object[] parameters)
        {
            var sqlParameters = ToSqlParameters(query, parameters);
            IEnumerable<T> results = this.QueryExecutor.ExecuteQuery<T>(query, sqlParameters);
            return results;
        }

        public void Add(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                DbSet.Add(entity);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string query, IEnumerable<DbParameter> parameters)
        {
            var context = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this.Container).ObjectContext;
            IEnumerable<TEntity> results = context.ExecuteStoreQuery<TEntity>(query, parameters.ToArray());

            return results;
        }

        private IEnumerable<DbParameter> ToSqlParameters(string query, params object[] parameters)
        {
            var expr = "@(\\w*)";
            var regex = new Regex(expr);
            var parameterNames =
                regex.Matches(query).Cast<Match>().SelectMany(m => m.Captures.Cast<Capture>()).Select(c => c.Value.Substring(1)).Distinct().ToArray();
            var sqlParameters = new List<DbParameter>();
            for (int i = 0; i < parameters.Length && i < parameterNames.Length; i++)
                sqlParameters.Add((DbParameter)(new SqlParameter(parameterNames[i], parameters[i])));

            return sqlParameters.ToArray();
        }
    }
}