using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The cluster state API allows to get a comprehensive state information of the whole cluster.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the cluster state operation</param>
		public static ClusterStateResponse ClusterState(this IElasticClient client,Func<ClusterStateDescriptor, IClusterStateRequest> selector = null);

		/// <inheritdoc />
		public static ClusterStateResponse ClusterState(this IElasticClient client,IClusterStateRequest request);

		/// <inheritdoc />
		public static Task<ClusterStateResponse> ClusterStateAsync(this IElasticClient client,Func<ClusterStateDescriptor, IClusterStateRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ClusterStateResponse> ClusterStateAsync(this IElasticClient client,IClusterStateRequest request, CancellationToken ct = default);
	}

}
