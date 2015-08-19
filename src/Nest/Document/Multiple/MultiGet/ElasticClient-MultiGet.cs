using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	using MultiGetConverter = Func<IApiCallDetails, Stream, MultiGetResponse>;
	
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiGetResponse MultiGet(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor());
			var converter = CreateCovariantMultiGetConverter(descriptor);
			var customCreator = new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, converter));
			return this.Dispatcher.Dispatch<MultiGetDescriptor, MultiGetRequestParameters, MultiGetResponse>(
				descriptor,
				(p, d) => this.LowLevelDispatch.MgetDispatch<MultiGetResponse>(p.DeserializationState(customCreator), d)
			);
		}

		/// <inheritdoc />
		public IMultiGetResponse MultiGet(IMultiGetRequest multiRequest)
		{
			var converter = CreateCovariantMultiGetConverter(multiRequest);
			var customCreator = new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, converter));
			return this.Dispatcher.Dispatch<IMultiGetRequest, MultiGetRequestParameters, MultiGetResponse>(
				multiRequest,
				(p, d) => this.LowLevelDispatch.MgetDispatch<MultiGetResponse>(p.DeserializationState(customCreator), d)
			);
		}

		/// <inheritdoc />
		public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor());
			var converter = CreateCovariantMultiGetConverter(descriptor);
			var customCreator = new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, converter));
			return this.Dispatcher.DispatchAsync<MultiGetDescriptor, MultiGetRequestParameters, MultiGetResponse, IMultiGetResponse>(
				descriptor,
				(p, d) => this.LowLevelDispatch.MgetDispatchAsync<MultiGetResponse>(p.DeserializationState(customCreator), d)
			);
		}

		/// <inheritdoc />
		public Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest multiGetRequest)
		{
			var converter = CreateCovariantMultiGetConverter(multiGetRequest);
			var customCreator = new MultiGetConverter((r, s) => this.DeserializeMultiGetResponse(r, s, converter));
			return this.Dispatcher.DispatchAsync<IMultiGetRequest, MultiGetRequestParameters, MultiGetResponse, IMultiGetResponse>(
				multiGetRequest,
				(p, d) => this.LowLevelDispatch.MgetDispatchAsync<MultiGetResponse>(p.DeserializationState(customCreator), d)
			);
		}

		private MultiGetResponse DeserializeMultiGetResponse(IApiCallDetails response, Stream stream, JsonConverter converter)=>
			new NestSerializer(this.ConnectionSettings, converter).Deserialize<MultiGetResponse>(stream);

		private JsonConverter CreateCovariantMultiGetConverter(IMultiGetRequest descriptor) => new MultiGetHitJsonConverter(descriptor);

	}
}