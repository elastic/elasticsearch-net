using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPercolateCountResponse PercolateCount<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector = null)
			where T : class;

		/// <inheritdoc/>
		IPercolateCountResponse PercolateCount<T>(IPercolateCountRequest<T> request)
			where T : class;

		Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector = null)
			where T : class;

		/// <inheritdoc/>
		Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> request)
			where T : class;
	}

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IPercolateCountResponse PercolateCount<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector = null)
			where T : class =>
			this.PercolateCount<T>(selector?.Invoke(new PercolateCountDescriptor<T>(typeof(T), typeof(T))));

		/// <inheritdoc/>
		public IPercolateCountResponse PercolateCount<T>(IPercolateCountRequest<T> request)
			where T : class => 
			this.Dispatcher.Dispatch<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse>(
				request,
				this.LowLevelDispatch.CountPercolateDispatch<PercolateCountResponse>
			);

		/// <inheritdoc/>
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector = null)
			where T : class => 
			this.PercolateCountAsync<T>(selector?.Invoke(new PercolateCountDescriptor<T>(typeof(T), typeof(T))));

		/// <inheritdoc/>
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> request)
			where T : class => 
			this.Dispatcher.DispatchAsync<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				request,
				this.LowLevelDispatch.CountPercolateDispatchAsync<PercolateCountResponse>
			);
	}
}