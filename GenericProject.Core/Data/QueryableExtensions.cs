﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GenericProject.Core.Attributes;
using GenericProject.Core.Model;
using GenericProject.Core.Models;

namespace GenericProject.Data
{
    /// <summary>
    /// 	OrderBy found here: http://stackoverflow.com/questions/41244/dynamic-linq-orderby 
    /// 	And improved here: http://aonnull.blogspot.com/2010/08/dynamic-sql-like-linq-orderby-extension.html
    /// </summary>
    public static class QueryableExtensions
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> enumerable, string orderBy) { return enumerable.AsQueryable().OrderBy(orderBy).AsEnumerable(); }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> collection, string orderBy)
        {
            foreach (var orderByInfo in ParseOrderBy(orderBy))
                collection = ApplyOrderBy(collection, orderByInfo);

            return collection;
        }

        private static IQueryable<T> ApplyOrderBy<T>(IQueryable<T> collection, OrderByInfo orderByInfo)
        {
            var props = orderByInfo.PropertyName.Split('.');
            var type = typeof (T);

            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (var prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                var pi = type.GetProperty(prop);
                if (pi == null) throw new ArgumentException(String.Format("Cannot apply order to collection.  Unable to find propery: {0}", prop));
                var orderByAttribute = (OrderByAttribute)pi.GetCustomAttributes(typeof (OrderByAttribute), false).FirstOrDefault();

                if (orderByAttribute != null)
                    pi = type.GetProperty(orderByAttribute.PropertyName);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            var delegateType = typeof (Func<,>).MakeGenericType(typeof (T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);
            var methodName = String.Empty;

            if (!orderByInfo.Initial && collection is IOrderedQueryable<T>)
            {
                if (orderByInfo.Direction == SortDirection.Ascending)
                    methodName = "ThenBy";
                else
                    methodName = "ThenByDescending";
            }
            else
            {
                if (orderByInfo.Direction == SortDirection.Ascending)
                    methodName = "OrderBy";
                else
                    methodName = "OrderByDescending";
            }

            //TODO: apply caching to the generic methodsinfos?
            return (IOrderedQueryable<T>)typeof (Queryable).GetMethods().Single(
                method => method.Name == methodName
                    && method.IsGenericMethodDefinition
                    && method.GetGenericArguments().Length == 2
                    && method.GetParameters().Length == 2)
                                                           .MakeGenericMethod(typeof (T), type)
                                                           .Invoke(null, new object[] { collection, lambda });
        }

        private static IEnumerable<OrderByInfo> ParseOrderBy(string orderBy)
        {
            if (String.IsNullOrEmpty(orderBy))
                yield break;

            var items = orderBy.Split(',');
            var initial = true;
            foreach (var item in items)
            {
                var pair = item.Trim().Split(' ');

                if (pair.Length > 2)
                    throw new ArgumentException(String.Format("Invalid OrderBy string '{0}'. Order By Format: Property, Property2 ASC, Property2 DESC", item));

                var prop = pair[0].Trim();

                if (String.IsNullOrEmpty(prop))
                    throw new ArgumentException("Invalid Property. Order By Format: Property, Property2 ASC, Property2 DESC");

                var dir = SortDirection.Ascending;

                if (pair.Length == 2)
                    dir = ("desc".Equals(pair[1].Trim(), StringComparison.OrdinalIgnoreCase) ? SortDirection.Descending : SortDirection.Ascending);

                yield return new OrderByInfo { PropertyName = prop, Direction = dir, Initial = initial };

                initial = false;
            }
        }

        private class OrderByInfo
        {
            public string PropertyName { get; set; }

            public SortDirection Direction { get; set; }

            public bool Initial { get; set; }
        }

        private enum SortDirection
        {
            Ascending = 0,

            Descending = 1
        }

        public static IEnumerable<T> ApplyPaging<T>(this IEnumerable<T> queryable, PaginationRequest paginationRequest)
        {
            var query = queryable;

            if (paginationRequest != null)
            {
                var orderBy = paginationRequest.OrderBy;
                if (!string.IsNullOrEmpty(orderBy)) query = query.OrderBy(orderBy);
                query = query.Skip(paginationRequest.StartIndex).Take(paginationRequest.PageSize);
            }

            return query;
        }
    }
}