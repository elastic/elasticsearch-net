using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IXPackInfoResponse XPackInfo(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null);

		/// <inheritdoc/>
		IXPackInfoResponse XPackInfo(IXPackInfoRequest request);

		/// <inheritdoc/>
		Task<IXPackInfoResponse> XPackInfoAsync(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IXPackInfoResponse> XPackInfoAsync(IXPackInfoRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IXPackInfoResponse XPackInfo(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null) =>
			this.XPackInfo(selector.InvokeOrDefault(new XPackInfoDescriptor()));

		/// <inheritdoc/>
		public IXPackInfoResponse XPackInfo(IXPackInfoRequest request) =>
			this.Dispatcher.Dispatch<IXPackInfoRequest, XPackInfoRequestParameters, XPackInfoResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackInfoDispatch<XPackInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IXPackInfoResponse> XPackInfoAsync(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.XPackInfoAsync(selector.InvokeOrDefault(new XPackInfoDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IXPackInfoResponse> XPackInfoAsync(IXPackInfoRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IXPackInfoRequest, XPackInfoRequestParameters, XPackInfoResponse, IXPackInfoResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackInfoDispatchAsync<XPackInfoResponse>(p, c)
			);
	}
}
