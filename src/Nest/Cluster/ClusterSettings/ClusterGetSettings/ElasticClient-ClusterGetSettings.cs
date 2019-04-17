using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets cluster wide specific settings. Settings updated can either be persistent
		/// (applied cross restarts) or transient (will not survive a full cluster restart).
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		ClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		ClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest request);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public ClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null) =>
			ClusterGetSettings(selector.InvokeOrDefault(new ClusterGetSettingsDescriptor()));

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public ClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest request) =>
			DoRequest<IClusterGetSettingsRequest, ClusterGetSettingsResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null,
			CancellationToken ct = default
		) => ClusterGetSettingsAsync(selector.InvokeOrDefault(new ClusterGetSettingsDescriptor()), ct);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(
			IClusterGetSettingsRequest request,
			CancellationToken ct = default
		) => DoRequestAsync<IClusterGetSettingsRequest, ClusterGetSettingsResponse>(request, request.RequestParameters, ct);
	}
}
