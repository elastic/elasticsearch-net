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
		public IUpgradeResponse Upgrade(IUpgradeRequest upgradeRequest)
		{
			return this.Dispatcher.Dispatch<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse>(
				upgradeRequest,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatch<UpgradeResponse>(p)
			);	
		}

		public IUpgradeResponse Upgrade(Func<UpgradeDescriptor, UpgradeDescriptor> upgradeDescriptor = null)
		{
			upgradeDescriptor = upgradeDescriptor ?? (s => s);
			return this.Dispatcher.Dispatch<UpgradeDescriptor, UpgradeRequestParameters, UpgradeResponse>(
				upgradeDescriptor,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatch<UpgradeResponse>(p)
			);
		}

		public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest upgradeRequest)
		{
			return this.Dispatcher.DispatchAsync<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				upgradeRequest,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p)
			);
		}

		public Task<IUpgradeResponse> UpgradeAsync(Func<UpgradeDescriptor, UpgradeDescriptor> upgradeDescriptor = null)
		{
			upgradeDescriptor = upgradeDescriptor ?? (s => s);
			return this.Dispatcher.DispatchAsync<UpgradeDescriptor, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				upgradeDescriptor,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p)
			);
		}

	}
}
