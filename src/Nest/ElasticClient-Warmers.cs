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

		public IIndicesOperationResponse PutWarmer(Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
			return this.Dispatch<PutWarmerDescriptor, PutWarmerQueryString, IndicesOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatch(p, d)
			);
		}
		
		public Task<IIndicesOperationResponse> PutWarmerAsync(Func<PutWarmerDescriptor, PutWarmerDescriptor> selector)
		{
			return this.DispatchAsync<PutWarmerDescriptor, PutWarmerQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesPutWarmerDispatchAsync(p, d)
			);
		}

		public IWarmerResponse GetWarmer(Func<GetWarmerDescriptor, GetWarmerDescriptor> selector)
		{
			return this.Dispatch<GetWarmerDescriptor, GetWarmerQueryString, WarmerResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatch(p)
			);
		}

		public Task<IWarmerResponse> GetWarmerAsync(Func<GetWarmerDescriptor, GetWarmerDescriptor> selector)
		{
			return this.DispatchAsync<GetWarmerDescriptor, GetWarmerQueryString, WarmerResponse, IWarmerResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetWarmerDispatchAsync(p)
			);
		}
		
		public IIndicesOperationResponse DeleteWarmer(Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector)
		{
			return this.Dispatch<DeleteWarmerDescriptor, DeleteWarmerQueryString, IndicesOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatch(p)
			);
		}

		public Task<IIndicesOperationResponse> DeleteWarmerAsync(Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector)
		{
			return this.DispatchAsync<DeleteWarmerDescriptor, DeleteWarmerQueryString, IndicesOperationResponse, IIndicesOperationResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesDeleteWarmerDispatchAsync(p)
			);
		}
	}
}