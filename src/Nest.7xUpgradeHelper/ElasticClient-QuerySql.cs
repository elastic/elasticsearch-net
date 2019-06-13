using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary> The SQL REST API accepts SQL in a JSON document, executes it, and returns the results. </summary>
		public static QuerySqlResponse QuerySql(this IElasticClient client,Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public static QuerySqlResponse QuerySql(this IElasticClient client,IQuerySqlRequest request);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public static Task<QuerySqlResponse> QuerySqlAsync(this IElasticClient client,Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		public static Task<QuerySqlResponse> QuerySqlAsync(this IElasticClient client,IQuerySqlRequest request, CancellationToken ct = default);
	}

}
