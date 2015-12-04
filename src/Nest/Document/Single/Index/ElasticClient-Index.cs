using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The index API adds or updates a typed JSON document in a specific index, making it searchable. 
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html</a>
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="object">The object to be indexed, Id will be inferred (Id property or IdProperty attribute on type)</param>
		/// <param name="selector">Optionally furter describe the index operation i.e override type/index/id</param>
		IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest> selector = null) where T : class;

		/// <inheritdoc/>
		IIndexResponse Index(IIndexRequest indexRequest);

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest> selector = null) where T : class;

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync(IIndexRequest indexRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest> selector = null)
			where T : class =>
			this.Index((@object is IIndexRequest) 
				? (IIndexRequest)@object 
				: selector.InvokeOrDefault(new IndexDescriptor<T>(@object)));

		/// <inheritdoc/>
		public IIndexResponse Index(IIndexRequest indexRequest) =>
			this.Dispatcher.Dispatch<IIndexRequest, IndexRequestParameters, IndexResponse>(
				indexRequest, this.LowLevelDispatch.IndexDispatch<IndexResponse>
			);

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest> selector = null)
			where T : class => 
			this.IndexAsync((@object is IIndexRequest) 
				? (IIndexRequest)@object
				: selector.InvokeOrDefault(new IndexDescriptor<T>(@object)));

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync(IIndexRequest indexRequest) =>
			this.Dispatcher.DispatchAsync<IIndexRequest, IndexRequestParameters, IndexResponse, IIndexResponse>(
				indexRequest, this.LowLevelDispatch.IndexDispatchAsync<IndexResponse>
			);
	}
}