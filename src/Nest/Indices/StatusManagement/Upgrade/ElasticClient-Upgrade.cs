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
		IUpgradeResponse Upgrade(IUpgradeRequest upgradeRequest);

		/// <inheritdoc/>
		IUpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> upgradeSelector = null);

		/// <inheritdoc/>
		Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest upgradeRequest);

		/// <inheritdoc/>
		Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> upgradeSelector = null);
	}

	public partial class ElasticClient
	{
		public IUpgradeResponse Upgrade(IUpgradeRequest upgradeRequest) =>
			this.Dispatcher.Dispatch<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse>(
				upgradeRequest,
				(p, d) => this.LowLevelDispatch.IndicesUpgradeDispatch<UpgradeResponse>(p)
			);

		public IUpgradeResponse Upgrade(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> upgradeSelector = null) =>
			this.Upgrade(upgradeSelector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)));

		public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest upgradeRequest) =>
			this.Dispatcher.DispatchAsync<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				upgradeRequest,
				(p, d) => this.LowLevelDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p)
			);

		public Task<IUpgradeResponse> UpgradeAsync(Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> upgradeSelector = null) =>
			this.UpgradeAsync(upgradeSelector.InvokeOrDefault(new UpgradeDescriptor().Index(indices)));
	}
}
