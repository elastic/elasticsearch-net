using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	using GetWarmerConverter = Func<IElasticsearchResponse, Stream, WarmerResponse>;
	public partial class ElasticClient
	{
		public IIndicesOperationResponse PutWarmer(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			return this.Dispatch<PutWarmerDescriptor, PutWarmerQueryString, IndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatch<IndicesOperationResponse>(p, d)
			);
		}

		public Task<IIndicesOperationResponse> PutWarmerAsync(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			return this.DispatchAsync<PutWarmerDescriptor, PutWarmerQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatchAsync<IndicesOperationResponse>(p, d)
			);
		}

		public IWarmerResponse GetWarmer(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<GetWarmerDescriptor, GetWarmerQueryString, WarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatch<WarmerResponse>(
					p, 
					new GetWarmerConverter(this.DeserializeWarmerResponse)
				)
			);
		}

		public Task<IWarmerResponse> GetWarmerAsync(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<GetWarmerDescriptor, GetWarmerQueryString, WarmerResponse, IWarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatchAsync<WarmerResponse>(
					p, 
					new GetWarmerConverter(this.DeserializeWarmerResponse)
				)
			);
		}
		
		private WarmerResponse DeserializeWarmerResponse(IElasticsearchResponse connectionStatus, Stream stream)
		{
			if (!connectionStatus.Success)
				return new WarmerResponse() { ConnectionStatus = connectionStatus, IsValid = false };

			var dict = this.Serializer.DeserializeInternal<Dictionary<string, Dictionary<string, Dictionary<string, WarmerMapping>>>>(stream);
			var indices = new Dictionary<string, Dictionary<string, WarmerMapping>>();
			foreach (var kv in dict)
			{
				var indexDict = kv.Value;
				Dictionary<string, WarmerMapping> warmers;
				if (indexDict == null || !indexDict.TryGetValue("warmers", out warmers) || warmers == null)
					continue;
				foreach (var kvW in warmers)
				{
					kvW.Value.Name = kvW.Key;
				}
				indices.Add(kv.Key, warmers);
			}

			return new WarmerResponse
			{
				ConnectionStatus = connectionStatus,
				IsValid = true,
				Indices = indices
			};
		}
		
		public IIndicesOperationResponse DeleteWarmer(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<DeleteWarmerDescriptor, DeleteWarmerQueryString, IndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatch<IndicesOperationResponse>(p)
			);
		}

		public Task<IIndicesOperationResponse> DeleteWarmerAsync(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<DeleteWarmerDescriptor, DeleteWarmerQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatchAsync<IndicesOperationResponse>(p)
			);
		}
	}
}