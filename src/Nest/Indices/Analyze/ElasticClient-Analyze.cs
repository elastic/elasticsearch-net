using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Performs the analysis process on a text and return the tokens breakdown of the text.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-analyze.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the analyze operation</param>
		IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> selector);

		/// <inheritdoc />
		IAnalyzeResponse Analyze(IAnalyzeRequest request);

		/// <inheritdoc />
		Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> selector) =>
			Analyze(selector?.Invoke(new AnalyzeDescriptor()));

		/// <inheritdoc />
		public IAnalyzeResponse Analyze(IAnalyzeRequest request) =>
			DoRequest<IAnalyzeRequest, AnalyzeResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IAnalyzeResponse> AnalyzeAsync(
			Func<AnalyzeDescriptor, IAnalyzeRequest> selector,
			CancellationToken ct = default
		) => AnalyzeAsync(selector?.Invoke(new AnalyzeDescriptor()), ct);

		/// <inheritdoc />
		public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IAnalyzeRequest, IAnalyzeResponse, AnalyzeResponse>(request, request.RequestParameters, ct);
	}
}
