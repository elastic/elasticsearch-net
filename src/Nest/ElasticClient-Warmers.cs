using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	public partial class ElasticClient
	{
		public IIndicesOperationResponse PutWarmer(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
            selector.ThrowIfNull("selector");
			return this.Dispatch<PutWarmerDescriptor, PutWarmerQueryString, IndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatch(p, d)
			);
		}
		
		public Task<IIndicesOperationResponse> PutWarmerAsync(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
            selector.ThrowIfNull("selector");
			return this.DispatchAsync<PutWarmerDescriptor, PutWarmerQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatchAsync(p, d)
			);
		}

		public IWarmerResponse GetWarmer(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
		    selector = selector ?? (s => s);
			return this.Dispatch<GetWarmerDescriptor, GetWarmerQueryString, WarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatch(p),
				DeserializeToWarmerResponse
			);
		}

		public Task<IWarmerResponse> GetWarmerAsync(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
		    selector = selector ?? (s => s);
			return this.DispatchAsync<GetWarmerDescriptor, GetWarmerQueryString, WarmerResponse, IWarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatchAsync(p),
				DeserializeToWarmerResponse
			);
		}
		
		private WarmerResponse DeserializeToWarmerResponse(GetWarmerDescriptor getWarmerDescriptor, ConnectionStatus connectionStatus)
		{
			var dict =  connectionStatus.Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, WarmerMapping>>>>();
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
				Indices = indices
			};
		}
		public IIndicesOperationResponse DeleteWarmer(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
		    selector = selector ?? (s => s);
			return this.Dispatch<DeleteWarmerDescriptor, DeleteWarmerQueryString, IndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatch(p)
			);
		}

		public Task<IIndicesOperationResponse> DeleteWarmerAsync(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null)
		{
		    selector = selector ?? (s => s);
			return this.DispatchAsync<DeleteWarmerDescriptor, DeleteWarmerQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatchAsync(p)
			);
		}
	}
}