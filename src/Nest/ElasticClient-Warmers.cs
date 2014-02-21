using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
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
				this.Serializer.DeserializeWarmerResponse
			);
		}

		public Task<IWarmerResponse> GetWarmerAsync(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<GetWarmerDescriptor, GetWarmerQueryString, WarmerResponse, IWarmerResponse>(
				d => selector(d.Name(name).AllIndices()),
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatchAsync(p),
				this.Serializer.DeserializeWarmerResponse
			);
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