using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary> The SQL REST API accepts SQL in a JSON document, executes it, and returns the results. </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static QuerySqlResponse QuerySql(this IElasticClient client, Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null)
			=> client.Sql.Query(selector);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static QuerySqlResponse QuerySql(this IElasticClient client, IQuerySqlRequest request)
			=> client.Sql.Query(request);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<QuerySqlResponse> QuerySqlAsync(this IElasticClient client, Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Sql.QueryAsync(selector, ct);

		/// <inheritdoc cref="QuerySql(System.Func{Nest.QuerySqlDescriptor,Nest.IQuerySqlRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<QuerySqlResponse> QuerySqlAsync(this IElasticClient client, IQuerySqlRequest request, CancellationToken ct = default)
			=> client.Sql.QueryAsync(request, ct);
	}
}
