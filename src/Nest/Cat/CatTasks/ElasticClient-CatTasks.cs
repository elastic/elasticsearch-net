using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatTasksRecord> CatTasks(Func<CatTasksDescriptor, ICatTasksRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatTasksRecord> CatTasks(ICatTasksRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatTasksRecord>> CatTasksAsync(
			Func<CatTasksDescriptor, ICatTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<ICatResponse<CatTasksRecord>> CatTasksAsync(ICatTasksRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatTasksRecord> CatTasks(Func<CatTasksDescriptor, ICatTasksRequest> selector = null) =>
			this.CatTasks(selector.InvokeOrDefault(new CatTasksDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatTasksRecord> CatTasks(ICatTasksRequest request) =>
			this.DoCat<ICatTasksRequest, CatTasksRequestParameters, CatTasksRecord>(
				request,
				this.LowLevelDispatch.CatTasksDispatch<CatResponse<CatTasksRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatTasksRecord>> CatTasksAsync(
			Func<CatTasksDescriptor, ICatTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.CatTasksAsync(selector.InvokeOrDefault(new CatTasksDescriptor()));

		/// <inheritdoc/>
		public Task<ICatResponse<CatTasksRecord>> CatTasksAsync(ICatTasksRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatTasksRequest, CatTasksRequestParameters, CatTasksRecord>(
				request,
				cancellationToken,
				this.LowLevelDispatch.CatTasksDispatchAsync<CatResponse<CatTasksRecord>>
			);
	}
}
