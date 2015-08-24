using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IBulkResponse Bulk(IBulkRequest bulkRequest)
		{
			return this.Dispatcher.Dispatch<IBulkRequest, BulkRequestParameters, BulkResponse>(
				bulkRequest, this.LowLevelDispatch.BulkDispatch<BulkResponse>
			);
		}
		
		/// <inheritdoc/>
		public IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			return this.Dispatcher.Dispatch<BulkDescriptor, BulkRequestParameters, BulkResponse>(
				bulkSelector, this.LowLevelDispatch.BulkDispatch<BulkResponse>
			);
		}

		/// <inheritdoc/>
		public Task<IBulkResponse> BulkAsync(IBulkRequest bulkRequest)
		{
			return this.Dispatcher.DispatchAsync<IBulkRequest, BulkRequestParameters, BulkResponse, IBulkResponse>(
				bulkRequest, this.LowLevelDispatch.BulkDispatchAsync<BulkResponse>
			);
		}

		/// <inheritdoc/>
		public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, BulkDescriptor> bulkSelector)
		{
			return this.Dispatcher.DispatchAsync<BulkDescriptor, BulkRequestParameters, BulkResponse, IBulkResponse>(
				bulkSelector, this.LowLevelDispatch.BulkDispatchAsync<BulkResponse>
			);
		}
	}
}