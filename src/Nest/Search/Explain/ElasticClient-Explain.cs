using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IExplainResponse<T> Explain<T>(Func<ExplainDescriptor<T>, ExplainDescriptor<T>> querySelector)
			where T : class
		{
			return this.Dispatcher.Dispatch<ExplainDescriptor<T>, ExplainRequestParameters, ExplainResponse<T>>(
				querySelector,
				(p, d) => this.RawDispatch.ExplainDispatch<ExplainResponse<T>>(p, d)
			);
		}

		/// <inheritdoc />
		public IExplainResponse<T> Explain<T>(IExplainRequest explainRequest)
			where T : class
		{
			return this.Dispatcher.Dispatch<IExplainRequest, ExplainRequestParameters, ExplainResponse<T>>(
				explainRequest,
				(p, d) => this.RawDispatch.ExplainDispatch<ExplainResponse<T>>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IExplainResponse<T>> ExplainAsync<T>(Func<ExplainDescriptor<T>, ExplainDescriptor<T>> querySelector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<ExplainDescriptor<T>, ExplainRequestParameters, ExplainResponse<T>, IExplainResponse<T>>(
				querySelector,
				(p, d) => this.RawDispatch.ExplainDispatchAsync<ExplainResponse<T>>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest explainRequest)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<IExplainRequest, ExplainRequestParameters, ExplainResponse<T>, IExplainResponse<T>>(
				explainRequest,
				(p, d) => this.RawDispatch.ExplainDispatchAsync<ExplainResponse<T>>(p, d)
			);
		}
	}
}