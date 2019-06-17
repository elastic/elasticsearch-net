using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(this IElasticClient client,
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null
		)
			=> client.Rollup.GetIndexCapabilities(index, selector);

		/// <inheritdoc
		///     cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(this IElasticClient client,
			IGetRollupIndexCapabilitiesRequest request
		)
			=> client.Rollup.GetIndexCapabilities(request);

		/// <inheritdoc
		///     cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(this IElasticClient client,
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Rollup.GetIndexCapabilitiesAsync(index, selector, ct);

		/// <inheritdoc
		///     cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(this IElasticClient client,
			IGetRollupIndexCapabilitiesRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.GetIndexCapabilitiesAsync(request, ct);
	}
}
