using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	using MultiSearchCreator = Func<IApiCallDetails, Stream, MultiSearchResponse>;

	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The multi search API allows to execute several search requests within the same API.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the search operations on the multi search api</param>
		public static MultiSearchResponse MultiSearch(this IElasticClient client,Func<MultiSearchDescriptor, IMultiSearchRequest> selector);

		/// <inheritdoc />
		public static MultiSearchResponse MultiSearch(this IElasticClient client,IMultiSearchRequest request);

		/// <inheritdoc />
		public static Task<MultiSearchResponse> MultiSearchAsync(this IElasticClient client,Func<MultiSearchDescriptor, IMultiSearchRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<MultiSearchResponse> MultiSearchAsync(this IElasticClient client,IMultiSearchRequest request, CancellationToken ct = default);
	}

}
