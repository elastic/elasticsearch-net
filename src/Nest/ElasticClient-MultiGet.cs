using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{

	public partial class ElasticClient
	{

		public IMultiGetResponse MultiGet(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor(this._connectionSettings));
			return this.Dispatch<MultiGetDescriptor, MultiGetQueryString, MultiGetResponse>(
				descriptor,
				(p, d) => this.RawDispatch.MgetDispatch(p, d),
				this.Serializer.DeserializeMultiGetResponse
			);
		}
		public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor(this._connectionSettings));
			return this.DispatchAsync<MultiGetDescriptor, MultiGetQueryString, MultiGetResponse, IMultiGetResponse>(
				descriptor,
				(p, d) => this.RawDispatch.MgetDispatchAsync(p, d),
				this.Serializer.DeserializeMultiGetResponse
			);
		}

	}
}
