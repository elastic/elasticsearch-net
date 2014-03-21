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
			return this.Dispatch<AnalyzeDescriptor, AnalyzeQueryString, AnalyzeResponse>(
				analyzeSelector,
				(p, d) => this.RawDispatch.IndicesAnalyzeDispatch<AnalyzeResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector)
		{
			return this.DispatchAsync<AnalyzeDescriptor, AnalyzeQueryString, AnalyzeResponse, IAnalyzeResponse>(
				analyzeSelector,
				(p, d) => this.RawDispatch.IndicesAnalyzeDispatchAsync<AnalyzeResponse>(p, d)
			);
		}
	}
}