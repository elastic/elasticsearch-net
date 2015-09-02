using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IExplainResponse<T> Explain<T>(Func<ExplainDescriptor<T>, IExplainRequest> querySelector)
			where T : class;

		/// <inheritdoc/>
		IExplainResponse<T> Explain<T>(IExplainRequest explainRequest)
			where T : class;

		/// <inheritdoc/>
		Task<IExplainResponse<T>> ExplainAsync<T>(Func<ExplainDescriptor<T>, IExplainRequest> querySelector)
			where T : class;

		/// <inheritdoc/>
		Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest explainRequest)
			where T : class;

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExplainResponse<T> Explain<T>(Func<ExplainDescriptor<T>, IExplainRequest> querySelector)
			where T : class => 
			this.Dispatcher.Dispatch<IExplainRequest, ExplainRequestParameters, ExplainResponse<T>>(
				querySelector?.Invoke(new ExplainDescriptor<T>()),
				this.LowLevelDispatch.ExplainDispatch<ExplainResponse<T>>
			);

		/// <inheritdoc/>
		public IExplainResponse<T> Explain<T>(IExplainRequest explainRequest)
			where T : class => 
			this.Dispatcher.Dispatch<IExplainRequest, ExplainRequestParameters, ExplainResponse<T>>(
				explainRequest,
				this.LowLevelDispatch.ExplainDispatch<ExplainResponse<T>>
			);

		/// <inheritdoc/>
		public Task<IExplainResponse<T>> ExplainAsync<T>(Func<ExplainDescriptor<T>, IExplainRequest> querySelector)
			where T : class => 
			this.Dispatcher.DispatchAsync<IExplainRequest, ExplainRequestParameters, ExplainResponse<T>, IExplainResponse<T>>(
				querySelector?.Invoke(new ExplainDescriptor<T>()),
				this.LowLevelDispatch.ExplainDispatchAsync<ExplainResponse<T>>
			);

		/// <inheritdoc/>
		public Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest explainRequest)
			where T : class => 
			this.Dispatcher.DispatchAsync<IExplainRequest, ExplainRequestParameters, ExplainResponse<T>, IExplainResponse<T>>(
				explainRequest,
				this.LowLevelDispatch.ExplainDispatchAsync<ExplainResponse<T>>
			);
	}
}