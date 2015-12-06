using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IUpgradeResponse Upgrade(IUpgradeRequest request);

		/// <inheritdoc/>
		IUpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null);

		/// <inheritdoc/>
		Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request);

		/// <inheritdoc/>
		Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null);
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

		public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest request) =>
			this.Dispatcher.DispatchAsync<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p)
			);

		public Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null) =>
			this.UpgradeAsync(selector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)));
	}
}
