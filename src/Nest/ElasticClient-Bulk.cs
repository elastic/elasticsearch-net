using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IBulkResponse Bulk(IBulkRequest bulkRequest)
		{
			return this.Dispatcher.Dispatch<IBulkRequest, BulkRequestParameters, BulkResponse>(
				bulkRequest,
				(p, d) =>
				{
					var json = Serializer.SerializeBulkDescriptor(d);
					return this.RawDispatch.BulkDispatch<BulkResponse>(p, json);
				}
			);
		}
		
		/// <inheritdoc />
		public IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			return this.Dispatcher.Dispatch<BulkDescriptor, BulkRequestParameters, BulkResponse>(
				bulkSelector,
				(p, d) =>
				{
					var json = Serializer.SerializeBulkDescriptor(d);
					return this.RawDispatch.BulkDispatch<BulkResponse>(p, json);
				}
			);
		}

		/// <inheritdoc />
		public Task<IBulkResponse> BulkAsync(IBulkRequest bulkRequest)
		{
			return this.Dispatcher.DispatchAsync<IBulkRequest, BulkRequestParameters, BulkResponse, IBulkResponse>(
				bulkRequest,
				(p, d) =>
				{
					var json = Serializer.SerializeBulkDescriptor(d);
					return this.RawDispatch.BulkDispatchAsync<BulkResponse>(p, json);
				}
			);
		}

		/// <inheritdoc />
		public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			return this.Dispatcher.DispatchAsync<BulkDescriptor, BulkRequestParameters, BulkResponse, IBulkResponse>(
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