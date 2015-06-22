using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null)
		{
			return this.DoCat<CatPendingTasksDescriptor, CatPendingTasksRequestParameters, CatPendingTasksRecord>(selector, this.RawDispatch.CatPendingTasksDispatch<CatResponse<CatPendingTasksRecord>>);
		}

		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request)
		{
			return this.DoCat<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, this.RawDispatch.CatPendingTasksDispatch<CatResponse<CatPendingTasksRecord>>);
		}

		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null)
		{
			return this.DoCatAsync<CatPendingTasksDescriptor, CatPendingTasksRequestParameters, CatPendingTasksRecord>(selector, this.RawDispatch.CatPendingTasksDispatchAsync<CatResponse<CatPendingTasksRecord>>);
		}

		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request)
		{
			return this.DoCatAsync<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, this.RawDispatch.CatPendingTasksDispatchAsync<CatResponse<CatPendingTasksRecord>>);
		}
	}
}