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
			CancellationToken ct = default
		);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		Task<IQuerySqlResponse> QuerySqlAsync(IQuerySqlRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public IQuerySqlResponse QuerySql(Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null) =>
			QuerySql(selector.InvokeOrDefault(new QuerySqlDescriptor()));

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public IQuerySqlResponse QuerySql(IQuerySqlRequest request) =>
			DoRequest<IQuerySqlRequest, QuerySqlResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public Task<IQuerySqlResponse> QuerySqlAsync(
			Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null,
			CancellationToken ct = default
		) => QuerySqlAsync(selector.InvokeOrDefault(new QuerySqlDescriptor()), ct);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public Task<IQuerySqlResponse> QuerySqlAsync(IQuerySqlRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IQuerySqlRequest, IQuerySqlResponse, QuerySqlResponse>
				(request, request.RequestParameters, ct);
	}
}
