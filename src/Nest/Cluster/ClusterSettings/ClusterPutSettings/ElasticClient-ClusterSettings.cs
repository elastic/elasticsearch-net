using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterPutSettingsResponse ClusterSettings(Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> selector)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, ClusterPutSettingsResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IClusterPutSettingsResponse> ClusterSettingsAsync(Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> selector)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse >(
				selector,
				(p, d) => this.LowLevelDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public IClusterPutSettingsResponse ClusterSettings(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatcher.Dispatch<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse>(
				clusterSettingsRequest,
				(p, d) => this.LowLevelDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>(p, d)
			);
		}
		
		/// <inheritdoc/>
		public Task<IClusterPutSettingsResponse> ClusterSettingsAsync(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatcher.DispatchAsync<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse >(
				clusterSettingsRequest,
				(p, d) => this.LowLevelDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>(p, d)
			);
		}
		
	}
}