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
		public IIndicesResponse ClusterSettings(
			Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> clusterHealthSelector = null)
		{
			clusterHealthSelector = clusterHealthSelector ?? (s => s);
			return this.Dispatch<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, IndicesResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatch<IndicesResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IIndicesResponse> ClusterSettingsAsync(
			Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> clusterHealthSelector = null)
		{
			clusterHealthSelector = clusterHealthSelector ?? (s => s);
			return this.DispatchAsync<ClusterSettingsDescriptor, ClusterSettingsRequestParameters, IndicesResponse, IIndicesResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatchAsync<IndicesResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IIndicesResponse ClusterSettings(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.Dispatch<IClusterSettingsRequest, ClusterSettingsRequestParameters, IndicesResponse>(
				clusterSettingsRequest,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatch<IndicesResponse>(p, d)
			);
		}
		
		/// <inheritdoc />
		public Task<IIndicesResponse> ClusterSettingsAsync(IClusterSettingsRequest clusterSettingsRequest)
		{
			return this.DispatchAsync<IClusterSettingsRequest, ClusterSettingsRequestParameters, IndicesResponse, IIndicesResponse>(
				clusterSettingsRequest,
				(p, d) => this.RawDispatch.ClusterPutSettingsDispatchAsync<IndicesResponse>(p, d)
			);
		}
	}
}