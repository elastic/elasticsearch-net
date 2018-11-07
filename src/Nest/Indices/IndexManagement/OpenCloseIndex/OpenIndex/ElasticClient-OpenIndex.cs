using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it.
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked
		/// for read/write operations.
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the open index operation</param>
		IOpenIndexResponse OpenIndex(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null);

		/// <inheritdoc />
		IOpenIndexResponse OpenIndex(IOpenIndexRequest request);

		/// <inheritdoc />
		Task<IOpenIndexResponse> OpenIndexAsync(
			Indices indices,
			Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IOpenIndexResponse> OpenIndexAsync(IOpenIndexRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IOpenIndexResponse OpenIndex(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null) =>
			OpenIndex(selector.InvokeOrDefault(new OpenIndexDescriptor(indices)));

		/// <inheritdoc />
		public IOpenIndexResponse OpenIndex(IOpenIndexRequest request) =>
			Dispatcher.Dispatch<IOpenIndexRequest, OpenIndexRequestParameters, OpenIndexResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesOpenDispatch<OpenIndexResponse>(p)
			);

		/// <inheritdoc />
		public Task<IOpenIndexResponse> OpenIndexAsync(
			Indices indices,
			Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => OpenIndexAsync(selector.InvokeOrDefault(new OpenIndexDescriptor(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IOpenIndexResponse> OpenIndexAsync(IOpenIndexRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IOpenIndexRequest, OpenIndexRequestParameters, OpenIndexResponse, IOpenIndexResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesOpenDispatchAsync<OpenIndexResponse>(p, c)
			);
	}
}
