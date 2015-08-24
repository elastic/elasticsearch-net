using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IPercolateResponse Percolate<T>(Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector)
			where T : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.Dispatcher.Dispatch<PercolateDescriptor<T>, PercolateRequestParameters, PercolateResponse>(
				percolateSelector,
				(p, d) => this.LowLevelDispatch.PercolateDispatch<PercolateResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public IPercolateResponse Percolate<T>(IPercolateRequest<T> percolateRequest)
			where T : class
		{
			return this.Dispatcher.Dispatch<IPercolateRequest<T>, PercolateRequestParameters, PercolateResponse>(
				percolateRequest,
				(p, d) => this.LowLevelDispatch.PercolateDispatch<PercolateResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector)
			where T : class
		{
			percolateSelector = percolateSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<PercolateDescriptor<T>, PercolateRequestParameters, PercolateResponse, IPercolateResponse>(
				percolateSelector,
				(p, d) => this.LowLevelDispatch.PercolateDispatchAsync<PercolateResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> percolateRequest)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<IPercolateRequest<T>, PercolateRequestParameters, PercolateResponse, IPercolateResponse>(
				percolateRequest,
				(p, d) => this.LowLevelDispatch.PercolateDispatchAsync<PercolateResponse>(p, d)
			);
		}

	}
}