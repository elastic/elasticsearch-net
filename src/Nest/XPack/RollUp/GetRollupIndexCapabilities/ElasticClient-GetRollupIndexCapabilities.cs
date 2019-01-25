using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// </summary>
		IGetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null
		);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		IGetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(IGetRollupIndexCapabilitiesRequest request);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		Task<IGetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		Task<IGetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(IGetRollupIndexCapabilitiesRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public IGetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null
		) =>
			GetRollupIndexCapabilities(selector.InvokeOrDefault(new GetRollupIndexCapabilitiesDescriptor(index)));

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public IGetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(IGetRollupIndexCapabilitiesRequest request) =>
			Dispatcher.Dispatch<IGetRollupIndexCapabilitiesRequest, GetRollupIndexCapabilitiesRequestParameters, GetRollupIndexCapabilitiesResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackRollupGetRollupIndexCapsDispatch<GetRollupIndexCapabilitiesResponse>(p)
			);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public Task<IGetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetRollupIndexCapabilitiesAsync(selector.InvokeOrDefault(new GetRollupIndexCapabilitiesDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public Task<IGetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(IGetRollupIndexCapabilitiesRequest request,
			CancellationToken cancellationToken = default
		) =>
			Dispatcher
				.DispatchAsync<IGetRollupIndexCapabilitiesRequest, GetRollupIndexCapabilitiesRequestParameters, GetRollupIndexCapabilitiesResponse,
					IGetRollupIndexCapabilitiesResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.XpackRollupGetRollupIndexCapsDispatchAsync<GetRollupIndexCapabilitiesResponse>(p, c)
				);
	}
}
