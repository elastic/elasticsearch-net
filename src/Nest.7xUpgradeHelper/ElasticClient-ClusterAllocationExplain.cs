using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The cluster allocation explanation API is designed to assist in answering the question "why is this shard unassigned?"
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-allocation-explain.html">https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-allocation-explain.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the cluster allocation explain operation</param>
		public static ClusterAllocationExplainResponse ClusterAllocationExplain(this IElasticClient client,
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null
		);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public static ClusterAllocationExplainResponse ClusterAllocationExplain(this IElasticClient client,IClusterAllocationExplainRequest request);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public static Task<ClusterAllocationExplainResponse> ClusterAllocationExplainAsync(this IElasticClient client,
			Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterAllocationExplain(System.Func{Nest.ClusterAllocationExplainDescriptor,Nest.IClusterAllocationExplainRequest})"/>
		public static Task<ClusterAllocationExplainResponse> ClusterAllocationExplainAsync(this IElasticClient client,
			IClusterAllocationExplainRequest request,
			CancellationToken ct = default
		);
	}

}
