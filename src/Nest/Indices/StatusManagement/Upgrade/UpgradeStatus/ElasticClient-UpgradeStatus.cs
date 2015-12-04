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
		IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest upgradeStatusRequest);

		/// <inheritdoc/>
		IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null);

		/// <inheritdoc/>
		Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest upgradeStatusRequest);

		/// <inheritdoc/>
		Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest upgradeStatusRequest) => 
			this.Dispatcher.Dispatch<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse>(
				upgradeStatusRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetUpgradeDispatch<UpgradeStatusResponse>(p)
			);

		/// <inheritdoc/>
		public IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null) =>
			this.UpgradeStatus(selector.InvokeOrDefault(new UpgradeStatusDescriptor()));

		/// <inheritdoc/>
		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest upgradeStatusRequest) => 
			this.Dispatcher.DispatchAsync<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse, IUpgradeStatusResponse>(
				upgradeStatusRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetUpgradeDispatchAsync<UpgradeStatusResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null) => 
			this.UpgradeStatusAsync(selector.InvokeOrDefault(new UpgradeStatusDescriptor()));
	}
}
