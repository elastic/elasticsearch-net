using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IndexDescriptor<T>> indexSelector = null)
			where T : class
		{
			@object.ThrowIfNull("object");
			indexSelector = indexSelector ?? (s => s);
			var descriptor = indexSelector(new IndexDescriptor<T>().Object(@object));
			return this.Dispatch<IndexDescriptor<T>, IndexRequestParameters, IndexResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndexDispatch<IndexResponse>(p, @object));
		}

		/// <inheritdoc />
		public IIndexResponse Index<T>(IIndexRequest<T> indexRequest)
			where T : class
		{
			return this.Dispatch<IIndexRequest<T>, IndexRequestParameters, IndexResponse>(
				indexRequest,
				(p, d) => this.RawDispatch.IndexDispatch<IndexResponse>(p, indexRequest.Document));
		}

		/// <inheritdoc />
		public Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IndexDescriptor<T>> indexSelector = null)
			where T : class
		{
			@object.ThrowIfNull("object");
			indexSelector = indexSelector ?? (s => s);
			var descriptor = indexSelector(new IndexDescriptor<T>().Object(@object));
			return this.DispatchAsync<IndexDescriptor<T>, IndexRequestParameters, IndexResponse, IIndexResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndexDispatchAsync<IndexResponse>(p, @object));
		}

		/// <inheritdoc />
		public Task<IIndexResponse> IndexAsync<T>(IIndexRequest<T> indexRequest)
			where T : class
		{
			return this.DispatchAsync<IIndexRequest<T>, IndexRequestParameters, IndexResponse, IIndexResponse>(
				indexRequest,
				(p, d) => this.RawDispatch.IndexDispatchAsync<IndexResponse>(p, indexRequest.Document));
		}

		// TODO move to convenience extensions

		/// <inheritdoc />
		public IBulkResponse IndexMany<T>(IEnumerable<T> @objects, string index = null, string type = null) 
			where T : class
		{
			@objects.ThrowIfEmpty("objects");
			var bulk = new BulkDescriptor().FixedPath(index, type);
			foreach (var o in @objects)
			{
				var o1 = o;
				bulk.Index<T>(b => b.Document(o1));
			}
			return Bulk(b => bulk);
		}

		/// <inheritdoc />
		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index = null, string type = null)
			where T : class
		{
			@objects.ThrowIfEmpty("objects");
			var bulk = new BulkDescriptor().FixedPath(index, type);
			foreach (var o in @objects)
			{
				var o1 = o;
				bulk.Index<T>(b => b.Document(o1));
			}
			return BulkAsync(b => bulk);
		}
	}
}