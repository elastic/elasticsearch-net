using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest_5_2_0
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		IPercolateCountResponse PercolateCount<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector = null)
			where T : class;

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		IPercolateCountResponse PercolateCount<T>(IPercolateCountRequest<T> request)
			where T : class;

		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;
	}

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		public IPercolateCountResponse PercolateCount<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector = null)
			where T : class =>
			this.PercolateCount<T>(selector?.Invoke(new PercolateCountDescriptor<T>(typeof(T), typeof(T))));

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		public IPercolateCountResponse PercolateCount<T>(IPercolateCountRequest<T> request)
			where T : class =>
			this.Dispatcher.Dispatch<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse>(
				request,
				this.LowLevelDispatch.CountPercolateDispatch<PercolateCountResponse>
			);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, IPercolateCountRequest<T>> selector = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.PercolateCountAsync<T>(selector?.Invoke(new PercolateCountDescriptor<T>(typeof(T), typeof(T))), cancellationToken);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
		public Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> request, CancellationToken cancellationToken = default(CancellationToken))
			where T : class =>
			this.Dispatcher.DispatchAsync<IPercolateCountRequest<T>, PercolateCountRequestParameters, PercolateCountResponse, IPercolateCountResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.CountPercolateDispatchAsync<PercolateCountResponse>
			);
	}
}
