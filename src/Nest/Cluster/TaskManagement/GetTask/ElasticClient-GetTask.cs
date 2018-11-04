using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IGetTaskResponse GetTask(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null);

		IGetTaskResponse GetTask(IGetTaskRequest request);

		Task<IGetTaskResponse> GetTaskAsync(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		Task<IGetTaskResponse> GetTaskAsync(IGetTaskRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		public IGetTaskResponse GetTask(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null) =>
			GetTask(selector.InvokeOrDefault(new GetTaskDescriptor().TaskId(id)));

		public IGetTaskResponse GetTask(IGetTaskRequest request) =>
			Dispatcher.Dispatch<IGetTaskRequest, GetTaskRequestParameters, GetTaskResponse>(
				request,
				(p, d) => LowLevelDispatch.TasksGetDispatch<GetTaskResponse>(p)
			);

		public Task<IGetTaskResponse> GetTaskAsync(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetTaskAsync(selector.InvokeOrDefault(new GetTaskDescriptor().TaskId(id)), cancellationToken);

		public Task<IGetTaskResponse> GetTaskAsync(IGetTaskRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetTaskRequest, GetTaskRequestParameters, GetTaskResponse, IGetTaskResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.TasksGetDispatchAsync<GetTaskResponse>(p, c)
			);
	}
}
