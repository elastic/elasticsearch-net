using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static AnalyzeResponse Analyze(this IElasticClient client,Func<AnalyzeDescriptor, IAnalyzeRequest> selector);

		/// <inheritdoc />
		public static AnalyzeResponse Analyze(this IElasticClient client,IAnalyzeRequest request);

		/// <inheritdoc />
		public static Task<AnalyzeResponse> AnalyzeAsync(this IElasticClient client,Func<AnalyzeDescriptor, IAnalyzeRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<AnalyzeResponse> AnalyzeAsync(this IElasticClient client,IAnalyzeRequest request, CancellationToken ct = default);
	}

}
