using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static ClearCachedRolesResponse ClearCachedRoles(this IElasticClient client,Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null);

		/// <inheritdoc />
		public static ClearCachedRolesResponse ClearCachedRoles(this IElasticClient client,IClearCachedRolesRequest request);

		/// <inheritdoc />
		public static Task<ClearCachedRolesResponse> ClearCachedRolesAsync(this IElasticClient client,Names roles, Func<ClearCachedRolesDescriptor, IClearCachedRolesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ClearCachedRolesResponse> ClearCachedRolesAsync(this IElasticClient client,IClearCachedRolesRequest request,
			CancellationToken ct = default
		);
	}

}
