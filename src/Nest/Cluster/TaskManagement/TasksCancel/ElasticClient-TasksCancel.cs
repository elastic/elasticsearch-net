using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Does a request to the root of an elasticsearch node
		/// </summary>
		/// <param name="selector">A descriptor to further describe the root operation</param>
		ITasksCancelResponse TasksCancel(Func<TasksCancelDescriptor, ITasksCancelRequest> selector = null);

		/// <inheritdoc/>
		ITasksCancelResponse TasksCancel(ITasksCancelRequest request);

		/// <inheritdoc/>
		Task<ITasksCancelResponse> TasksCancelAsync(Func<TasksCancelDescriptor, ITasksCancelRequest> selector = null);

		/// <inheritdoc/>
		Task<ITasksCancelResponse> TasksCancelAsync(ITasksCancelRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ITasksCancelResponse TasksCancel(Func<TasksCancelDescriptor, ITasksCancelRequest> selector = null) =>
			this.TasksCancel(selector.InvokeOrDefault(new TasksCancelDescriptor()));

		/// <inheritdoc/>
		public ITasksCancelResponse TasksCancel(ITasksCancelRequest request) =>
			this.Dispatcher.Dispatch<ITasksCancelRequest, TasksCancelRequestParameters, TasksCancelResponse>(
				request,
				(p, d) => this.LowLevelDispatch.TasksCancelDispatch<TasksCancelResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ITasksCancelResponse> TasksCancelAsync(Func<TasksCancelDescriptor, ITasksCancelRequest> selector = null) =>
			this.TasksCancelAsync(selector.InvokeOrDefault(new TasksCancelDescriptor()));

		/// <inheritdoc/>
		public Task<ITasksCancelResponse> TasksCancelAsync(ITasksCancelRequest request) =>
			this.Dispatcher.DispatchAsync<ITasksCancelRequest, TasksCancelRequestParameters, TasksCancelResponse, ITasksCancelResponse>(
				request,
				(p, d) => this.LowLevelDispatch.TasksCancelDispatchAsync<TasksCancelResponse>(p)
			);
	}
}
