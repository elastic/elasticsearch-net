using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

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
		Task<ITasksCancelResponse> TasksCancelAsync(Func<TasksCancelDescriptor, ITasksCancelRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ITasksCancelResponse> TasksCancelAsync(ITasksCancelRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
		public Task<ITasksCancelResponse> TasksCancelAsync(Func<TasksCancelDescriptor, ITasksCancelRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.TasksCancelAsync(selector.InvokeOrDefault(new TasksCancelDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ITasksCancelResponse> TasksCancelAsync(ITasksCancelRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ITasksCancelRequest, TasksCancelRequestParameters, TasksCancelResponse, ITasksCancelResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.TasksCancelDispatchAsync<TasksCancelResponse>(p, c)
			);
	}
}
