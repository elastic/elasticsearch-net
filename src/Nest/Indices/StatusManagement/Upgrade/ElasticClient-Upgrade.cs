using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		UpgradeResponse Upgrade(IUpgradeRequest request);

		/// <inheritdoc />
		UpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null);

		/// <inheritdoc />
		Task<UpgradeResponse> UpgradeAsync(IUpgradeRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		Task<UpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		public UpgradeResponse Upgrade(IUpgradeRequest request) =>
			DoRequest<IUpgradeRequest, UpgradeResponse>(request, request.RequestParameters);

		public UpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null) =>
			Upgrade(selector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)));

		public Task<UpgradeResponse> UpgradeAsync(IUpgradeRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUpgradeRequest, UpgradeResponse>(request, request.RequestParameters, ct);

		public Task<UpgradeResponse> UpgradeAsync(
			Indices indices,
			Func<UpgradeDescriptor, IUpgradeRequest> selector = null,
			CancellationToken ct = default
		) => UpgradeAsync(selector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)), ct);
	}
}
