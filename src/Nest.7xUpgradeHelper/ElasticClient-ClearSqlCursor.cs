using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Clear a cursor returned by <see cref="QuerySqlResponse.Cursor" />. Not that this is only necessary if you wish to bail out early
		/// </summary>
		public static ClearSqlCursorResponse ClearSqlCursor(this IElasticClient client,Func<ClearSqlCursorDescriptor, IClearSqlCursorRequest> selector = null);

		/// <inheritdoc cref="ClearSqlCursor(System.Func{Nest.ClearSqlCursorDescriptor,Nest.IClearSqlCursorRequest})" />
		public static ClearSqlCursorResponse ClearSqlCursor(this IElasticClient client,IClearSqlCursorRequest request);

		/// <inheritdoc cref="ClearSqlCursor(System.Func{Nest.ClearSqlCursorDescriptor,Nest.IClearSqlCursorRequest})" />
		public static Task<ClearSqlCursorResponse> ClearSqlCursorAsync(this IElasticClient client,Func<ClearSqlCursorDescriptor, IClearSqlCursorRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClearSqlCursor(System.Func{Nest.ClearSqlCursorDescriptor,Nest.IClearSqlCursorRequest})" />
		public static Task<ClearSqlCursorResponse> ClearSqlCursorAsync(this IElasticClient client,IClearSqlCursorRequest request,
			CancellationToken ct = default
		);
	}

}
