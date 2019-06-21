using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Sql.ClearCursor(), please update this usage.")]
		public static ClearSqlCursorResponse ClearSqlCursor(this IElasticClient client,
			Func<ClearSqlCursorDescriptor, IClearSqlCursorRequest> selector = null
		)
			=> client.Sql.ClearCursor(selector);

		[Obsolete("Moved to client.Sql.ClearCursor(), please update this usage.")]
		public static ClearSqlCursorResponse ClearSqlCursor(this IElasticClient client, IClearSqlCursorRequest request)
			=> client.Sql.ClearCursor(request);

		[Obsolete("Moved to client.Sql.ClearCursorAsync(), please update this usage.")]
		public static Task<ClearSqlCursorResponse> ClearSqlCursorAsync(this IElasticClient client,
			Func<ClearSqlCursorDescriptor, IClearSqlCursorRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Sql.ClearCursorAsync(selector, ct);

		[Obsolete("Moved to client.Sql.ClearCursorAsync(), please update this usage.")]
		public static Task<ClearSqlCursorResponse> ClearSqlCursorAsync(this IElasticClient client, IClearSqlCursorRequest request,
			CancellationToken ct = default
		)
			=> client.Sql.ClearCursorAsync(request, ct);
	}
}
