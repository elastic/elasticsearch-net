using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeprecationInfoResponse DeprecationInfo(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null);

		/// <inheritdoc/>
		IDeprecationInfoResponse DeprecationInfo(IDeprecationInfoRequest request);

		/// <inheritdoc/>
		Task<IDeprecationInfoResponse> DeprecationInfoAsync(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeprecationInfoResponse> DeprecationInfoAsync(IDeprecationInfoRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeprecationInfoResponse DeprecationInfo(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null) =>
			this.DeprecationInfo(selector.InvokeOrDefault(new DeprecationInfoDescriptor()));

		/// <inheritdoc/>
		public IDeprecationInfoResponse DeprecationInfo(IDeprecationInfoRequest request) =>
			this.Dispatcher.Dispatch<IDeprecationInfoRequest, DeprecationInfoRequestParameters, DeprecationInfoResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackDeprecationInfoDispatch<DeprecationInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeprecationInfoResponse> DeprecationInfoAsync(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeprecationInfoAsync(selector.InvokeOrDefault(new DeprecationInfoDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeprecationInfoResponse> DeprecationInfoAsync(IDeprecationInfoRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeprecationInfoRequest, DeprecationInfoRequestParameters, DeprecationInfoResponse, IDeprecationInfoResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackDeprecationInfoDispatchAsync<DeprecationInfoResponse>(p, c)
			);
	}
}
