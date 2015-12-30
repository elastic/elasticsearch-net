using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices, 
		/// including executing operations across several indices.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the create index operation</param>
		IAcknowledgedResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null);

		/// <inheritdoc/>
		IAcknowledgedResponse CreateIndex(ICreateIndexRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> CreateIndexAsync(ICreateIndexRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null) =>
			this.CreateIndex(selector.InvokeOrDefault(new CreateIndexDescriptor(index)));

		/// <inheritdoc/>
		public IAcknowledgedResponse CreateIndex(ICreateIndexRequest request) => 
			this.Dispatcher.Dispatch<ICreateIndexRequest, CreateIndexRequestParameters, AcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesCreateDispatch<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null) => 
			this.CreateIndexAsync(selector.InvokeOrDefault(new CreateIndexDescriptor(index)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> CreateIndexAsync(ICreateIndexRequest request) => 
			this.Dispatcher.DispatchAsync<ICreateIndexRequest, CreateIndexRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesCreateDispatchAsync<AcknowledgedResponse>
			);
	}
}