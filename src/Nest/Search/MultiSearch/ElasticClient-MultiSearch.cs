using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	using MultiSearchCreator = Func<IApiCallDetails, Stream, MultiSearchResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The multi search API allows to execute several search requests within the same API.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the search operations on the multi search api</param>
		MultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, IMultiSearchRequest> selector);

		/// <inheritdoc />
		MultiSearchResponse MultiSearch(IMultiSearchRequest request);

		/// <inheritdoc />
		Task<MultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<MultiSearchResponse> MultiSearchAsync(IMultiSearchRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public MultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, IMultiSearchRequest> selector) =>
			MultiSearch(selector?.Invoke(new MultiSearchDescriptor()));

		/// <inheritdoc />
		public MultiSearchResponse MultiSearch(IMultiSearchRequest request)
		{
			CreateMultiSearchConverter(request);
			return DoRequest<IMultiSearchRequest, MultiSearchResponse>(request, request.RequestParameters);
		}

		/// <inheritdoc />
		public Task<MultiSearchResponse> MultiSearchAsync(
			Func<MultiSearchDescriptor, IMultiSearchRequest> selector,
			CancellationToken ct = default
		) => MultiSearchAsync(selector?.Invoke(new MultiSearchDescriptor()), ct);


		/// <inheritdoc />
		public Task<MultiSearchResponse> MultiSearchAsync(IMultiSearchRequest request, CancellationToken ct = default)
		{
			CreateMultiSearchConverter(request);
			return DoRequestAsync<IMultiSearchRequest, MultiSearchResponse, MultiSearchResponse>(request, request.RequestParameters, ct);
		}

		private void CreateMultiSearchConverter(IMultiSearchRequest request)
		{
			var formatter = new MultiSearchResponseFormatter(request);
			var serializer = ConnectionSettings.CreateStateful(formatter);
			var creator = new MultiSearchCreator((r, s) => serializer.Deserialize<MultiSearchResponse>(s));
			request.RequestParameters.DeserializationOverride = creator;
		}
	}
}
