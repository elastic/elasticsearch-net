using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
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
			var converter = CreateCovariantMultiGetConverter(descriptor);
			return this.Dispatch<MultiGetDescriptor, MultiGetQueryString, MultiGetResponse>(
				descriptor,
				(p, d) => this.RawDispatch.MgetDispatch<MultiGetResponse>(p, d, converter)
			);
		}
		public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor(this._connectionSettings));
			var converter = CreateCovariantMultiGetConverter(descriptor);
			return this.DispatchAsync<MultiGetDescriptor, MultiGetQueryString, MultiGetResponse, IMultiGetResponse>(
				descriptor,
				(p, d) => this.RawDispatch.MgetDispatchAsync<MultiGetResponse>(p, d, converter)
			);
		}

		private JsonConverter CreateCovariantMultiGetConverter(MultiGetDescriptor descriptor)
		{
			var multiGetHitConverter = new MultiGetHitConverter(descriptor);
			return multiGetHitConverter;
		}

	}
}
