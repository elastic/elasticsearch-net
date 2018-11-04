using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices,
		/// including executing operations across several indices.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the create index operation</param>
		ICreateIndexResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null);

		/// <inheritdoc />
		ICreateIndexResponse CreateIndex(ICreateIndexRequest request);

		/// <inheritdoc />
		Task<ICreateIndexResponse> CreateIndexAsync(
			IndexName index,
			Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICreateIndexResponse> CreateIndexAsync(ICreateIndexRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICreateIndexResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null) =>
			CreateIndex(selector.InvokeOrDefault(new CreateIndexDescriptor(index)));

		/// <inheritdoc />
		public ICreateIndexResponse CreateIndex(ICreateIndexRequest request) =>
			Dispatcher.Dispatch<ICreateIndexRequest, CreateIndexRequestParameters, CreateIndexResponse>(
				request,
				LowLevelDispatch.IndicesCreateDispatch<CreateIndexResponse>
			);

		/// <inheritdoc />
		public Task<ICreateIndexResponse> CreateIndexAsync(
			IndexName index,
			Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => CreateIndexAsync(selector.InvokeOrDefault(new CreateIndexDescriptor(index)));

		/// <inheritdoc />
		public Task<ICreateIndexResponse> CreateIndexAsync(ICreateIndexRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<ICreateIndexRequest, CreateIndexRequestParameters, CreateIndexResponse, ICreateIndexResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.IndicesCreateDispatchAsync<CreateIndexResponse>
			);
	}
}
