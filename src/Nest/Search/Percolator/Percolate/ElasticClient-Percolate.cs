using System;
using System.Threading.Tasks;
using Elasticsearch.Net_5_2_0;
using System.Threading;

namespace Nest_5_2_0
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Percolate a document
		/// </summary>
		/// <typeparam name="T">The type to infer the index/type from, and of the object that is being percolated</typeparam>
		/// <param name="selector">An optional descriptor describing the percolate operation further</param>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		IPercolateResponse Percolate<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector)
			where T : class;

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		IPercolateResponse Percolate<T>(IPercolateRequest<T> request)
			where T : class;

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		public IPercolateResponse Percolate<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector)
			where T : class =>
			this.Percolate(selector?.Invoke(new PercolateDescriptor<T>(typeof(T), typeof(T))));

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		public IPercolateResponse Percolate<T>(IPercolateRequest<T> request)
			where T : class =>
			this.Dispatcher.Dispatch<IPercolateRequest<T>, PercolateRequestParameters, PercolateResponse>(
				request,
				this.LowLevelDispatch.PercolateDispatch<PercolateResponse>
			);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		public Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, IPercolateRequest<T>> selector, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.PercolateAsync(selector?.Invoke(new PercolateDescriptor<T>(typeof(T), typeof(T))), cancellationToken);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		public Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.Dispatcher.DispatchAsync<IPercolateRequest<T>, PercolateRequestParameters, PercolateResponse, IPercolateResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.PercolateDispatchAsync<PercolateResponse>
			);
	}
}
