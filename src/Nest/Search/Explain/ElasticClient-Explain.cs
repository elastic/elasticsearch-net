using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IExplainResponse<T> Explain<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> querySelector)
			where T : class;

		/// <inheritdoc/>
		IExplainResponse<T> Explain<T>(IExplainRequest<T> explainRequest)
			where T : class;

		/// <inheritdoc/>
		Task<IExplainResponse<T>> ExplainAsync<T>(DocumentPath<T> document,Func<ExplainDescriptor<T>, IExplainRequest<T>> querySelector)
			where T : class;

		/// <inheritdoc/>
		Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest<T> explainRequest)
			where T : class;

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExplainResponse<T> Explain<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> querySelector)
			where T : class =>
			this.Explain<T>(querySelector?.Invoke(new ExplainDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public IExplainResponse<T> Explain<T>(IExplainRequest<T> explainRequest)
			where T : class => 
			this.Dispatcher.Dispatch<IExplainRequest<T>, ExplainRequestParameters, ExplainResponse<T>>(
				explainRequest,
				this.LowLevelDispatch.ExplainDispatch<ExplainResponse<T>>
			);

		/// <inheritdoc/>
		public Task<IExplainResponse<T>> ExplainAsync<T>(DocumentPath<T> document, Func<ExplainDescriptor<T>, IExplainRequest<T>> querySelector)
			where T : class => 
			this.ExplainAsync<T>(querySelector?.Invoke(new ExplainDescriptor<T>(document.Self.Index, document.Self.Type, document.Self.Id)));

		/// <inheritdoc/>
		public Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest<T> explainRequest)
			where T : class => 
			this.Dispatcher.DispatchAsync<IExplainRequest<T>, ExplainRequestParameters, ExplainResponse<T>, IExplainResponse<T>>(
				explainRequest,
				this.LowLevelDispatch.ExplainDispatchAsync<ExplainResponse<T>>
			);
	}
}