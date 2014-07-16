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
	using MultiSearchCreator = Func<IElasticsearchResponse, Stream, MultiSearchResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector)
		{
			return this.Dispatch<MultiSearchDescriptor, MultiSearchRequestParameters, MultiSearchResponse>(
				multiSearchSelector(new MultiSearchDescriptor()),
				(p, d) =>
				{
					var converter = CreateMultiSearchConverter(d);
					var json = Serializer.SerializeMultiSearch(d);
					var creator = new MultiSearchCreator((r, s) => this.DeserializeMultiSearchHit(r, s, converter));
					return this.RawDispatch.MsearchDispatch<MultiSearchResponse>(p.DeserializationState(creator), json);
				}
			);
		}

		/// <inheritdoc />
		public IMultiSearchResponse MultiSearch(IMultiSearchRequest multiSearchRequest)
		{
			return this.Dispatch<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse>(
				multiSearchRequest,
				(p, d) =>
				{
					var converter = CreateMultiSearchConverter(d);
					var json = Serializer.SerializeMultiSearch(d);
					var creator = new MultiSearchCreator((r, s) => this.DeserializeMultiSearchHit(r, s, converter));
					return this.RawDispatch.MsearchDispatch<MultiSearchResponse>(p.DeserializationState(creator), json);
				}
			);
		}

		/// <inheritdoc />
		public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector) {
			return this.DispatchAsync<MultiSearchDescriptor, MultiSearchRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
				multiSearchSelector(new MultiSearchDescriptor()),
				(p, d) =>
				{
					var converter = CreateMultiSearchConverter(d);
					var json = Serializer.SerializeMultiSearch(d);
					var creator = new MultiSearchCreator((r, s) => this.DeserializeMultiSearchHit(r, s, converter));
					return this.RawDispatch.MsearchDispatchAsync<MultiSearchResponse>(p.DeserializationState(creator), json);
				}
			);
		}

		/// <inheritdoc />
		public Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest multiSearchRequest) 
		{
			return this.DispatchAsync<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
				multiSearchRequest,
				(p, d) =>
				{
					var converter = CreateMultiSearchConverter(d);
					var json = Serializer.SerializeMultiSearch(d);
					var creator = new MultiSearchCreator((r, s) => this.DeserializeMultiSearchHit(r, s, converter));
					return this.RawDispatch.MsearchDispatchAsync<MultiSearchResponse>(p.DeserializationState(creator), json);
				}
			);
		}



		private MultiSearchResponse DeserializeMultiSearchHit(IElasticsearchResponse response, Stream stream, JsonConverter converter)
		{
			return this.Serializer.DeserializeInternal<MultiSearchResponse>(stream, converter);
		}

		private JsonConverter CreateMultiSearchConverter(IMultiSearchRequest descriptor)
		{
			if (descriptor.Operations != null)
			{
				foreach (var kv in descriptor.Operations)
					SearchPathInfo.CloseOverAutomagicCovariantResultSelector(this.Infer, kv.Value);				
			}


			var multiSearchConverter = new MultiSearchConverter(_connectionSettings, descriptor);
			return multiSearchConverter;
		}
	}
}