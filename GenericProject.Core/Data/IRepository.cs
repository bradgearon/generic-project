using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GenericProject.Core.Model;
using GenericProject.Core.Models;

namespace GenericProject.Data
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> AsQueryable();

        IQueryable<T> AsQueryable(params string[] includes);

        IQueryable<T> AsQueryable(params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAll(params string[] includes);

        IEnumerable<T> GetAll(PaginationRequest paginationRequest, params string[] includes);

        T FindByKey(params object[] keys);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params string[] includes);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, PaginationRequest paginationRequest, params string[] includes);

        T Single(Expression<Func<T, bool>> predicate, params string[] includes);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate, params string[] includes);

        T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        T SingleOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        T First(Expression<Func<T, bool>> predicate, params string[] includes);

        T FirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includes);

        T First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        int Count();

        int Count(Expression<Func<T, bool>> predicate);

        bool Any(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Remove(T entity);

        void Remove(Expression<Func<T, bool>> predicate);

        void Detach(T entity);

        void Attach(T entity);

        void AttachAsModified(T entity);

        void AttachAsAdded(T entity);

        IEnumerable<T> ExecuteQuery(string query, params object[] parameters);

        void Add(IEnumerable<T> entities);
    }
}