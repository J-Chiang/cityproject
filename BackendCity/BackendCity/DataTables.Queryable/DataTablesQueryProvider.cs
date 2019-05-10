using DataTables.AspNet.Core;
using System.Linq;
using System.Linq.Expressions;

namespace DataTables.Queryable
{
    internal class DataTablesQueryProvider<T> : IQueryProvider
    {
        private IQueryProvider sourceProvider;
        private IDataTablesRequest request;

        internal DataTablesQueryProvider(IQueryProvider sourceProvider, IDataTablesRequest request)
        {
            this.sourceProvider = sourceProvider;
            this.request = request;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var x = sourceProvider.CreateQuery(expression);
            IQueryable<T> y = x as IQueryable<T>;
            return new DataTablesQueryable<T>(y, request);
        }

        public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
        {
            return (IQueryable<TResult>)CreateQuery(expression);
        }

        public object Execute(Expression expression)
        {
            return sourceProvider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)sourceProvider.Execute(expression);
        }
    }
}
