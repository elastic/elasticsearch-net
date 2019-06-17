using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieving which patterns the grok processor is packaged with, useful as different versions are bundled with different
		/// processors.
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/plugins/master/ingest.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the endpoint usage operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GrokProcessorPatternsResponse GrokProcessorPatterns(this IElasticClient client,
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null
		)
			=> client.Ingest.GrokProcessorPatterns(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GrokProcessorPatternsResponse GrokProcessorPatterns(this IElasticClient client, IGrokProcessorPatternsRequest request)
			=> client.Ingest.GrokProcessorPatterns(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(this IElasticClient client,
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Ingest.GrokProcessorPatternsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(this IElasticClient client,
			IGrokProcessorPatternsRequest request,
			CancellationToken ct = default
		)
			=> client.Ingest.GrokProcessorPatternsAsync(request, ct);
	}
}
