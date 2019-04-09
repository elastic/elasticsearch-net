using System;
using System.Threading;
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
		ICancelTasksResponse CancelTasks(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null);

		/// <inheritdoc />
		ICancelTasksResponse CancelTasks(ICancelTasksRequest request);

		/// <inheritdoc />
		Task<ICancelTasksResponse> CancelTasksAsync(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ICancelTasksResponse> CancelTasksAsync(ICancelTasksRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICancelTasksResponse CancelTasks(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null) =>
			CancelTasks(selector.InvokeOrDefault(new CancelTasksDescriptor()));

		/// <inheritdoc />
		public ICancelTasksResponse CancelTasks(ICancelTasksRequest request) =>
			Dispatch2<ICancelTasksRequest, CancelTasksResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ICancelTasksResponse> CancelTasksAsync(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null,
			CancellationToken ct = default
		) =>
			CancelTasksAsync(selector.InvokeOrDefault(new CancelTasksDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICancelTasksResponse> CancelTasksAsync(ICancelTasksRequest request, CancellationToken ct = default) =>
			Dispatch2Async<ICancelTasksRequest, ICancelTasksResponse, CancelTasksResponse>(request, request.RequestParameters, ct);
	}
}
