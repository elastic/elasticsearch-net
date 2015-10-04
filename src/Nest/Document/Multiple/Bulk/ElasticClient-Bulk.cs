using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The bulk API makes it possible to perform many index/delete operations in a single API call. 
		/// This can greatly increase the indexing speed.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html
		/// </summary>
		/// <param name="bulkRequest">A descriptor the describe the index/create/delete operation for this bulk operation</param>
		IBulkResponse Bulk(IBulkRequest bulkRequest);

		/// <inheritdoc/>
		IBulkResponse Bulk(Func<BulkDescriptor, IBulkRequest> bulkSelector = null);

		/// <inheritdoc/>
		Task<IBulkResponse> BulkAsync(IBulkRequest bulkRequest);

		/// <inheritdoc/>
		Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> bulkSelector = null);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IBulkResponse Bulk(IBulkRequest bulkRequest) => 
			this.Dispatcher.Dispatch<IBulkRequest, BulkRequestParameters, BulkResponse>(
				bulkRequest, this.LowLevelDispatch.BulkDispatch<BulkResponse>
			);

		/// <inheritdoc/>
		public IBulkResponse Bulk(Func<BulkDescriptor, IBulkRequest> bulkSelector = null) =>
			this.Bulk(bulkSelector.InvokeOrDefault(new BulkDescriptor()));

		/// <inheritdoc/>
		public Task<IBulkResponse> BulkAsync(IBulkRequest bulkRequest) => 
			this.Dispatcher.DispatchAsync<IBulkRequest, BulkRequestParameters, BulkResponse, IBulkResponse>(
				bulkRequest, this.LowLevelDispatch.BulkDispatchAsync<BulkResponse>
			);

		/// <inheritdoc/>
		public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> bulkSelector = null) =>
			this.BulkAsync(bulkSelector.InvokeOrDefault(new BulkDescriptor()));
	}
}