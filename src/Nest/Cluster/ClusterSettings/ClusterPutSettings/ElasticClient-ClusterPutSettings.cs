using System;
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
		IClusterPutSettingsResponse ClusterPutSettings(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector);

		/// <inheritdoc/>
		Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector);

		/// <inheritdoc/>
		IClusterPutSettingsResponse ClusterPutSettings(IClusterPutSettingsRequest request);

		/// <inheritdoc/>
		Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request);
	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterPutSettingsResponse ClusterPutSettings(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector) =>
			this.ClusterPutSettings(selector.InvokeOrDefault(new ClusterPutSettingsDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector) =>
			this.ClusterPutSettingsAsync(selector.InvokeOrDefault(new ClusterPutSettingsDescriptor()));

		/// <inheritdoc/>
		public IClusterPutSettingsResponse ClusterPutSettings(IClusterPutSettingsRequest request) => 
			this.Dispatcher.Dispatch<IClusterPutSettingsRequest, ClusterPutSettingsRequestParameters, ClusterPutSettingsResponse>(
				request,
				this.LowLevelDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>
			);

		/// <inheritdoc/>
		public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request) => 
			this.Dispatcher.DispatchAsync<IClusterPutSettingsRequest, ClusterPutSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse>(
				request,
				this.LowLevelDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>
			);
	}
}