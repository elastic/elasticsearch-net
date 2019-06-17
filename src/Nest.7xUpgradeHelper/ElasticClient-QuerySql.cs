using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Sql.Query(), please update this usage.")]
		public static QuerySqlResponse QuerySql(this IElasticClient client, Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null)
			=> client.Sql.Query(selector);

		[Obsolete("Moved to client.Sql.Query(), please update this usage.")]
		public static QuerySqlResponse QuerySql(this IElasticClient client, IQuerySqlRequest request)
			=> client.Sql.Query(request);

		[Obsolete("Moved to client.Sql.QueryAsync(), please update this usage.")]
		public static Task<QuerySqlResponse> QuerySqlAsync(this IElasticClient client, Func<QuerySqlDescriptor, IQuerySqlRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Sql.QueryAsync(selector, ct);

		[Obsolete("Moved to client.Sql.QueryAsync(), please update this usage.")]
		public static Task<QuerySqlResponse> QuerySqlAsync(this IElasticClient client, IQuerySqlRequest request, CancellationToken ct = default)
			=> client.Sql.QueryAsync(request, ct);
	}
}
