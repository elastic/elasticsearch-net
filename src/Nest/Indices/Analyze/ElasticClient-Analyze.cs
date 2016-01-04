using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Performs the analysis process on a text and return the tokens breakdown of the text.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-analyze.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the analyze operation</param>
		IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> selector);

		/// <inheritdoc/>
		IAnalyzeResponse Analyze(IAnalyzeRequest request);

		/// <inheritdoc/>
		Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector);

		/// <inheritdoc/>
		Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> selector) =>
			this.Analyze(selector?.Invoke(new AnalyzeDescriptor()));

		/// <inheritdoc/>
		public IAnalyzeResponse Analyze(IAnalyzeRequest request) => 
			this.Dispatcher.Dispatch<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesAnalyzeDispatch<AnalyzeResponse>(p, MoveTextFromQueryString(request))
			);

		/// <inheritdoc/>
		public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> selector) =>
			this.AnalyzeAsync(selector?.Invoke(new AnalyzeDescriptor()));

		/// <inheritdoc/>
		public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest request) => 
			this.Dispatcher.DispatchAsync<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse, IAnalyzeResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesAnalyzeDispatchAsync<AnalyzeResponse>(p, MoveTextFromQueryString(request))
			);

		private static string MoveTextFromQueryString(IAnalyzeRequest d)
		{
			IRequest<AnalyzeRequestParameters> request = d;
			var text = request.RequestParameters.GetQueryStringValue<string[]>("text");
			request.RequestParameters.RemoveQueryString("text");
			return string.Join(",", text);
		}
	}
}