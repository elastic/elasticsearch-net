using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cluster.AllocationExplain(), please update this usage.")]
		public static ClusterAllocationExplainResponse ClusterAllocationExplain(this IElasticClient client,
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null
		)
			=> client.Cluster.AllocationExplain(selector);

		[Obsolete("Moved to client.Cluster.AllocationExplain(), please update this usage.")]
		public static ClusterAllocationExplainResponse ClusterAllocationExplain(this IElasticClient client, IClusterAllocationExplainRequest request)
			=> client.Cluster.AllocationExplain(request);

		[Obsolete("Moved to client.Cluster.AllocationExplainAsync(), please update this usage.")]
		public static Task<ClusterAllocationExplainResponse> ClusterAllocationExplainAsync(this IElasticClient client,
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.AllocationExplainAsync(selector, ct);

		[Obsolete("Moved to client.Cluster.AllocationExplainAsync(), please update this usage.")]
		public static Task<ClusterAllocationExplainResponse> ClusterAllocationExplainAsync(this IElasticClient client,
			IClusterAllocationExplainRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.AllocationExplainAsync(request, ct);
	}
}
