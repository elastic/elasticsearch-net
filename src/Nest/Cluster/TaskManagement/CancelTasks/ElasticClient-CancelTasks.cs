using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		///     Does a request to the root of an elasticsearch node
		/// </summary>
		/// <param name="selector">A descriptor to further describe the root operation</param>
		ICancelTasksResponse CancelTasks(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null);

		/// <inheritdoc />
		ICancelTasksResponse CancelTasks(ICancelTasksRequest request);

		/// <inheritdoc />
		Task<ICancelTasksResponse> CancelTasksAsync(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICancelTasksResponse> CancelTasksAsync(ICancelTasksRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICancelTasksResponse CancelTasks(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null) =>
			CancelTasks(selector.InvokeOrDefault(new CancelTasksDescriptor()));

		/// <inheritdoc />
		public ICancelTasksResponse CancelTasks(ICancelTasksRequest request) =>
			Dispatcher.Dispatch<ICancelTasksRequest, CancelTasksRequestParameters, CancelTasksResponse>(
				request,
				(p, d) => LowLevelDispatch.TasksCancelDispatch<CancelTasksResponse>(p)
			);

		/// <inheritdoc />
		public Task<ICancelTasksResponse> CancelTasksAsync(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CancelTasksAsync(selector.InvokeOrDefault(new CancelTasksDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICancelTasksResponse> CancelTasksAsync(ICancelTasksRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<ICancelTasksRequest, CancelTasksRequestParameters, CancelTasksResponse, ICancelTasksResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.TasksCancelDispatchAsync<CancelTasksResponse>(p, c)
			);
	}
}
