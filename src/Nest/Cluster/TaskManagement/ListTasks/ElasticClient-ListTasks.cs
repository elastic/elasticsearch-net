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
		IListTasksResponse ListTasks(Func<ListTasksDescriptor, IListTasksRequest> selector = null);

		/// <inheritdoc/>
		IListTasksResponse ListTasks(IListTasksRequest request);

		/// <inheritdoc/>
		Task<IListTasksResponse> ListTasksAsync(Func<ListTasksDescriptor, IListTasksRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IListTasksResponse> ListTasksAsync(IListTasksRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IListTasksResponse ListTasks(Func<ListTasksDescriptor, IListTasksRequest> selector = null) =>
			this.ListTasks(selector.InvokeOrDefault(new ListTasksDescriptor()));

		/// <inheritdoc/>
		public IListTasksResponse ListTasks(IListTasksRequest request) =>
			this.Dispatcher.Dispatch<IListTasksRequest, ListTasksRequestParameters, ListTasksResponse>(
				request,
				(p, d) => this.LowLevelDispatch.TasksListDispatch<ListTasksResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IListTasksResponse> ListTasksAsync(Func<ListTasksDescriptor, IListTasksRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ListTasksAsync(selector.InvokeOrDefault(new ListTasksDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IListTasksResponse> ListTasksAsync(IListTasksRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IListTasksRequest, ListTasksRequestParameters, ListTasksResponse, IListTasksResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.TasksListDispatchAsync<ListTasksResponse>(p, c)
			);
	}
}
