using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	using Elasticsearch.Net.Serialization;
	using MultiSearchCreator = Func<IApiCallDetails, Stream, MultiSearchResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector)
		{
			return this.Dispatcher.Dispatch<MultiSearchDescriptor, MultiSearchRequestParameters, MultiSearchResponse>(
				multiSearchSelector(new MultiSearchDescriptor()),
				(p, d) =>
				{
					var converter = CreateMultiSearchConverter(d);
					var serializer = new NestSerializer(this.ConnectionSettings, converter);
					var json = serializer.SerializeToBytes(d).Utf8String();
					var creator = new MultiSearchCreator((r, s) =>  serializer.Deserialize<MultiSearchResponse>(s));
					return this.LowLevelDispatch.MsearchDispatch<MultiSearchResponse>(p.DeserializationState(creator), json);
				}
			);
		}

		/// <inheritdoc/>
		public IMultiSearchResponse MultiSearch(IMultiSearchRequest multiSearchRequest)
		{
			return this.Dispatcher.Dispatch<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse>(
				multiSearchRequest,
				(p, d) =>
				{
					var converter = CreateMultiSearchConverter(d);
					var serializer = new NestSerializer(this.ConnectionSettings, converter);
					var json = serializer.SerializeToBytes(d).Utf8String();
					var creator = new MultiSearchCreator((r, s) =>  serializer.Deserialize<MultiSearchResponse>(s));
					return this.LowLevelDispatch.MsearchDispatch<MultiSearchResponse>(p.DeserializationState(creator), json);
				}
			);
		}

		/// <inheritdoc/>
		public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector) {
			return this.Dispatcher.DispatchAsync<MultiSearchDescriptor, MultiSearchRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
				multiSearchSelector(new MultiSearchDescriptor()),
				(p, d) =>
				{
					var converter = CreateMultiSearchConverter(d);
					var serializer = new NestSerializer(this.ConnectionSettings, converter);
					var json = serializer.SerializeToBytes(d).Utf8String();
					var creator = new MultiSearchCreator((r, s) =>  serializer.Deserialize<MultiSearchResponse>(s));
					return this.LowLevelDispatch.MsearchDispatchAsync<MultiSearchResponse>(p.DeserializationState(creator), json);
				}
			);
		}

		/// <inheritdoc/>
		public Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest multiSearchRequest) 
		{
			return this.Dispatcher.DispatchAsync<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
				multiSearchRequest,
				(p, d) =>
				{
					var converter = CreateMultiSearchConverter(d);
					var serializer = new NestSerializer(this.ConnectionSettings, converter);
					var json = serializer.SerializeToBytes(d).Utf8String();
					var creator = new MultiSearchCreator((r, s) =>  serializer.Deserialize<MultiSearchResponse>(s));
					return this.LowLevelDispatch.MsearchDispatchAsync<MultiSearchResponse>(p.DeserializationState(creator), d);
				}
			);
		}

		private JsonConverter CreateMultiSearchConverter(IMultiSearchRequest descriptor)
		{
			if (descriptor.Operations != null)
			{
				foreach (var kv in descriptor.Operations)
					SearchPathInfo.CloseOverAutomagicCovariantResultSelector(this.Infer, kv.Value);				
			}

			var multiSearchConverter = new MultiSearchJsonConverter(ConnectionSettings, descriptor);
			return multiSearchConverter;
		}
	}
}