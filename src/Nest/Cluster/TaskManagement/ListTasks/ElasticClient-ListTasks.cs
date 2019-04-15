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
		ListTasksResponse ListTasks(Func<ListTasksDescriptor, IListTasksRequest> selector = null);

		/// <inheritdoc />
		ListTasksResponse ListTasks(IListTasksRequest request);

		/// <inheritdoc />
		Task<ListTasksResponse> ListTasksAsync(Func<ListTasksDescriptor, IListTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ListTasksResponse> ListTasksAsync(IListTasksRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ListTasksResponse ListTasks(Func<ListTasksDescriptor, IListTasksRequest> selector = null) =>
			ListTasks(selector.InvokeOrDefault(new ListTasksDescriptor()));

		/// <inheritdoc />
		public ListTasksResponse ListTasks(IListTasksRequest request) =>
			DoRequest<IListTasksRequest, ListTasksResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ListTasksResponse> ListTasksAsync(Func<ListTasksDescriptor, IListTasksRequest> selector = null,
			CancellationToken ct = default
		) =>
			ListTasksAsync(selector.InvokeOrDefault(new ListTasksDescriptor()), ct);

		/// <inheritdoc />
		public Task<ListTasksResponse> ListTasksAsync(IListTasksRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IListTasksRequest, ListTasksResponse, ListTasksResponse>(request, request.RequestParameters, ct);
	}
}
