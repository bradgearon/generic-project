using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenericProject.Core.Data.LinqKit
{
	/// <summary>
	/// An IQueryable wrapper that allows us to visit the query's expression tree just before LINQ to SQL gets to it.
	/// This is based on the excellent work of Tomas Petricek: http://tomasp.net/blog/linq-expand.aspx
	/// </summary>
	public class ExpandableQuery<T> : IOrderedQueryable<T>
	{
		#region Fields
		private readonly ExpandableQueryProvider<T> _Provider;
		private readonly IQueryable<T> _Inner;
		#endregion

		#region Properties
		Expression IQueryable.Expression
		{
			get
			{
				return _Inner.Expression;
			}
		}

		Type IQueryable.ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		IQueryProvider IQueryable.Provider
		{
			get
			{
				return _Provider;
			}
		}

		internal IQueryable<T> InnerQuery
		{
			get
			{
				return _Inner;
			}
		} // Original query, that we're wrapping
		#endregion

		#region Constructors
		internal ExpandableQuery(IQueryable<T> inner)
		{
			_Inner = inner;
			_Provider = new ExpandableQueryProvider<T>(this);
		}
		#endregion

		#region Overrides
		public IEnumerator<T> GetEnumerator()
		{
			return _Inner.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _Inner.GetEnumerator();
		}

		public override string ToString()
		{
			return _Inner.ToString();
		}
		#endregion
	}

	internal class ExpandableQueryProvider<T> : IQueryProvider
	{
		#region Fields
		private readonly ExpandableQuery<T> _Query;
		#endregion

		#region Constructors
		internal ExpandableQueryProvider(ExpandableQuery<T> query)
		{
			_Query = query;
		}
		#endregion

		// The following four methods first call ExpressionExpander to visit the expression tree, then call
		// upon the inner query to do the remaining work.
		IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
		{
			return new ExpandableQuery<TElement>(_Query.InnerQuery.Provider.CreateQuery<TElement>(expression.Expand()));
		}

		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			return _Query.InnerQuery.Provider.CreateQuery(expression.Expand());
		}

		TResult IQueryProvider.Execute<TResult>(Expression expression)
		{
			return _Query.InnerQuery.Provider.Execute<TResult>(expression.Expand());
		}

		object IQueryProvider.Execute(Expression expression)
		{
			return _Query.InnerQuery.Provider.Execute(expression.Expand());
		}
	}
}