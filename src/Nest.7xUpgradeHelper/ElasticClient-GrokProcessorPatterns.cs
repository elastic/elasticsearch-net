using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieving which patterns the grok processor is packaged with, useful as different versions are bundled with different processors.
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the endpoint usage operation</param>
		public static GrokProcessorPatternsResponse GrokProcessorPatterns(this IElasticClient client,Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null);

		/// <inheritdoc />
		public static GrokProcessorPatternsResponse GrokProcessorPatterns(this IElasticClient client,IGrokProcessorPatternsRequest request);

		/// <inheritdoc />
		public static Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(this IElasticClient client,
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(this IElasticClient client,IGrokProcessorPatternsRequest request,
			CancellationToken ct = default
		);
	}

}
