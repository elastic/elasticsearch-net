using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.ClusterGetSettingsDispatch<ClusterGetSettingsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<ClusterGetSettingsDescriptor, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse, IClusterGetSettingsResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.ClusterGetSettingsDispatchAsync<ClusterGetSettingsResponse>(p)
			);
		}

		/// <inheritdoc />
		public IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatcher.Dispatch<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse>(
				clusterSettingsRequest ?? new ClusterGetSettingsRequest(),
				(p, d) => this.LowLevelDispatch.ClusterGetSettingsDispatch<ClusterGetSettingsResponse>(p)
			);
		}
		
		/// <inheritdoc />
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest clusterSettingsRequest = null)
		{
			return this.Dispatcher.DispatchAsync<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse, IClusterGetSettingsResponse>(
				clusterSettingsRequest ?? new ClusterGetSettingsRequest(),
				(p, d) => this.LowLevelDispatch.ClusterGetSettingsDispatchAsync<ClusterGetSettingsResponse>(p)
			);
		}
	}
}