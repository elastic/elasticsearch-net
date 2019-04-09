using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IUpgradeResponse Upgrade(IUpgradeRequest request);

		/// <inheritdoc />
		IUpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null);

		/// <inheritdoc />
		Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		public IUpgradeResponse Upgrade(IUpgradeRequest request) =>
			Dispatch2<IUpgradeRequest, UpgradeResponse>(request, request.RequestParameters);

		public IUpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null) =>
			Upgrade(selector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)));

		public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IUpgradeRequest, IUpgradeResponse, UpgradeResponse>(request, request.RequestParameters, ct);

		public Task<IUpgradeResponse> UpgradeAsync(
			Indices indices,
			Func<UpgradeDescriptor, IUpgradeRequest> selector = null,
			CancellationToken ct = default
		) => UpgradeAsync(selector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)), ct);
	}
}
