using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest<T>> indexSelector = null)
			where T : class => 
			this.Dispatcher.Dispatch<IIndexRequest<T>, IndexRequestParameters, IndexResponse>(
				indexSelector?.InvokeOrDefault(new IndexDescriptor<T>().Document(@object)),
				(p, d) => this.LowLevelDispatch.IndexDispatch<IndexResponse>(p, @object));

		/// <inheritdoc/>
		public IIndexResponse Index<T>(IIndexRequest<T> indexRequest)
			where T : class => 
			this.Dispatcher.Dispatch<IIndexRequest<T>, IndexRequestParameters, IndexResponse>(
				indexRequest, this.LowLevelDispatch.IndexDispatch<IndexResponse>
			);

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest<T>> indexSelector = null)
			where T : class => 
			this.Dispatcher.DispatchAsync<IIndexRequest<T>, IndexRequestParameters, IndexResponse, IIndexResponse>(
				indexSelector?.InvokeOrDefault(new IndexDescriptor<T>().Document(@object)),
				(p, d) => this.LowLevelDispatch.IndexDispatchAsync<IndexResponse>(p, @object));

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync<T>(IIndexRequest<T> indexRequest)
			where T : class => 
			this.Dispatcher.DispatchAsync<IIndexRequest<T>, IndexRequestParameters, IndexResponse, IIndexResponse>(
				indexRequest, this.LowLevelDispatch.IndexDispatchAsync<IndexResponse>
			);
	}
}