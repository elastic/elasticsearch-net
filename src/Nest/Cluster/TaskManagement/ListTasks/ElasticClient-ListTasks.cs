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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IListTasksResponse> ListTasksAsync(IListTasksRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IListTasksResponse ListTasks(Func<ListTasksDescriptor, IListTasksRequest> selector = null) =>
			ListTasks(selector.InvokeOrDefault(new ListTasksDescriptor()));

		/// <inheritdoc />
		public IListTasksResponse ListTasks(IListTasksRequest request) =>
			Dispatch2<IListTasksRequest, ListTasksResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IListTasksResponse> ListTasksAsync(Func<ListTasksDescriptor, IListTasksRequest> selector = null,
			CancellationToken ct = default
		) =>
			ListTasksAsync(selector.InvokeOrDefault(new ListTasksDescriptor()), ct);

		/// <inheritdoc />
		public Task<IListTasksResponse> ListTasksAsync(IListTasksRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IListTasksRequest, IListTasksResponse, ListTasksResponse>(request, request.RequestParameters, ct);
	}
}
