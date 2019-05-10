using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataTables.Queryable
{
    /// <summary>
    /// Extended version of standard <see cref="IQueryable{T}"/> interface with
    /// additional property to access <see cref="DataTablesRequest{T}"/>.
    /// </summary>
    /// <typeparam name="T">Data type.</typeparam>
    public interface IDataTablesQueryable<T> : IQueryable<T>
    {
        /// <summary>
        /// <see cref="DataTablesRequest{T}"/> instance to filter the original <see cref="IQueryable{T}"/>.
        /// </summary>
        IDataTablesRequest Request { get; }

        IDataTablesResponse ToResponse(int Total);
    }

    /// <summary>
    /// Internal implementation of <see cref="IDataTablesQueryable{T}"/> interface.
    /// In fact, this is a wrapper around an <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <typeparam name="T">Data type.</typeparam>
    public class DataTablesQueryable<T> : IDataTablesRequest, IDataTablesQueryable<T>
    {
        private IQueryable<T> sourceQueryable;
        private DataTablesQueryProvider<T> sourceProvider;
        private readonly IDataTablesRequest dTrequest;

        internal DataTablesQueryable(IQueryable<T> q, IDataTablesRequest r)
        {
            this.sourceQueryable = q;
            this.dTrequest = r;
            this.sourceProvider = new DataTablesQueryProvider<T>(q.Provider, dTrequest);
        }

        public Type ElementType
        {
            get
            {
                return typeof(T);
            }
        }

        public Expression Expression
        {
            get
            {
                return sourceQueryable.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return sourceProvider;
            }
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return sourceQueryable.GetEnumerator();
        }

        public IDataTablesRequest Request
        {
            get
            {
                return dTrequest;
            }
        }

        public IDataTablesResponse ToResponse(int Total)
        {
            object o = this.ToArray();
            return DataTablesResponse.Create(dTrequest, Total, this.Count(), o);
        }

        public int Draw => Request.Draw;

        public int Start => Request.Start;

        public int Length => Request.Length;

        public ISearch Search => Request.Search;

        public IEnumerable<IColumn> Columns => Request.Columns;

        public IDictionary<string, object> AdditionalParameters => Request.AdditionalParameters;

        public override string ToString()
        {
            return sourceQueryable.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IQueryable)sourceQueryable).GetEnumerator();
        }
    }
}
