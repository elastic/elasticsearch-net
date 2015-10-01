using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets cluster wide specific settings. Settings updated can either be persistent 
		/// (applied cross restarts) or transient (will not survive a full cluster restart). 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null);

		/// <inheritdoc/>
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null);

		/// <inheritdoc/>
		IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest clusterSettingsRequest);

		/// <inheritdoc/>
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest clusterSettingsRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null) =>
			this.ClusterGetSettings(selector.InvokeOrDefault(new ClusterGetSettingsDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null) =>
			this.ClusterGetSettingsAsync(selector.InvokeOrDefault(new ClusterGetSettingsDescriptor()));

		/// <inheritdoc/>
		public IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest clusterSettingsRequest) => 
			this.Dispatcher.Dispatch<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse>(
				clusterSettingsRequest ?? new ClusterGetSettingsRequest(),
				(p, d) => this.LowLevelDispatch.ClusterGetSettingsDispatch<ClusterGetSettingsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest clusterSettingsRequest = null) => 
			this.Dispatcher.DispatchAsync<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse, IClusterGetSettingsResponse>(
				clusterSettingsRequest ?? new ClusterGetSettingsRequest(),
				(p, d) => this.LowLevelDispatch.ClusterGetSettingsDispatchAsync<ClusterGetSettingsResponse>(p)
			);
	}
}