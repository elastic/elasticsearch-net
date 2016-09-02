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
		ICancelTasksResponse CancelTasks(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null);

		/// <inheritdoc/>
		ICancelTasksResponse CancelTasks(ICancelTasksRequest request);

		/// <inheritdoc/>
		Task<ICancelTasksResponse> CancelTasksAsync(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ICancelTasksResponse> CancelTasksAsync(ICancelTasksRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICancelTasksResponse CancelTasks(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null) =>
			this.CancelTasks(selector.InvokeOrDefault(new CancelTasksDescriptor()));

		/// <inheritdoc/>
		public ICancelTasksResponse CancelTasks(ICancelTasksRequest request) =>
			this.Dispatcher.Dispatch<ICancelTasksRequest, CancelTasksRequestParameters, CancelTasksResponse>(
				request,
				(p, d) => this.LowLevelDispatch.TasksCancelDispatch<CancelTasksResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ICancelTasksResponse> CancelTasksAsync(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.CancelTasksAsync(selector.InvokeOrDefault(new CancelTasksDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICancelTasksResponse> CancelTasksAsync(ICancelTasksRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ICancelTasksRequest, CancelTasksRequestParameters, CancelTasksResponse, ICancelTasksResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.TasksCancelDispatchAsync<CancelTasksResponse>(p, c)
			);
	}
}
