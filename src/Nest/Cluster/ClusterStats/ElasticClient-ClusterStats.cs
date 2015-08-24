using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterStatsResponse ClusterStats(Func<ClusterStatsDescriptor, ClusterStatsDescriptor> clusterStatsSelector = null)
		{
			clusterStatsSelector = clusterStatsSelector ?? (s => s);
			return this.Dispatcher.Dispatch<ClusterStatsDescriptor, ClusterStatsRequestParameters, ClusterStatsResponse>(
				clusterStatsSelector,
				(p, d) => this.LowLevelDispatch.ClusterStatsDispatch<ClusterStatsResponse>(p)
			);
		}

		/// <inheritdoc/>
		public Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, ClusterStatsDescriptor> clusterStatsSelector = null)
		{
			clusterStatsSelector = clusterStatsSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<ClusterStatsDescriptor, ClusterStatsRequestParameters, ClusterStatsResponse, IClusterStatsResponse>(
				clusterStatsSelector,
				(p, d) => this.LowLevelDispatch.ClusterStatsDispatchAsync<ClusterStatsResponse>(p)
			);
		}

		/// <inheritdoc/>
		public IClusterStatsResponse ClusterStats(IClusterStatsRequest clusterStatsRequest)
		{
			return this.Dispatcher.Dispatch<IClusterStatsRequest, ClusterStatsRequestParameters, ClusterStatsResponse>(
				clusterStatsRequest,
				(p, d) => this.LowLevelDispatch.ClusterStatsDispatch<ClusterStatsResponse>(p)
			);
		}

		/// <inheritdoc/>
		public Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest clusterStatsRequest)
		{
			return this.Dispatcher.DispatchAsync<IClusterStatsRequest, ClusterStatsRequestParameters, ClusterStatsResponse, IClusterStatsResponse>(
				clusterStatsRequest,
				(p, d) => this.LowLevelDispatch.ClusterStatsDispatchAsync<ClusterStatsResponse>(p)
			);
		}
	}
}
