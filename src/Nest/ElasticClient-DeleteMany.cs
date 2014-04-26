using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IBulkResponse DeleteMany<T>(IEnumerable<T> @objects, string index = null, string type = null) where T : class
		{
			@objects.ThrowIfEmpty("objects");
			return this.Bulk(b => b.FixedPath(index, type).DeleteMany(@objects));
		}

		/// <inheritdoc />
		public Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index = null, string type = null)
			where T : class
		{
			@objects.ThrowIfEmpty("objects");
			return this.BulkAsync(b => b.FixedPath(index, type).DeleteMany(@objects));
		}
	}
}