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
		ClusterPutSettingsResponse ClusterPutSettings(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector);

		/// <inheritdoc />
		Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		ClusterPutSettingsResponse ClusterPutSettings(IClusterPutSettingsRequest request);

		/// <inheritdoc />
		Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ClusterPutSettingsResponse ClusterPutSettings(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector) =>
			ClusterPutSettings(selector.InvokeOrDefault(new ClusterPutSettingsDescriptor()));

		/// <inheritdoc />
		public ClusterPutSettingsResponse ClusterPutSettings(IClusterPutSettingsRequest request) =>
			DoRequest<IClusterPutSettingsRequest, ClusterPutSettingsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector,
			CancellationToken ct = default
		) =>
			ClusterPutSettingsAsync(selector.InvokeOrDefault(new ClusterPutSettingsDescriptor()), ct);

		/// <inheritdoc />
		public Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(IClusterPutSettingsRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IClusterPutSettingsRequest, ClusterPutSettingsResponse, ClusterPutSettingsResponse>(request, request.RequestParameters, ct);
	}
}
