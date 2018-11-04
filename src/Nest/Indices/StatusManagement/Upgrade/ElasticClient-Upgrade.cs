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
		Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc />
		Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		public IUpgradeResponse Upgrade(IUpgradeRequest request) =>
			Dispatcher.Dispatch<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesUpgradeDispatch<UpgradeResponse>(p)
			);

		public IUpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null) =>
			Upgrade(selector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)));

		public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p, c)
			);

		public Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			UpgradeAsync(selector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)), cancellationToken);
	}
}
