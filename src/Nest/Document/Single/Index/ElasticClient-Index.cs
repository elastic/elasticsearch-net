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
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="object">The object to be indexed, Id will be inferred (Id property or IdProperty attribute on type)</param>
		/// <param name="indexSelector">Optionally furter describe the index operation i.e override type/index/id</param>
		IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest<T>> indexSelector = null) where T : class;

		/// <inheritdoc/>
		IIndexResponse Index<T>(IIndexRequest<T> indexRequest) where T : class;

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest<T>> indexSelector = null) where T : class; 

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync<T>(IIndexRequest<T> indexRequest) where T : class;

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest<T>> indexSelector = null)
			where T : class =>
			this.Index(indexSelector?.InvokeOrDefault(new IndexDescriptor<T>(typeof(T), typeof(T)).Document(@object).Id(Id.From(@object))));

		/// <inheritdoc/>
		public IIndexResponse Index<T>(IIndexRequest<T> indexRequest)
			where T : class => 
			this.Dispatcher.Dispatch<IIndexRequest<T>, IndexRequestParameters, IndexResponse>(
				indexRequest, this.LowLevelDispatch.IndexDispatch<IndexResponse>
			);

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest<T>> indexSelector = null)
			where T : class => 
			this.IndexAsync(indexSelector?.InvokeOrDefault(new IndexDescriptor<T>(typeof(T), typeof(T)).Document(@object).Id(Id.From(@object))));

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync<T>(IIndexRequest<T> indexRequest)
			where T : class => 
			this.Dispatcher.DispatchAsync<IIndexRequest<T>, IndexRequestParameters, IndexResponse, IIndexResponse>(
				indexRequest, this.LowLevelDispatch.IndexDispatchAsync<IndexResponse>
			);
	}
}