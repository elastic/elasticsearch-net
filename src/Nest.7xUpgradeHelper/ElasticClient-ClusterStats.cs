using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// allows to retrieve statistics from a cluster wide perspective. The API returns basic index metrics
		/// (shard numbers, store size, memory usage) and information about the current nodes that form the
		/// cluster (number, roles, os, jvm versions, memory usage, cpu and installed plugins).
		/// </summary>
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/guide/current/_cluster_stats.html">https://www.elastic.co/guide/en/elasticsearch/guide/current/_cluster_stats.html</a>
		/// <param name="selector">A descriptor that describes the cluster stats operation</param>
		public static ClusterStatsResponse ClusterStats(this IElasticClient client,Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null);

		/// <inheritdoc />
		public static Task<ClusterStatsResponse> ClusterStatsAsync(this IElasticClient client,Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static ClusterStatsResponse ClusterStats(this IElasticClient client,IClusterStatsRequest request);

		/// <inheritdoc />
		public static Task<ClusterStatsResponse> ClusterStatsAsync(this IElasticClient client,IClusterStatsRequest request, CancellationToken ct = default);
	}

}
