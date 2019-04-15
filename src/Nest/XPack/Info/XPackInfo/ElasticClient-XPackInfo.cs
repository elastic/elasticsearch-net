using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		XPackInfoResponse XPackInfo(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null);

		/// <inheritdoc />
		XPackInfoResponse XPackInfo(IXPackInfoRequest request);

		/// <inheritdoc />
		Task<XPackInfoResponse> XPackInfoAsync(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<XPackInfoResponse> XPackInfoAsync(IXPackInfoRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public XPackInfoResponse XPackInfo(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null) =>
			XPackInfo(selector.InvokeOrDefault(new XPackInfoDescriptor()));

		/// <inheritdoc />
		public XPackInfoResponse XPackInfo(IXPackInfoRequest request) =>
			DoRequest<IXPackInfoRequest, XPackInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<XPackInfoResponse> XPackInfoAsync(
			Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null,
			CancellationToken ct = default
		) => XPackInfoAsync(selector.InvokeOrDefault(new XPackInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<XPackInfoResponse> XPackInfoAsync(IXPackInfoRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IXPackInfoRequest, XPackInfoResponse, XPackInfoResponse>(request, request.RequestParameters, ct);
	}
}
