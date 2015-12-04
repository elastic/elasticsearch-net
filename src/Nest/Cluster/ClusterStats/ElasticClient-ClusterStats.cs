using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// allows to retrieve statistics from a cluster wide perspective. The API returns basic index metrics 
		/// (shard numbers, store size, memory usage) and information about the current nodes that form the 
		/// cluster (number, roles, os, jvm versions, memory usage, cpu and installed plugins).
		/// </summary>
		/// <para> </para><a href="https://www.elastic.co/guide/en/elasticsearch/guide/current/_cluster_stats.html">https://www.elastic.co/guide/en/elasticsearch/guide/current/_cluster_stats.html</a>
		/// <param name="selector">A descriptor that describes the cluster stats operation</param>
		IClusterStatsResponse ClusterStats(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null);

		/// <inheritdoc/>
		Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null);

		/// <inheritdoc/>
		IClusterStatsResponse ClusterStats(IClusterStatsRequest clusterStatsRequest);

		/// <inheritdoc/>
		Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest clusterStatsRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterStatsResponse ClusterStats(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null) =>
			this.ClusterStats(selector.InvokeOrDefault(new ClusterStatsDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null) =>
			this.ClusterStatsAsync(selector.InvokeOrDefault(new ClusterStatsDescriptor()));

		/// <inheritdoc/>
		public IClusterStatsResponse ClusterStats(IClusterStatsRequest clusterStatsRequest) => 
			this.Dispatcher.Dispatch<IClusterStatsRequest, ClusterStatsRequestParameters, ClusterStatsResponse>(
				clusterStatsRequest,
				(p, d) => this.LowLevelDispatch.ClusterStatsDispatch<ClusterStatsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest clusterStatsRequest) => 
			this.Dispatcher.DispatchAsync<IClusterStatsRequest, ClusterStatsRequestParameters, ClusterStatsResponse, IClusterStatsResponse>(
				clusterStatsRequest,
				(p, d) => this.LowLevelDispatch.ClusterStatsDispatchAsync<ClusterStatsResponse>(p)
			);
	}
}
