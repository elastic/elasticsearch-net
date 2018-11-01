using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		///     allows to retrieve statistics from a cluster wide perspective. The API returns basic index metrics
		///     (shard numbers, store size, memory usage) and information about the current nodes that form the
		///     cluster (number, roles, os, jvm versions, memory usage, cpu and installed plugins).
		/// </summary>
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/guide/current/_cluster_stats.html">https://www.elastic.co/guide/en/elasticsearch/guide/current/_cluster_stats.html</a>
		/// <param name="selector">A descriptor that describes the cluster stats operation</param>
		IClusterStatsResponse ClusterStats(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null);

		/// <inheritdoc />
		IClusterStatsResponse ClusterStats(IClusterStatsRequest request);

		/// <inheritdoc />
		Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterStatsResponse ClusterStats(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null) =>
			ClusterStats(selector.InvokeOrDefault(new ClusterStatsDescriptor()));

		/// <inheritdoc />
		public IClusterStatsResponse ClusterStats(IClusterStatsRequest request) =>
			Dispatcher.Dispatch<IClusterStatsRequest, ClusterStatsRequestParameters, ClusterStatsResponse>(
				request,
				(p, d) => LowLevelDispatch.ClusterStatsDispatch<ClusterStatsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ClusterStatsAsync(selector.InvokeOrDefault(new ClusterStatsDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IClusterStatsRequest, ClusterStatsRequestParameters, ClusterStatsResponse, IClusterStatsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.ClusterStatsDispatchAsync<ClusterStatsResponse>(p, c)
			);
	}
}
