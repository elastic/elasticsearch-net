using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets the rollup capabilities that have been configured for an index or index pattern.
		/// This API is useful because a rollup job is often configured to rollup only a subset of fields from the
		/// source index. Furthermore, only certain aggregations can be configured for various fields,
		/// leading to a limited subset of functionality depending on that configuration.
		/// <para />
		/// <para />
		/// This API will allow you to inspect an index and determine:
		/// <para />
		/// 1. Does this index have associated rollup data somewhere in the cluster?
		/// <para />
		/// 2. If yes to the first question, what fields were rolled up, what aggregations can be performed, and where does the
		/// data live?
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRollupCapabilitiesResponse GetRollupCapabilities(this IElasticClient client,
			Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null
		)
			=> client.Rollup.GetCapabilities(selector);

		/// <inheritdoc
		///     cref="GetRollupCapabilities(System.Func{Nest.GetRollupCapabilitiesDescriptor,Nest.IGetRollupCapabilitiesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetRollupCapabilitiesResponse GetRollupCapabilities(this IElasticClient client, IGetRollupCapabilitiesRequest request)
			=> client.Rollup.GetCapabilities(request);

		/// <inheritdoc
		///     cref="GetRollupCapabilities(System.Func{Nest.GetRollupCapabilitiesDescriptor,Nest.IGetRollupCapabilitiesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(this IElasticClient client,
			Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Rollup.GetCapabilitiesAsync(selector, ct);

		/// <inheritdoc
		///     cref="GetRollupCapabilities(System.Func{Nest.GetRollupCapabilitiesDescriptor,Nest.IGetRollupCapabilitiesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(this IElasticClient client,
			IGetRollupCapabilitiesRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.GetCapabilitiesAsync(request, ct);
	}
}
