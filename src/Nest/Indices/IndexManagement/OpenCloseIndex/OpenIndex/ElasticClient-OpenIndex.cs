using System;
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
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <param name="openIndexSelector">A descriptor thata describes the open index operation</param>
		IIndicesOperationResponse OpenIndex(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> openIndexSelector = null);

		/// <inheritdoc/>
		IIndicesOperationResponse OpenIndex(IOpenIndexRequest openIndexRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> OpenIndexAsync(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> openIndexSelector = null);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> OpenIndexAsync(IOpenIndexRequest openIndexRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse OpenIndex(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> openIndexSelector = null) =>
			this.OpenIndex(openIndexSelector.InvokeOrDefault(new OpenIndexDescriptor(indices)));

		/// <inheritdoc/>
		public IIndicesOperationResponse OpenIndex(IOpenIndexRequest openIndexRequest) => 
			this.Dispatcher.Dispatch<IOpenIndexRequest, OpenIndexRequestParameters, IndicesOperationResponse>(
				openIndexRequest,
				(p, d) => this.LowLevelDispatch.IndicesOpenDispatch<IndicesOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> OpenIndexAsync(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> openIndexSelector = null)=>
			this.OpenIndexAsync(openIndexSelector.InvokeOrDefault(new OpenIndexDescriptor(indices)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> OpenIndexAsync(IOpenIndexRequest openIndexRequest) => 
			this.Dispatcher.DispatchAsync<IOpenIndexRequest, OpenIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				openIndexRequest,
				(p, d) => this.LowLevelDispatch.IndicesOpenDispatchAsync<IndicesOperationResponse>(p)
			);
	}
}