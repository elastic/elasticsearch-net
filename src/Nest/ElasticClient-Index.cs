using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IndexDescriptor<T>> indexSelector = null)
			where T : class
		{
			@object.ThrowIfNull("object");
			indexSelector = indexSelector ?? (s => s);
			var descriptor = indexSelector(new IndexDescriptor<T>().Object(@object));
			return this.Dispatch<IndexDescriptor<T>, IndexQueryString, IndexResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndexDispatch<IndexResponse>(p, @object));
		}
		public Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IndexDescriptor<T>> indexSelector = null)
			where T : class
		{
			@object.ThrowIfNull("object");
			indexSelector = indexSelector ?? (s => s);
			var descriptor = indexSelector(new IndexDescriptor<T>().Object(@object));
			return this.DispatchAsync<IndexDescriptor<T>, IndexQueryString, IndexResponse, IIndexResponse>(
				descriptor,
				(p, d) => this.RawDispatch.IndexDispatchAsync<IndexResponse>(p, @object));
		}


		public IBulkResponse IndexMany<T>(IEnumerable<T> @objects, string index = null, string type = null) where T : class
		{
			@objects.ThrowIfEmpty("objects");
			var bulk = new BulkDescriptor().FixedPath(index, type);
			foreach (var o in @objects)
			{
				T o1 = o;
				bulk.Index<T>(b => b.Object(o1));
			}
			return this.Bulk(b => bulk);
		}

		public Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index = null, string type = null)
			where T : class
		{
			@objects.ThrowIfEmpty("objects");
			var bulk = new BulkDescriptor().FixedPath(index, type);
			foreach (var o in @objects)
			{
				T o1 = o;
				bulk.Index<T>(b => b.Object(o1));
			}
			return this.BulkAsync(b => bulk);
		}

	}
}