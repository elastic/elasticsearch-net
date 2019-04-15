using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		XPackUsageResponse XPackUsage(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null);

		/// <inheritdoc />
		XPackUsageResponse XPackUsage(IXPackUsageRequest request);

		/// <inheritdoc />
		Task<XPackUsageResponse> XPackUsageAsync(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<XPackUsageResponse> XPackUsageAsync(IXPackUsageRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public XPackUsageResponse XPackUsage(Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null) =>
			XPackUsage(selector.InvokeOrDefault(new XPackUsageDescriptor()));

		/// <inheritdoc />
		public XPackUsageResponse XPackUsage(IXPackUsageRequest request) =>
			DoRequest<IXPackUsageRequest, XPackUsageResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<XPackUsageResponse> XPackUsageAsync(
			Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null,
			CancellationToken ct = default
		) => XPackUsageAsync(selector.InvokeOrDefault(new XPackUsageDescriptor()), ct);

		/// <inheritdoc />
		public Task<XPackUsageResponse> XPackUsageAsync(IXPackUsageRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IXPackUsageRequest, XPackUsageResponse, XPackUsageResponse>(request, request.RequestParameters, ct);
	}
}
