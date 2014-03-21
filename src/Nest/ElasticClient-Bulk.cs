using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			return this.Dispatch<BulkDescriptor, BulkQueryString, BulkResponse>(
				bulkSelector,
				(p, d) =>
				{
					var json = Serializer.SerializeBulkDescriptor(d);
					return this.RawDispatch.BulkDispatch<BulkResponse>(p, json);
				}
			);
		}

		/// <inheritdoc />
		public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			return this.DispatchAsync<BulkDescriptor, BulkQueryString, BulkResponse, IBulkResponse>(
				bulkSelector,
				(p, d) =>
				{
					var json = Serializer.SerializeBulkDescriptor(d);
					return this.RawDispatch.BulkDispatchAsync<BulkResponse>(p, json);
				}
			);
		}
	}
}