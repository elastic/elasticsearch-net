using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IXPackInfoResponse XPackInfo(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null);

		/// <inheritdoc />
		IXPackInfoResponse XPackInfo(IXPackInfoRequest request);

		/// <inheritdoc />
		Task<IXPackInfoResponse> XPackInfoAsync(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IXPackInfoResponse> XPackInfoAsync(IXPackInfoRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IXPackInfoResponse XPackInfo(Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null) =>
			XPackInfo(selector.InvokeOrDefault(new XPackInfoDescriptor()));

		/// <inheritdoc />
		public IXPackInfoResponse XPackInfo(IXPackInfoRequest request) =>
			DoRequest<IXPackInfoRequest, XPackInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IXPackInfoResponse> XPackInfoAsync(
			Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null,
			CancellationToken ct = default
		) => XPackInfoAsync(selector.InvokeOrDefault(new XPackInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<IXPackInfoResponse> XPackInfoAsync(IXPackInfoRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IXPackInfoRequest, IXPackInfoResponse, XPackInfoResponse>(request, request.RequestParameters, ct);
	}
}
