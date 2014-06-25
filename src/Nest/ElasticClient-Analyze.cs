using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector)
		{
			return this.Dispatch<AnalyzeDescriptor, AnalyzeRequestParameters, AnalyzeResponse>(
				analyzeSelector,
				(p, d) =>
				{
					IRequest<AnalyzeRequestParameters> request = d;
					var text = request.RequestParameters.GetQueryStringValue<string>("text");
					request.RequestParameters.RemoveQueryString("text");
					text.ThrowIfNullOrEmpty("No text specified to analyze");
					return this.RawDispatch.IndicesAnalyzeDispatch<AnalyzeResponse>(p, text);
				}
			);
		}

		/// <inheritdoc />
		public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector)
		{
			return this.DispatchAsync<AnalyzeDescriptor, AnalyzeRequestParameters, AnalyzeResponse, IAnalyzeResponse>(
				analyzeSelector,
				(p, d) =>
				{
					IRequest<AnalyzeRequestParameters> request = d;
					var text = request.RequestParameters.GetQueryStringValue<string>("text");
					request.RequestParameters.RemoveQueryString("text");
					text.ThrowIfNullOrEmpty("No text specified to analyze");
					return this.RawDispatch.IndicesAnalyzeDispatchAsync<AnalyzeResponse>(p, text);
				}
			);
		}
	}
}