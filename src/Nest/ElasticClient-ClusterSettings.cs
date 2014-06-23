using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterPutSettingsResponse ClusterSettings(
			Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, ClusterPutSettingsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IClusterPutSettingsResponse > ClusterSettingsAsync(
			Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse >(
				selector,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IClusterPutSettingsResponse ClusterSettings(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatch<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse>(
				clusterSettingsRequest,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>(p, d)
			);
		}
		
		/// <inheritdoc />
		public Task<IClusterPutSettingsResponse > ClusterSettingsAsync(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.DispatchAsync<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse >(
				clusterSettingsRequest,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>(p, d)
			);
		}
	}
}