using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Unregister a percolator
		/// </summary>
		/// <param name="name">The name for the percolator</param>
		/// <param name="selector">An optional descriptor describing the unregister percolator operation further</param>
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		IUnregisterPercolatorResponse UnregisterPercolator<T>(Name name,
			Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null
		)
			where T : class;

		/// <inheritdoc />
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		IUnregisterPercolatorResponse UnregisterPercolator(IUnregisterPercolatorRequest request);

		/// <inheritdoc />
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync<T>(Name name,
			Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class;

		/// <inheritdoc />
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		public IUnregisterPercolatorResponse UnregisterPercolator<T>(Name name,
			Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null
		)
			where T : class =>
			UnregisterPercolator(selector.InvokeOrDefault(new UnregisterPercolatorDescriptor<T>(name)));

		/// <inheritdoc />
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		public IUnregisterPercolatorResponse UnregisterPercolator(IUnregisterPercolatorRequest request) =>
			Dispatcher.Dispatch<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolatorResponse>(
				request,
				(p, d) => LowLevelDispatch.DeleteDispatch<UnregisterPercolatorResponse>(p)
			);

		/// <inheritdoc />
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync<T>(Name name,
			Func<UnregisterPercolatorDescriptor<T>, IUnregisterPercolatorRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class =>
			UnregisterPercolatorAsync(selector.InvokeOrDefault(new UnregisterPercolatorDescriptor<T>(name)), cancellationToken);

		/// <inheritdoc />
		[Obsolete("Deprecated. Will be removed in the next major release. Index a document containing a field mapped with percolator type")]
		public Task<IUnregisterPercolatorResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IUnregisterPercolatorRequest, DeleteRequestParameters, UnregisterPercolatorResponse, IUnregisterPercolatorResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.DeleteDispatchAsync<UnregisterPercolatorResponse>(p, c)
				);
	}
}
