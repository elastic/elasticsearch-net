using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IXPackUsageResponse XPackUsage(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null);

		/// <inheritdoc/>
		IXPackUsageResponse XPackUsage(IXPackUsageRequest request);

		/// <inheritdoc/>
		Task<IXPackUsageResponse> XPackUsageAsync(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IXPackUsageResponse> XPackUsageAsync(IXPackUsageRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IXPackUsageResponse XPackUsage(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null) =>
			this.XPackUsage(selector.InvokeOrDefault(new XPackUsageDescriptor()));

		/// <inheritdoc/>
		public IXPackUsageResponse XPackUsage(IXPackUsageRequest request) =>
			this.Dispatcher.Dispatch<IXPackUsageRequest, XPackUsageRequestParameters, XPackUsageResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackUsageDispatch<XPackUsageResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IXPackUsageResponse> XPackUsageAsync(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.XPackUsageAsync(selector.InvokeOrDefault(new XPackUsageDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IXPackUsageResponse> XPackUsageAsync(IXPackUsageRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IXPackUsageRequest, XPackUsageRequestParameters, XPackUsageResponse, IXPackUsageResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackUsageDispatchAsync<XPackUsageResponse>(p, c)
			);
	}
}
