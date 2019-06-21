using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.ClearCachedRealms(), please update this usage.")]
		public static ClearCachedRealmsResponse ClearCachedRealms(this IElasticClient client, Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null
		)
			=> client.Security.ClearCachedRealms(realms, selector);

		[Obsolete("Moved to client.Security.ClearCachedRealms(), please update this usage.")]
		public static ClearCachedRealmsResponse ClearCachedRealms(this IElasticClient client, IClearCachedRealmsRequest request)
			=> client.Security.ClearCachedRealms(request);

		[Obsolete("Moved to client.Security.ClearCachedRealmsAsync(), please update this usage.")]
		public static Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(this IElasticClient client, Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.ClearCachedRealmsAsync(realms, selector, ct);

		[Obsolete("Moved to client.Security.ClearCachedRealmsAsync(), please update this usage.")]
		public static Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(this IElasticClient client, IClearCachedRealmsRequest request,
			CancellationToken ct = default
		)
			=> client.Security.ClearCachedRealmsAsync(request, ct);
	}
}
