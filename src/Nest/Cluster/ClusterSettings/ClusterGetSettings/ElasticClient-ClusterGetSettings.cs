using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		///     Gets cluster wide specific settings. Settings updated can either be persistent
		///     (applied cross restarts) or transient (will not survive a full cluster restart).
		///     <para> </para>
		///     http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null);

		/// <inheritdoc />
		IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest request);

		/// <inheritdoc />
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null) =>
			ClusterGetSettings(selector.InvokeOrDefault(new ClusterGetSettingsDescriptor()));

		/// <inheritdoc />
		public IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest request) =>
			Dispatcher.Dispatch<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse>(
				request ?? new ClusterGetSettingsRequest(),
				(p, d) => LowLevelDispatch.ClusterGetSettingsDispatch<ClusterGetSettingsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ClusterGetSettingsAsync(selector.InvokeOrDefault(new ClusterGetSettingsDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IClusterGetSettingsRequest, ClusterGetSettingsRequestParameters, ClusterGetSettingsResponse,
					IClusterGetSettingsResponse>(
					request ?? new ClusterGetSettingsRequest(),
					cancellationToken,
					(p, d, c) => LowLevelDispatch.ClusterGetSettingsDispatchAsync<ClusterGetSettingsResponse>(p, c)
				);
	}
}
