using System;
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
		/// <param name="request">A descriptor the describe the index/create/delete operation for this bulk operation</param>
		IBulkResponse Bulk(IBulkRequest request);

		/// <inheritdoc/>
		IBulkResponse Bulk(Func<BulkDescriptor, IBulkRequest> selector = null);

		/// <inheritdoc/>
		Task<IBulkResponse> BulkAsync(IBulkRequest request);

		/// <inheritdoc/>
		Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> selector = null);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IBulkResponse Bulk(IBulkRequest request) =>
			this.Dispatcher.Dispatch<IBulkRequest, BulkRequestParameters, BulkResponse>(
				request, this.LowLevelDispatch.BulkDispatch<BulkResponse>
			);

		/// <inheritdoc/>
		public IBulkResponse Bulk(Func<BulkDescriptor, IBulkRequest> selector = null)
		{
			// selector should not be nullable, but we can't change it for backwards compatibility reasons
			if (selector == null)
				throw new ArgumentNullException(nameof(selector));
            return this.Bulk(selector.InvokeOrDefault(new BulkDescriptor()));
		}

		/// <inheritdoc/>
		public Task<IBulkResponse> BulkAsync(IBulkRequest request) =>
			this.Dispatcher.DispatchAsync<IBulkRequest, BulkRequestParameters, BulkResponse, IBulkResponse>(
				request, this.LowLevelDispatch.BulkDispatchAsync<BulkResponse>
			);

		/// <inheritdoc/>
		public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> selector = null)
		{
			// selector should not be nullable, but we can't change it for backwards compatibility reasons
			if (selector == null)
				throw new ArgumentNullException(nameof(selector));
			return this.BulkAsync(selector.InvokeOrDefault(new BulkDescriptor()));
		}
	}
}
