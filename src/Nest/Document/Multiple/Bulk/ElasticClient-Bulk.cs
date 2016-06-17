using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

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
		IBulkResponse Bulk(Func<BulkDescriptor, IBulkRequest> selector);

		/// <inheritdoc/>
		Task<IBulkResponse> BulkAsync(IBulkRequest request, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IBulkResponse Bulk(IBulkRequest request) =>
			this.Dispatcher.Dispatch<IBulkRequest, BulkRequestParameters, BulkResponse>(
				request, this.LowLevelDispatch.BulkDispatch<BulkResponse>
			);

		/// <inheritdoc/>
		public IBulkResponse Bulk(Func<BulkDescriptor, IBulkRequest> selector) =>
			this.Bulk(selector.InvokeOrDefault(new BulkDescriptor()));

		/// <inheritdoc/>
		public Task<IBulkResponse> BulkAsync(IBulkRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IBulkRequest, BulkRequestParameters, BulkResponse, IBulkResponse>(
				request, cancellationToken, this.LowLevelDispatch.BulkDispatchAsync<BulkResponse>
			);

		/// <inheritdoc/>
		public Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, IBulkRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.BulkAsync(selector.InvokeOrDefault(new BulkDescriptor()), cancellationToken);
	}
}
