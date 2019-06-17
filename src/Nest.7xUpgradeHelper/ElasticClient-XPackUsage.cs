using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static XPackUsageResponse XPackUsage(this IElasticClient client, Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null)
			=> client.XPack.Usage(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static XPackUsageResponse XPackUsage(this IElasticClient client, IXPackUsageRequest request)
			=> client.XPack.Usage(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<XPackUsageResponse> XPackUsageAsync(this IElasticClient client,
			Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.XPack.UsageAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<XPackUsageResponse> XPackUsageAsync(this IElasticClient client, IXPackUsageRequest request, CancellationToken ct = default)
			=> client.XPack.UsageAsync(request, ct);
	}
}
