using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetRollupCapabilitiesResponse GetRollupCapabilities(Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null);

		/// <inheritdoc/>
		IGetRollupCapabilitiesResponse GetRollupCapabilities(IGetRollupCapabilitiesRequest request);

		/// <inheritdoc/>
		Task<IGetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(
			Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null, CancellationToken cancellationToken = default);

		/// <inheritdoc/>
		Task<IGetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(IGetRollupCapabilitiesRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetRollupCapabilitiesResponse GetRollupCapabilities(Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null) =>
			this.GetRollupCapabilities(selector.InvokeOrDefault(new GetRollupCapabilitiesDescriptor()));

		/// <inheritdoc/>
		public IGetRollupCapabilitiesResponse GetRollupCapabilities(IGetRollupCapabilitiesRequest request) =>
			this.Dispatcher.Dispatch<IGetRollupCapabilitiesRequest, GetRollupCapabilitiesRequestParameters, GetRollupCapabilitiesResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackRollupGetRollupCapsDispatch<GetRollupCapabilitiesResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(
			Func<GetRollupCapabilitiesDescriptor, IGetRollupCapabilitiesRequest> selector = null, CancellationToken cancellationToken = default
		)  =>
			this.GetRollupCapabilitiesAsync(selector.InvokeOrDefault(new GetRollupCapabilitiesDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetRollupCapabilitiesResponse> GetRollupCapabilitiesAsync(IGetRollupCapabilitiesRequest request, CancellationToken cancellationToken = default) =>
			this.Dispatcher.DispatchAsync<IGetRollupCapabilitiesRequest, GetRollupCapabilitiesRequestParameters, GetRollupCapabilitiesResponse, IGetRollupCapabilitiesResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackRollupGetRollupCapsDispatchAsync<GetRollupCapabilitiesResponse>(p, c)
			);
	}
}
