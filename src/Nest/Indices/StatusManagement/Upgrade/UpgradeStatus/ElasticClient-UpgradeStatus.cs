using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest request);

		/// <inheritdoc />
		IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null);

		/// <inheritdoc />
		Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest request) =>
			Dispatch2<IUpgradeStatusRequest, UpgradeStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null) =>
			UpgradeStatus(selector.InvokeOrDefault(new UpgradeStatusDescriptor()));

		/// <inheritdoc />
		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IUpgradeStatusRequest, IUpgradeStatusResponse, UpgradeStatusResponse>(request, request.RequestParameters, ct);

		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(
			Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null,
			CancellationToken ct = default
		) => UpgradeStatusAsync(selector.InvokeOrDefault(new UpgradeStatusDescriptor()), ct);
	}
}
