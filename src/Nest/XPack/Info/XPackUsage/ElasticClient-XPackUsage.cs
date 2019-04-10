using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IXPackUsageResponse XPackUsage(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null);

		/// <inheritdoc />
		IXPackUsageResponse XPackUsage(IXPackUsageRequest request);

		/// <inheritdoc />
		Task<IXPackUsageResponse> XPackUsageAsync(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IXPackUsageResponse> XPackUsageAsync(IXPackUsageRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IXPackUsageResponse XPackUsage(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null) =>
			XPackUsage(selector.InvokeOrDefault(new XPackUsageDescriptor()));

		/// <inheritdoc />
		public IXPackUsageResponse XPackUsage(IXPackUsageRequest request) =>
			DoRequest<IXPackUsageRequest, XPackUsageResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IXPackUsageResponse> XPackUsageAsync(
			Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null,
			CancellationToken ct = default
		) => XPackUsageAsync(selector.InvokeOrDefault(new XPackUsageDescriptor()), ct);

		/// <inheritdoc />
		public Task<IXPackUsageResponse> XPackUsageAsync(IXPackUsageRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IXPackUsageRequest, IXPackUsageResponse, XPackUsageResponse>(request, request.RequestParameters, ct);
	}
}
