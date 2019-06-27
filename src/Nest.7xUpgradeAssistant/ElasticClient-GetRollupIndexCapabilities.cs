using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Rollup.GetIndexCapabilities(), please update this usage.")]
		public static GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(this IElasticClient client,
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null
		)
			=> client.Rollup.GetIndexCapabilities(index, selector);

		[Obsolete("Moved to client.Rollup.GetIndexCapabilities(), please update this usage.")]
		public static GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(this IElasticClient client,
			IGetRollupIndexCapabilitiesRequest request
		)
			=> client.Rollup.GetIndexCapabilities(request);

		[Obsolete("Moved to client.Rollup.GetIndexCapabilitiesAsync(), please update this usage.")]
		public static Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(this IElasticClient client,
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Rollup.GetIndexCapabilitiesAsync(index, selector, ct);

		[Obsolete("Moved to client.Rollup.GetIndexCapabilitiesAsync(), please update this usage.")]
		public static Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(this IElasticClient client,
			IGetRollupIndexCapabilitiesRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.GetIndexCapabilitiesAsync(request, ct);
	}
}
