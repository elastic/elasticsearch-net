using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieve information about the tasks currently executing on one or more nodes in the cluster.
		/// </summary>
		/// <param name="selector">A descriptor to further describe the tasks to retrieve information for</param>
		ITasksListResponse TasksList(Func<TasksListDescriptor, ITasksListRequest> selector = null);

		/// <inheritdoc/>
		ITasksListResponse TasksList(ITasksListRequest request);

		/// <inheritdoc/>
		Task<ITasksListResponse> TasksListAsync(Func<TasksListDescriptor, ITasksListRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ITasksListResponse> TasksListAsync(ITasksListRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ITasksListResponse TasksList(Func<TasksListDescriptor, ITasksListRequest> selector = null) =>
			this.TasksList(selector.InvokeOrDefault(new TasksListDescriptor()));

		/// <inheritdoc/>
		public ITasksListResponse TasksList(ITasksListRequest request) =>
			this.Dispatcher.Dispatch<ITasksListRequest, TasksListRequestParameters, TasksListResponse>(
				request,
				(p, d) => this.LowLevelDispatch.TasksListDispatch<TasksListResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ITasksListResponse> TasksListAsync(Func<TasksListDescriptor, ITasksListRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.TasksListAsync(selector.InvokeOrDefault(new TasksListDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ITasksListResponse> TasksListAsync(ITasksListRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ITasksListRequest, TasksListRequestParameters, TasksListResponse, ITasksListResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.TasksListDispatchAsync<TasksListResponse>(p, c)
			);
	}
}
