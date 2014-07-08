using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	using MultiGetConverter = Func<IElasticsearchResponse, Stream, MultiGetResponse>;
	
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiGetResponse MultiGet(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor());
			var converter = CreateCovariantMultiGetConverter(descriptor);
			var customCreator = new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, converter));
			return this.Dispatch<MultiGetDescriptor, MultiGetRequestParameters, MultiGetResponse>(
				descriptor,
				(p, d) => this.RawDispatch.MgetDispatch<MultiGetResponse>(p.DeserializationState(customCreator), d)
			);
		}

		/// <inheritdoc />
		public IMultiGetResponse MultiGet(IMultiGetRequest multiRequest)
		{
			var converter = CreateCovariantMultiGetConverter(multiRequest);
			var customCreator = new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, converter));
			return this.Dispatch<IMultiGetRequest, MultiGetRequestParameters, MultiGetResponse>(
				multiRequest,
				(p, d) => this.RawDispatch.MgetDispatch<MultiGetResponse>(p.DeserializationState(customCreator), d)
			);
		}

		/// <inheritdoc />
		public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor());
			var converter = CreateCovariantMultiGetConverter(descriptor);
			var customCreator = new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, converter));
			return this.DispatchAsync<MultiGetDescriptor, MultiGetRequestParameters, MultiGetResponse, IMultiGetResponse>(
				descriptor,
				(p, d) => this.RawDispatch.MgetDispatchAsync<MultiGetResponse>(p.DeserializationState(customCreator), d)
			);
		}

		/// <inheritdoc />
		public Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest multiGetRequest)
		{
			var converter = CreateCovariantMultiGetConverter(multiGetRequest);
			var customCreator = new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, converter));
			return this.DispatchAsync<IMultiGetRequest, MultiGetRequestParameters, MultiGetResponse, IMultiGetResponse>(
				multiGetRequest,
				(p, d) => this.RawDispatch.MgetDispatchAsync<MultiGetResponse>(p.DeserializationState(customCreator), d)
			);
		}



		private MultiGetResponse DeserializeMultiGetResponse(IElasticsearchResponse response, Stream stream, JsonConverter converter)
		{
			return this.Serializer.DeserializeInternal<MultiGetResponse>(stream, converter);
		}

		private JsonConverter CreateCovariantMultiGetConverter(IMultiGetRequest descriptor)
		{
			var multiGetHitConverter = new MultiGetHitConverter(descriptor);
			return multiGetHitConverter;
		}
	}
}