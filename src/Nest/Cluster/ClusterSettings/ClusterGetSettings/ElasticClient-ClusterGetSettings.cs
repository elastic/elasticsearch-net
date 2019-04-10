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
		IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest request);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null) =>
			ClusterGetSettings(selector.InvokeOrDefault(new ClusterGetSettingsDescriptor()));

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest request) =>
			DoRequest<IClusterGetSettingsRequest, ClusterGetSettingsResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null,
			CancellationToken ct = default
		) =>
			ClusterGetSettingsAsync(selector.InvokeOrDefault(new ClusterGetSettingsDescriptor()), ct);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IClusterGetSettingsRequest, IClusterGetSettingsResponse, ClusterGetSettingsResponse>(request, request.RequestParameters, ct);
	}
}
