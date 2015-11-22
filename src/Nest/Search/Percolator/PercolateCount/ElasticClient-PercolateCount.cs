using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPercolateCountResponse PercolateCount<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> percolateSelector)
			where T : class;

		/// <inheritdoc/>
		IPercolateCountResponse PercolateCount<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class;

		Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> percolateSelector = null)
			where T : class;

		/// <inheritdoc/>
		Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class;
	}

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IPercolateCountResponse PercolateCount<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> percolateSelector)
			where T : class =>
			this.PercolateCount<T>(percolateSelector?.Invoke(new PercolateCountDescriptor<T>(typeof(T), typeof(T))));

		/// <inheritdoc/>
		public IPercolateCountResponse PercolateCount<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class => 
			this.Dispatcher.Dispatch<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse>(
				percolateCountRequest,
				this.LowLevelDispatch.CountPercolateDispatch<PercolateCountResponse>
			);

		/// <inheritdoc/>
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> percolateSelector)
			where T : class => 
			this.PercolateCountAsync<T>(percolateSelector?.Invoke(new PercolateCountDescriptor<T>(typeof(T), typeof(T))));

		/// <inheritdoc/>
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class => 
			this.Dispatcher.DispatchAsync<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				percolateCountRequest,
				this.LowLevelDispatch.CountPercolateDispatchAsync<PercolateCountResponse>
			);
	}
}