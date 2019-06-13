using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// </summary>
		public static GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(this IElasticClient client,
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null
		);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public static GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(this IElasticClient client,IGetRollupIndexCapabilitiesRequest request);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public static Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(this IElasticClient client,
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public static Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(this IElasticClient client,IGetRollupIndexCapabilitiesRequest request,
			CancellationToken ct = default
		);
	}

}
