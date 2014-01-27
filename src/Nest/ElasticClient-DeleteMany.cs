using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IBulkResponse DeleteMany<T>(IEnumerable<T> @objects, string index = null, string type = null) where T : class
		{
			@objects.ThrowIfEmpty("objects");
			var bulk = new BulkDescriptor().FixedPath(index, type);
			foreach (var o in @objects)
			{
				T o1 = o;
				bulk.Delete<T>(b => b.Object(o1));
			}
			return this.Bulk(b => bulk);
		}

		public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index = null, string type = null) where T : class
		{
			@objects.ThrowIfEmpty("objects");
			var bulk = new BulkDescriptor().FixedPath(index, type);
			foreach (var o in @objects)
			{
				T o1 = o;
				bulk.Delete<T>(b => b.Object(o1));
			}
			return this.BulkAsync(b => bulk);
		}


	}
}
