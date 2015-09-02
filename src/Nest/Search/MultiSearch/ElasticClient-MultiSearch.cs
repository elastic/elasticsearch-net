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
	public interface IElasticCLient
	{
		/// <summary>
		/// The multi search API allows to execute several search requests within the same API.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html
		/// </summary>
		/// <param name="multiSearchSelector">A descriptor that describes the search operations on the multi search api</param>
		IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, IMultiSearchRequest> multiSearchSelector);

		/// <inheritdoc/>
		IMultiSearchResponse MultiSearch(IMultiSearchRequest multiSearchRequest);

		/// <inheritdoc/>
		Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> multiSearchSelector);

		/// <inheritdoc/>
		Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest multiSearchRequest);
	}

	//TODO the custom serialize and deserialize here is ugly
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, IMultiSearchRequest> multiSearchSelector)
		{
			return this.Dispatcher.Dispatch<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse>(
				multiSearchSelector?.Invoke(new MultiSearchDescriptor()),
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
		public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> multiSearchSelector) {
			return this.Dispatcher.DispatchAsync<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
				multiSearchSelector?.Invoke(new MultiSearchDescriptor()),
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