using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		IGetTaskResponse GetTask(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null);

		IGetTaskResponse GetTask(IGetTaskRequest request);

		Task<IGetTaskResponse> GetTaskAsync(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		Task<IGetTaskResponse> GetTaskAsync(IGetTaskRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		public IGetTaskResponse GetTask(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null) =>
			this.GetTask(selector.InvokeOrDefault(new GetTaskDescriptor(id)));

		public IGetTaskResponse GetTask(IGetTaskRequest request) =>
			this.Dispatcher.Dispatch<IGetTaskRequest, GetTaskRequestParameters, GetTaskResponse>(
				request,
				(p, d) => this.LowLevelDispatch.TasksGetDispatch<GetTaskResponse>(p)
			);

		public Task<IGetTaskResponse> GetTaskAsync(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetTaskAsync(selector.InvokeOrDefault(new GetTaskDescriptor(id)), cancellationToken);

		public Task<IGetTaskResponse> GetTaskAsync(IGetTaskRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetTaskRequest, GetTaskRequestParameters, GetTaskResponse, IGetTaskResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.TasksGetDispatchAsync<GetTaskResponse>(p, c)
			);
	}
}
