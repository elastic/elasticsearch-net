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
		/// <param name="analyzeSelector">A descriptor that describes the analyze operation</param>
		IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> analyzeSelector);

		/// <inheritdoc/>
		IAnalyzeResponse Analyze(IAnalyzeRequest analyzeRequest);

		/// <inheritdoc/>
		Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> analyzeSelector);

		/// <inheritdoc/>
		Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest analyzeRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, IAnalyzeRequest> analyzeSelector) =>
			this.Analyze(analyzeSelector?.Invoke(new AnalyzeDescriptor()));

		/// <inheritdoc/>
		public IAnalyzeResponse Analyze(IAnalyzeRequest analyzeRequest) => 
			this.Dispatcher.Dispatch<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse>(
				analyzeRequest,
				(p, d) => this.LowLevelDispatch.IndicesAnalyzeDispatch<AnalyzeResponse>(p, MoveTextFromQueryString(analyzeRequest))
			);

		/// <inheritdoc/>
		public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, IAnalyzeRequest> analyzeSelector) =>
			this.AnalyzeAsync(analyzeSelector?.Invoke(new AnalyzeDescriptor()));

		/// <inheritdoc/>
		public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest analyzeRequest) => 
			this.Dispatcher.DispatchAsync<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse, IAnalyzeResponse>(
				analyzeRequest,
				(p, d) => this.LowLevelDispatch.IndicesAnalyzeDispatchAsync<AnalyzeResponse>(p, MoveTextFromQueryString(analyzeRequest))
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