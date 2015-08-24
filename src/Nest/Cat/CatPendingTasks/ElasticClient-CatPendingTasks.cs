using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null) =>
			this.DoCat<CatPendingTasksDescriptor, CatPendingTasksRequestParameters, CatPendingTasksRecord>(selector, this.LowLevelDispatch.CatPendingTasksDispatch<CatResponse<CatPendingTasksRecord>>);

		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request) =>
			this.DoCat<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, this.LowLevelDispatch.CatPendingTasksDispatch<CatResponse<CatPendingTasksRecord>>);

		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null) =>
			this.DoCatAsync<CatPendingTasksDescriptor, CatPendingTasksRequestParameters, CatPendingTasksRecord>(selector, this.LowLevelDispatch.CatPendingTasksDispatchAsync<CatResponse<CatPendingTasksRecord>>);

		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request) =>
			this.DoCatAsync<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, this.LowLevelDispatch.CatPendingTasksDispatchAsync<CatResponse<CatPendingTasksRecord>>);
	}
}