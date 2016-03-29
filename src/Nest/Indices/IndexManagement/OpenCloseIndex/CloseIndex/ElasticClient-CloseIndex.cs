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
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html</a>
		/// </summary>
		/// <param name="selector">A descriptor thata describes the close index operation</param>
		ICloseIndexResponse CloseIndex(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null);

		/// <inheritdoc/>
		ICloseIndexResponse CloseIndex(ICloseIndexRequest request);

		/// <inheritdoc/>
		Task<ICloseIndexResponse> CloseIndexAsync(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null);

		/// <inheritdoc/>
		Task<ICloseIndexResponse> CloseIndexAsync(ICloseIndexRequest request);
	}

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public ICloseIndexResponse CloseIndex(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null) =>
			this.CloseIndex(selector.InvokeOrDefault(new CloseIndexDescriptor(indices)));

		/// <inheritdoc/>
		public ICloseIndexResponse CloseIndex(ICloseIndexRequest request) => 
			this.Dispatcher.Dispatch<ICloseIndexRequest, CloseIndexRequestParameters, CloseIndexResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesCloseDispatch<CloseIndexResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ICloseIndexResponse> CloseIndexAsync(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null) =>
			this.CloseIndexAsync(selector.InvokeOrDefault(new CloseIndexDescriptor(indices)));

		/// <inheritdoc/>
		public Task<ICloseIndexResponse> CloseIndexAsync(ICloseIndexRequest request) => 
			this.Dispatcher.DispatchAsync<ICloseIndexRequest, CloseIndexRequestParameters, CloseIndexResponse, ICloseIndexResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesCloseDispatchAsync<CloseIndexResponse>(p)
			);
	}
}