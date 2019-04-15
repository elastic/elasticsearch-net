using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		UpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest request);

		/// <inheritdoc />
		UpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null);

		/// <inheritdoc />
		Task<UpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<UpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public UpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest request) =>
			DoRequest<IUpgradeStatusRequest, UpgradeStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public UpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null) =>
			UpgradeStatus(selector.InvokeOrDefault(new UpgradeStatusDescriptor()));

		/// <inheritdoc />
		public Task<UpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUpgradeStatusRequest, UpgradeStatusResponse, UpgradeStatusResponse>(request, request.RequestParameters, ct);

		public Task<UpgradeStatusResponse> UpgradeStatusAsync(
			Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null,
			CancellationToken ct = default
		) => UpgradeStatusAsync(selector.InvokeOrDefault(new UpgradeStatusDescriptor()), ct);
	}
}
