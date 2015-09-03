using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Allows to update cluster wide specific settings. Settings updated can either be persistent 
		/// (applied cross restarts) or transient (will not survive a full cluster restart). 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		IClusterPutSettingsResponse ClusterSettings(Func<ClusterSettingsDescriptor, IClusterSettingsRequest> clusterHealthSelector);

		/// <inheritdoc/>
		Task<IClusterPutSettingsResponse> ClusterSettingsAsync(Func<ClusterSettingsDescriptor, IClusterSettingsRequest> clusterHealthSelector);

		/// <inheritdoc/>
		IClusterPutSettingsResponse ClusterSettings(IClusterSettingsRequest clusterSettingsRequest);

		/// <inheritdoc/>
		Task<IClusterPutSettingsResponse> ClusterSettingsAsync(IClusterSettingsRequest clusterSettingsRequest);
	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterPutSettingsResponse ClusterSettings(Func<ClusterSettingsDescriptor, IClusterSettingsRequest> selector) =>
			this.ClusterSettings(selector?.Invoke(new ClusterSettingsDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterPutSettingsResponse> ClusterSettingsAsync(Func<ClusterSettingsDescriptor, IClusterSettingsRequest> selector) =>
			this.ClusterSettingsAsync(selector?.Invoke(new ClusterSettingsDescriptor()));

		/// <inheritdoc/>
		public IClusterPutSettingsResponse ClusterSettings(IClusterSettingsRequest clusterSettingsRequest) => 
			this.Dispatcher.Dispatch<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse>(
				clusterSettingsRequest,
				this.LowLevelDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>
			);

		/// <inheritdoc/>
		public Task<IClusterPutSettingsResponse> ClusterSettingsAsync(IClusterSettingsRequest clusterSettingsRequest) => 
			this.Dispatcher.DispatchAsync<IClusterSettingsRequest, ClusterSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse>(
				clusterSettingsRequest,
				this.LowLevelDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>
			);
	}
}