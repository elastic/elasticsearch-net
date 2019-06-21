using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.XPack.Usage(), please update this usage.")]
		public static XPackUsageResponse XPackUsage(this IElasticClient client, Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null)
			=> client.XPack.Usage(selector);

		[Obsolete("Moved to client.XPack.Usage(), please update this usage.")]
		public static XPackUsageResponse XPackUsage(this IElasticClient client, IXPackUsageRequest request)
			=> client.XPack.Usage(request);

		[Obsolete("Moved to client.XPack.UsageAsync(), please update this usage.")]
		public static Task<XPackUsageResponse> XPackUsageAsync(this IElasticClient client,
			Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.XPack.UsageAsync(selector, ct);

		[Obsolete("Moved to client.XPack.UsageAsync(), please update this usage.")]
		public static Task<XPackUsageResponse> XPackUsageAsync(this IElasticClient client, IXPackUsageRequest request, CancellationToken ct = default)
			=> client.XPack.UsageAsync(request, ct);
	}
}
