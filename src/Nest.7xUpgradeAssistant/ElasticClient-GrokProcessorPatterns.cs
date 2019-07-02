using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Ingest.GrokProcessorPatterns(), please update this usage.")]
		public static GrokProcessorPatternsResponse GrokProcessorPatterns(this IElasticClient client,
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null
		)
			=> client.Ingest.GrokProcessorPatterns(selector);

		[Obsolete("Moved to client.Ingest.GrokProcessorPatterns(), please update this usage.")]
		public static GrokProcessorPatternsResponse GrokProcessorPatterns(this IElasticClient client, IGrokProcessorPatternsRequest request)
			=> client.Ingest.GrokProcessorPatterns(request);

		[Obsolete("Moved to client.Ingest.GrokProcessorPatternsAsync(), please update this usage.")]
		public static Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(this IElasticClient client,
			Func<GrokProcessorPatternsDescriptor, IGrokProcessorPatternsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Ingest.GrokProcessorPatternsAsync(selector, ct);

		[Obsolete("Moved to client.Ingest.GrokProcessorPatternsAsync(), please update this usage.")]
		public static Task<GrokProcessorPatternsResponse> GrokProcessorPatternsAsync(this IElasticClient client,
			IGrokProcessorPatternsRequest request,
			CancellationToken ct = default
		)
			=> client.Ingest.GrokProcessorPatternsAsync(request, ct);
	}
}
