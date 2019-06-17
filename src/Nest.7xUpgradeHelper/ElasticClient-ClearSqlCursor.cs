using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Clear a cursor returned by <see cref="QuerySqlResponse.Cursor" />. Not that this is only necessary if you wish to bail
		/// out early
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClearSqlCursorResponse ClearSqlCursor(this IElasticClient client,
			Func<ClearSqlCursorDescriptor, IClearSqlCursorRequest> selector = null
		)
			=> client.Sql.ClearCursor(selector);

		/// <inheritdoc cref="ClearSqlCursor(System.Func{Nest.ClearSqlCursorDescriptor,Nest.IClearSqlCursorRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClearSqlCursorResponse ClearSqlCursor(this IElasticClient client, IClearSqlCursorRequest request)
			=> client.Sql.ClearCursor(request);

		/// <inheritdoc cref="ClearSqlCursor(System.Func{Nest.ClearSqlCursorDescriptor,Nest.IClearSqlCursorRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClearSqlCursorResponse> ClearSqlCursorAsync(this IElasticClient client,
			Func<ClearSqlCursorDescriptor, IClearSqlCursorRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Sql.ClearCursorAsync(selector, ct);

		/// <inheritdoc cref="ClearSqlCursor(System.Func{Nest.ClearSqlCursorDescriptor,Nest.IClearSqlCursorRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClearSqlCursorResponse> ClearSqlCursorAsync(this IElasticClient client, IClearSqlCursorRequest request,
			CancellationToken ct = default
		)
			=> client.Sql.ClearCursorAsync(request, ct);
	}
}
