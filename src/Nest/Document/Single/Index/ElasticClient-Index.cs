using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Adds or updates a typed JSON document in a specific index, making it searchable.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html</a>
		/// </summary>
		/// <typeparam name="T">The document type used to infer the default index, type and id</typeparam>
		/// <param name="document">The document to be indexed. Id will be inferred from (in order):
		/// <para>1. Id property set up on <see cref="ConnectionSettings"/> for <typeparamref name="T"/></para> 
		/// <para>2. <see cref="ElasticsearchTypeAttribute.IdProperty"/> property on <see cref="ElasticsearchTypeAttribute"/> applied to <typeparamref name="T"/></para> 
		/// <para>3. A property named Id on <typeparamref name="T"/></para> 
		/// </param>
		/// <param name="selector">Optionally further describe the index operation i.e override type, index, id</param>
		IIndexResponse Index<T>(T document, Func<IndexDescriptor<T>, IIndexRequest> selector = null) where T : class;

		/// <inheritdoc/>
		IIndexResponse Index(IIndexRequest request);

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync<T>(T document, Func<IndexDescriptor<T>, IIndexRequest> selector = null) where T : class;

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync(IIndexRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndexResponse Index<T>(T document, Func<IndexDescriptor<T>, IIndexRequest> selector = null)
			where T : class
		{
			var request = document as IIndexRequest;
			return this.Index(request ?? selector.InvokeOrDefault(new IndexDescriptor<T>(document)));
		}

		/// <inheritdoc/>
		public IIndexResponse Index(IIndexRequest request) =>
			this.Dispatcher.Dispatch<IIndexRequest, IndexRequestParameters, IndexResponse>(
				request,
				this.LowLevelDispatch.IndexDispatch<IndexResponse>
			);

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync<T>(T document, Func<IndexDescriptor<T>, IIndexRequest> selector = null)
			where T : class
		{
			var request = document as IIndexRequest;
			return this.IndexAsync(request ?? selector.InvokeOrDefault(new IndexDescriptor<T>(document)));
		}

		/// <inheritdoc/>
		public Task<IIndexResponse> IndexAsync(IIndexRequest request) =>
			this.Dispatcher.DispatchAsync<IIndexRequest, IndexRequestParameters, IndexResponse, IIndexResponse>(
				request, this.LowLevelDispatch.IndexDispatchAsync<IndexResponse>
			);
	}
}
