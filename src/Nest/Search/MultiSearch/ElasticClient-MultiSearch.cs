using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	using MultiSearchCreator = Func<IApiCallDetails, Stream, MultiSearchResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The multi search API allows to execute several search requests within the same API.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the search operations on the multi search api</param>
		IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, IMultiSearchRequest> selector);

		/// <inheritdoc/>
		IMultiSearchResponse MultiSearch(IMultiSearchRequest request);

		/// <inheritdoc/>
		Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> selector);

		/// <inheritdoc/>
		Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, IMultiSearchRequest> selector) =>
			this.MultiSearch(selector?.Invoke(new MultiSearchDescriptor()));

		/// <inheritdoc />
		public IMultiSearchResponse MultiSearch(IMultiSearchRequest request)
		{
			return this.Dispatcher.Dispatch<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse>(
				request,
				(p, d) =>
				{
					var converter = CreateMultiSearchDeserializer(p);
					var serializer = new JsonNetSerializer(this.ConnectionSettings, converter);
					var json = serializer.SerializeToBytes(p).Utf8String();
					var creator = new MultiSearchCreator((r, s) => serializer.Deserialize<MultiSearchResponse>(s));
					request.RequestParameters.DeserializationOverride(creator);
					return this.LowLevelDispatch.MsearchDispatch<MultiSearchResponse>(p, json);
				}
			);
		}

		/// <inheritdoc />
		public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> selector) =>
			this.MultiSearchAsync(selector?.Invoke(new MultiSearchDescriptor()));


		/// <inheritdoc />
		public Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest request)
		{
			return this.Dispatcher.DispatchAsync<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
				request,
				(p, d) =>
				{
					var converter = CreateMultiSearchDeserializer(p);
					var serializer = new JsonNetSerializer(this.ConnectionSettings, converter);
					var json = serializer.SerializeToBytes(p).Utf8String();
					var creator = new MultiSearchCreator((r, s) => serializer.Deserialize<MultiSearchResponse>(s));
					request.RequestParameters.DeserializationOverride(creator);
					return this.LowLevelDispatch.MsearchDispatchAsync<MultiSearchResponse>(p, json);
				}
			);
		}

		private JsonConverter CreateMultiSearchDeserializer(IMultiSearchRequest request)
		{
			if (request.Operations != null)
			{
				foreach (var operation in request.Operations.Values)
					CovariantSearch.CloseOverAutomagicCovariantResultSelector(this.Infer, operation);
			}

			return new MultiSearchResponseJsonConverter(this.ConnectionSettings, request);
		}
	}
}