using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Allows to update cluster wide specific settings. Settings updated can either be persistent
		/// (applied cross restarts) or transient (will not survive a full cluster restart).
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		IClusterPutSettingsResponse ClusterPutSettings(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector);

		/// <inheritdoc />
		Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		IClusterPutSettingsResponse ClusterPutSettings(IClusterPutSettingsRequest request);

		/// <inheritdoc />
		Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterPutSettingsResponse ClusterPutSettings(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector) =>
			ClusterPutSettings(selector.InvokeOrDefault(new ClusterPutSettingsDescriptor()));

		/// <inheritdoc />
		public IClusterPutSettingsResponse ClusterPutSettings(IClusterPutSettingsRequest request) =>
			DoRequest<IClusterPutSettingsRequest, ClusterPutSettingsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector,
			CancellationToken ct = default
		) =>
			ClusterPutSettingsAsync(selector.InvokeOrDefault(new ClusterPutSettingsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IClusterPutSettingsRequest, IClusterPutSettingsResponse, ClusterPutSettingsResponse>(request, request.RequestParameters, ct);
	}
}
