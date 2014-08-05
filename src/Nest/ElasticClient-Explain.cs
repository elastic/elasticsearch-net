using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExplainResponse Explain<T>(Func<ExplainDescriptor<T>, ExplainDescriptor<T>> querySelector)
			where T : class
		{
			return this.Dispatch<ExplainDescriptor<T>, ExplainRequestParameters, ExplainResponse>(
				querySelector,
				(p, d) => this.RawDispatch.ExplainDispatch<ExplainResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IExplainResponse Explain(IExplainRequest explainRequest)
		{
			return this.Dispatch<IExplainRequest, ExplainRequestParameters, ExplainResponse>(
				explainRequest,
				(p, d) => this.RawDispatch.ExplainDispatch<ExplainResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IExplainResponse> ExplainAsync<T>(Func<ExplainDescriptor<T>, ExplainDescriptor<T>> querySelector)
			where T : class
		{
			return this.DispatchAsync<ExplainDescriptor<T>, ExplainRequestParameters, ExplainResponse, IExplainResponse>(
				querySelector,
				(p, d) => this.RawDispatch.ExplainDispatchAsync<ExplainResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IExplainResponse> ExplainAsync(IExplainRequest explainRequest)
		{
			return this.DispatchAsync<IExplainRequest, ExplainRequestParameters, ExplainResponse, IExplainResponse>(
				explainRequest,
				(p, d) => this.RawDispatch.ExplainDispatchAsync<ExplainResponse>(p, d)
			);
		}
	}
}