using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	using UpgradeStatusResponseConverter = Func<IElasticsearchResponse, Stream, UpgradeStatusResponse>;

	public partial class ElasticClient
	{
		public IUpgradeResponse Upgrade(IUpgradeRequest upgradeRequest)
		{
			return this.Dispatch<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse>(
				upgradeRequest,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatch<UpgradeResponse>(p)
			);	
		}

		public IUpgradeResponse Upgrade(Func<UpgradeDescriptor, UpgradeDescriptor> upgradeDescriptor = null)
		{
			upgradeDescriptor = upgradeDescriptor ?? (s => s);
			return this.Dispatch<UpgradeDescriptor, UpgradeRequestParameters, UpgradeResponse>(
				upgradeDescriptor,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatch<UpgradeResponse>(p)
			);
		}

		public Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest upgradeRequest)
		{
			return this.DispatchAsync<IUpgradeRequest, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				upgradeRequest,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p)
			);
		}

		public Task<IUpgradeResponse> UpgradeAsync(Func<UpgradeDescriptor, UpgradeDescriptor> upgradeDescriptor = null)
		{
			upgradeDescriptor = upgradeDescriptor ?? (s => s);
			return this.DispatchAsync<UpgradeDescriptor, UpgradeRequestParameters, UpgradeResponse, IUpgradeResponse>(
				upgradeDescriptor,
				(p, d) => this.RawDispatch.IndicesUpgradeDispatchAsync<UpgradeResponse>(p)
			);
		}

		public IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest upgradeStatusRequest)
		{
			return this.Dispatch<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse>(
				upgradeStatusRequest,
				(p, d) => this.RawDispatch.IndicesGetUpgradeDispatch<UpgradeStatusResponse>(
					p.DeserializationState(new UpgradeStatusResponseConverter((r, s) => DeserializeUpgradeStatusResponse(r, s)))
				)
			);	
		}

		public IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, UpgradeStatusDescriptor> upgradeStatusDescriptor = null)
		{
			upgradeStatusDescriptor = upgradeStatusDescriptor ?? (s => s);
			return this.Dispatch<UpgradeStatusDescriptor, UpgradeStatusRequestParameters, UpgradeStatusResponse>(
				upgradeStatusDescriptor,
				(p, d) => this.RawDispatch.IndicesGetUpgradeDispatch<UpgradeStatusResponse>(
					p.DeserializationState(new UpgradeStatusResponseConverter((r, s) => DeserializeUpgradeStatusResponse(r, s)))
				)
			);
		}

		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest upgradeStatusRequest)
		{
			return this.DispatchAsync<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse, IUpgradeStatusResponse>(
				upgradeStatusRequest,
				(p, d) => this.RawDispatch.IndicesGetUpgradeDispatchAsync<UpgradeStatusResponse>(
					p.DeserializationState(new UpgradeStatusResponseConverter((r, s) => DeserializeUpgradeStatusResponse(r, s)))
				)
			);
		}

		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, UpgradeStatusDescriptor> upgradeStatusDescriptor = null)
		{
			upgradeStatusDescriptor = upgradeStatusDescriptor ?? (s => s);
			return this.DispatchAsync<UpgradeStatusDescriptor, UpgradeStatusRequestParameters, UpgradeStatusResponse, IUpgradeStatusResponse>(
				upgradeStatusDescriptor,
				(p, d) => this.RawDispatch.IndicesGetUpgradeDispatchAsync<UpgradeStatusResponse>(
					p.DeserializationState(new UpgradeStatusResponseConverter((r, s) => DeserializeUpgradeStatusResponse(r, s)))
				)
			);
		}

		private UpgradeStatusResponse DeserializeUpgradeStatusResponse(IElasticsearchResponse response, Stream stream)
		{
			return new UpgradeStatusResponse
			{
				Upgrades = this.Serializer.Deserialize<Dictionary<string, UpgradeStatus>>(stream)
			};
		}
	}
}
