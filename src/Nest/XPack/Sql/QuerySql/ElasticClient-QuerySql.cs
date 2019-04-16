using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary> The SQL REST API accepts SQL in a JSON document, executes it, and returns the results. </summary>
		IQuerySqlResponse QuerySql(Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		IQuerySqlResponse QuerySql(IQuerySqlRequest request);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		Task<IQuerySqlResponse> QuerySqlAsync(Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		Task<IQuerySqlResponse> QuerySqlAsync(IQuerySqlRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public IQuerySqlResponse QuerySql(Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null) =>
			QuerySql(selector.InvokeOrDefault(new QuerySqlDescriptor()));

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public IQuerySqlResponse QuerySql(IQuerySqlRequest request) =>
			Dispatcher.Dispatch<IQuerySqlRequest, QuerySqlRequestParameters, QuerySqlResponse>(
				request,
				(p, d) => LowLevelDispatch.SqlQueryDispatch<QuerySqlResponse>(p, d)
			);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public Task<IQuerySqlResponse> QuerySqlAsync(Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			QuerySqlAsync(selector.InvokeOrDefault(new QuerySqlDescriptor()), cancellationToken);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public Task<IQuerySqlResponse> QuerySqlAsync(IQuerySqlRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IQuerySqlRequest, QuerySqlRequestParameters, QuerySqlResponse, IQuerySqlResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SqlQueryDispatchAsync<QuerySqlResponse>(p, d, c)
			);
	}
}
