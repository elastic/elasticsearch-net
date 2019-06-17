using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClearCachedRealmsResponse ClearCachedRealms(this IElasticClient client, Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null
		)
			=> client.Security.ClearCachedRealms(realms, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClearCachedRealmsResponse ClearCachedRealms(this IElasticClient client, IClearCachedRealmsRequest request)
			=> client.Security.ClearCachedRealms(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(this IElasticClient client, Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.ClearCachedRealmsAsync(realms, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(this IElasticClient client, IClearCachedRealmsRequest request,
			CancellationToken ct = default
		)
			=> client.Security.ClearCachedRealmsAsync(request, ct);
	}
}
