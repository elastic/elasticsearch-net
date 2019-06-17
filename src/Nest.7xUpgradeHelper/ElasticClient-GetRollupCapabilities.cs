using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Rollup.GetCapabilities(), please update this usage.")]
		public static GetRollupCapabilitiesResponse GetRollupCapabilities(this IElasticClient client,
			Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null
		)
			=> client.Rollup.GetCapabilities(selector);

		[Obsolete("Moved to client.Rollup.GetCapabilities(), please update this usage.")]
		public static GetRollupCapabilitiesResponse GetRollupCapabilities(this IElasticClient client, IGetRollupCapabilitiesRequest request)
			=> client.Rollup.GetCapabilities(request);

		[Obsolete("Moved to client.Rollup.GetCapabilitiesAsync(), please update this usage.")]
		public static Task<GetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(this IElasticClient client,
			Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Rollup.GetCapabilitiesAsync(selector, ct);

		[Obsolete("Moved to client.Rollup.GetCapabilitiesAsync(), please update this usage.")]
		public static Task<GetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(this IElasticClient client,
			IGetRollupCapabilitiesRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.GetCapabilitiesAsync(request, ct);
	}
}
