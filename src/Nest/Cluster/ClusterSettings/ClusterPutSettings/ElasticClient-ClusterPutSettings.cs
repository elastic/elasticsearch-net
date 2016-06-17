using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

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
		Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		IClusterPutSettingsResponse ClusterPutSettings(IClusterPutSettingsRequest request);

		/// <inheritdoc/>
		Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterPutSettingsResponse ClusterPutSettings(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector) =>
			this.ClusterPutSettings(selector.InvokeOrDefault(new ClusterPutSettingsDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ClusterPutSettingsAsync(selector.InvokeOrDefault(new ClusterPutSettingsDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public IClusterPutSettingsResponse ClusterPutSettings(IClusterPutSettingsRequest request) =>
			this.Dispatcher.Dispatch<IClusterPutSettingsRequest, ClusterPutSettingsRequestParameters, ClusterPutSettingsResponse>(
				request,
				this.LowLevelDispatch.ClusterPutSettingsDispatch<ClusterPutSettingsResponse>
			);

		/// <inheritdoc/>
		public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IClusterPutSettingsRequest, ClusterPutSettingsRequestParameters, ClusterPutSettingsResponse, IClusterPutSettingsResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.ClusterPutSettingsDispatchAsync<ClusterPutSettingsResponse>
			);
	}
}
