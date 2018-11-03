using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieve information about the tasks currently executing on one or more nodes in the cluster.
		/// </summary>
		/// <param name="selector">A descriptor to further describe the tasks to retrieve information for</param>
		IListTasksResponse ListTasks(Func<ListTasksDescriptor, IListTasksRequest> selector = null);

		/// <inheritdoc />
		IListTasksResponse ListTasks(IListTasksRequest request);

		/// <inheritdoc />
		Task<IListTasksResponse> ListTasksAsync(Func<ListTasksDescriptor, IListTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IListTasksResponse> ListTasksAsync(IListTasksRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IListTasksResponse ListTasks(Func<ListTasksDescriptor, IListTasksRequest> selector = null) =>
			ListTasks(selector.InvokeOrDefault(new ListTasksDescriptor()));

		/// <inheritdoc />
		public IListTasksResponse ListTasks(IListTasksRequest request) =>
			Dispatcher.Dispatch<IListTasksRequest, ListTasksRequestParameters, ListTasksResponse>(
				request,
				(p, d) => LowLevelDispatch.TasksListDispatch<ListTasksResponse>(p)
			);

		/// <inheritdoc />
		public Task<IListTasksResponse> ListTasksAsync(Func<ListTasksDescriptor, IListTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ListTasksAsync(selector.InvokeOrDefault(new ListTasksDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IListTasksResponse> ListTasksAsync(IListTasksRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IListTasksRequest, ListTasksRequestParameters, ListTasksResponse, IListTasksResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.TasksListDispatchAsync<ListTasksResponse>(p, c)
			);
	}
}
