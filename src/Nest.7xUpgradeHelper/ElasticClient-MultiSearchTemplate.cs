using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	using MultiSearchTemplateCreator = Func<IApiCallDetails, Stream, MultiSearchResponse>;

	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The multi search API allows to execute several search requests within the same API.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the search operations on the multi search api</param>
		public static MultiSearchResponse MultiSearchTemplate(this IElasticClient client,Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector);

		/// <inheritdoc />
		public static MultiSearchResponse MultiSearchTemplate(this IElasticClient client,IMultiSearchTemplateRequest request);

		/// <inheritdoc />
		public static Task<MultiSearchResponse> MultiSearchTemplateAsync(this IElasticClient client,Func<MultiSearchTemplateDescriptor, IMultiSearchTemplateRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<MultiSearchResponse> MultiSearchTemplateAsync(this IElasticClient client,IMultiSearchTemplateRequest request,
			CancellationToken ct = default
		);
	}

}
