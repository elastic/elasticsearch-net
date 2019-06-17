using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Performs the analysis process on a text and return the tokens breakdown of the text.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-analyze.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the analyze operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static AnalyzeResponse Analyze(this IElasticClient client, Func<AnalyzeDescriptor, IAnalyzeRequest> selector)
			=> client.Indices.Analyze(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static AnalyzeResponse Analyze(this IElasticClient client, IAnalyzeRequest request)
			=> client.Indices.Analyze(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<AnalyzeResponse> AnalyzeAsync(this IElasticClient client, Func<AnalyzeDescriptor, IAnalyzeRequest> selector,
			CancellationToken ct = default
		)
			=> client.Indices.AnalyzeAsync(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<AnalyzeResponse> AnalyzeAsync(this IElasticClient client, IAnalyzeRequest request, CancellationToken ct = default)
			=> client.Indices.AnalyzeAsync(request, ct);
	}
}
