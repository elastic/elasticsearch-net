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
					return this.RawDispatch.IndicesAnalyzeDispatch<AnalyzeResponse>(p, text);
				}
			);
		}

		/// <inheritdoc />
		public IAnalyzeResponse Analyze(IAnalyzeRequest analyzeRequest)
		{
			return this.Dispatch<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse>(
				analyzeRequest,
				(p, d) =>
				{
					IRequest<AnalyzeRequestParameters> request = d;
					var text = request.RequestParameters.GetQueryStringValue<string>("text");
					request.RequestParameters.RemoveQueryString("text");
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
					return this.RawDispatch.IndicesAnalyzeDispatchAsync<AnalyzeResponse>(p, text);
				}
			);
		}

		/// <inheritdoc />
		public Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest analyzeRequest)
		{
			return this.DispatchAsync<IAnalyzeRequest, AnalyzeRequestParameters, AnalyzeResponse, IAnalyzeResponse>(
				analyzeRequest,
				(p, d) =>
				{
					IRequest<AnalyzeRequestParameters> request = d;
					var text = request.RequestParameters.GetQueryStringValue<string>("text");
					request.RequestParameters.RemoveQueryString("text");
					return this.RawDispatch.IndicesAnalyzeDispatchAsync<AnalyzeResponse>(p, text);
				}
			);
		}

	}
}