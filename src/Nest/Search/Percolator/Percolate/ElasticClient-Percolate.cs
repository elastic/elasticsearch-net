using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Percolate a document
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-percolate.html
		/// </summary>
		/// <typeparam name="T">The type to infer the index/type from, and of the object that is being percolated</typeparam>
		/// <param name="selector">An optional descriptor describing the percolate operation further</param>
		IPercolateResponse Percolate<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector)
			where T : class;

		/// <inheritdoc/>
		IPercolateResponse Percolate<T>(IPercolateRequest<T> request)
			where T : class;

		/// <inheritdoc/>
		Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector)
			where T : class;

		/// <inheritdoc/>
		Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> request)
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPercolateResponse Percolate<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector)
			where T : class =>
			this.Percolate(selector?.Invoke(new PercolateDescriptor<T>(typeof(T), typeof(T))));

		/// <inheritdoc/>
		public IPercolateResponse Percolate<T>(IPercolateRequest<T> request)
			where T : class => 
			this.Dispatcher.Dispatch<IPercolateRequest<T>, PercolateRequestParameters, PercolateResponse>(
				request,
				this.LowLevelDispatch.PercolateDispatch<PercolateResponse>
			);

		/// <inheritdoc/>
		public Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector)
			where T : class => 
			this.PercolateAsync(selector?.Invoke(new PercolateDescriptor<T>(typeof(T), typeof(T))));

		/// <inheritdoc/>
		public Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> request)
			where T : class => 
			this.Dispatcher.DispatchAsync<IPercolateRequest<T>, PercolateRequestParameters, PercolateResponse, IPercolateResponse>(
				request,
				this.LowLevelDispatch.PercolateDispatchAsync<PercolateResponse>
			);
	}
}