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
		/// <param name="selector">A descriptor that describes the open index operation</param>
		IOpenIndexResponse OpenIndex(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null);

		/// <inheritdoc/>
		IOpenIndexResponse OpenIndex(IOpenIndexRequest request);

		/// <inheritdoc/>
		Task<IOpenIndexResponse> OpenIndexAsync(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null);

		/// <inheritdoc/>
		Task<IOpenIndexResponse> OpenIndexAsync(IOpenIndexRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IOpenIndexResponse OpenIndex(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null) =>
			this.OpenIndex(selector.InvokeOrDefault(new OpenIndexDescriptor(indices)));

		/// <inheritdoc/>
		public IOpenIndexResponse OpenIndex(IOpenIndexRequest request) => 
			this.Dispatcher.Dispatch<IOpenIndexRequest, OpenIndexRequestParameters, OpenIndexResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesOpenDispatch<OpenIndexResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IOpenIndexResponse> OpenIndexAsync(Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null)=>
			this.OpenIndexAsync(selector.InvokeOrDefault(new OpenIndexDescriptor(indices)));

		/// <inheritdoc/>
		public Task<IOpenIndexResponse> OpenIndexAsync(IOpenIndexRequest request) => 
			this.Dispatcher.DispatchAsync<IOpenIndexRequest, OpenIndexRequestParameters, OpenIndexResponse, IOpenIndexResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesOpenDispatchAsync<OpenIndexResponse>(p)
			);
	}
}