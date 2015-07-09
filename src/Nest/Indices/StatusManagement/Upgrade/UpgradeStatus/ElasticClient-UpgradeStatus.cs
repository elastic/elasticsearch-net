using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest upgradeStatusRequest)
		{
			return this.Dispatcher.Dispatch<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse>(
				upgradeStatusRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetUpgradeDispatch<UpgradeStatusResponse>(p)
			);	
		}

		public IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, UpgradeStatusDescriptor> upgradeStatusDescriptor = null)
		{
			upgradeStatusDescriptor = upgradeStatusDescriptor ?? (s => s);
			return this.Dispatcher.Dispatch<UpgradeStatusDescriptor, UpgradeStatusRequestParameters, UpgradeStatusResponse>(
				upgradeStatusDescriptor,
				(p, d) => this.LowLevelDispatch.IndicesGetUpgradeDispatch<UpgradeStatusResponse>(p)
			);
		}

		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest upgradeStatusRequest)
		{
			return this.Dispatcher.DispatchAsync<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse, IUpgradeStatusResponse>(
				upgradeStatusRequest,
				(p, d) => this.LowLevelDispatch.IndicesGetUpgradeDispatchAsync<UpgradeStatusResponse>(p)
			);
		}

		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, UpgradeStatusDescriptor> upgradeStatusDescriptor = null)
		{
			upgradeStatusDescriptor = upgradeStatusDescriptor ?? (s => s);
			return this.Dispatcher.DispatchAsync<UpgradeStatusDescriptor, UpgradeStatusRequestParameters, UpgradeStatusResponse, IUpgradeStatusResponse>(
				upgradeStatusDescriptor,
				(p, d) => this.LowLevelDispatch.IndicesGetUpgradeDispatchAsync<UpgradeStatusResponse>(p)
			);
		}
	}
}
