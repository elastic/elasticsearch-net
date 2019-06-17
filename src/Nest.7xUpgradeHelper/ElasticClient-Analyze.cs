using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Analyze(), please update this usage.")]
		public static AnalyzeResponse Analyze(this IElasticClient client, Func<AnalyzeDescriptor, IAnalyzeRequest> selector)
			=> client.Indices.Analyze(selector);

		[Obsolete("Moved to client.Indices.Analyze(), please update this usage.")]
		public static AnalyzeResponse Analyze(this IElasticClient client, IAnalyzeRequest request)
			=> client.Indices.Analyze(request);

		[Obsolete("Moved to client.Indices.AnalyzeAsync(), please update this usage.")]
		public static Task<AnalyzeResponse> AnalyzeAsync(this IElasticClient client, Func<AnalyzeDescriptor, IAnalyzeRequest> selector,
			CancellationToken ct = default
		)
			=> client.Indices.AnalyzeAsync(selector, ct);

		[Obsolete("Moved to client.Indices.AnalyzeAsync(), please update this usage.")]
		public static Task<AnalyzeResponse> AnalyzeAsync(this IElasticClient client, IAnalyzeRequest request, CancellationToken ct = default)
			=> client.Indices.AnalyzeAsync(request, ct);
	}
}
