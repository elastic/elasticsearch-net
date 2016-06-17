using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IUpgradeResponse Upgrade(IUpgradeRequest request);

		/// <inheritdoc/>
		IUpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null);

		/// <inheritdoc/>
		Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		public IUpgradeResponse Upgrade(IUpgradeRequest request) =>
			this.Dispatcher.Dispatch<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesUpgradeDispatch<UpgradeResponse>(p)
			);

		public IUpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null) =>
			this.Upgrade(selector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)));

		public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p, c)
			);

		public Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.UpgradeAsync(selector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)), cancellationToken);
	}
}
