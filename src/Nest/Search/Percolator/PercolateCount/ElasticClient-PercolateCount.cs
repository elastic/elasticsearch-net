using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IPercolateCountResponse PercolateCount<T>(Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector)
			where T : class
		{
			return this.Dispatcher.Dispatch<PercolateCountDescriptor<T>, PercolateCountRequestParameters, PercolateCountResponse>(
				percolateSelector,
				(p, d) => this.LowLevelDispatch.CountPercolateDispatch<PercolateCountResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public IPercolateCountResponse PercolateCount<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class
		{
			return this.Dispatcher.Dispatch<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse>(
				percolateCountRequest,
				(p, d) => this.LowLevelDispatch.CountPercolateDispatch<PercolateCountResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<PercolateCountDescriptor<T>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				percolateSelector,
				(p, d) => this.LowLevelDispatch.CountPercolateDispatchAsync<PercolateCountResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				percolateCountRequest,
				(p, d) => this.LowLevelDispatch.CountPercolateDispatchAsync<PercolateCountResponse>(p, d)
			);
		}

	}
}