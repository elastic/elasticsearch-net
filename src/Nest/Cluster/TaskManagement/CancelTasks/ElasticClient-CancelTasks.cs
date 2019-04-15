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
		CancelTasksResponse CancelTasks(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null);

		/// <inheritdoc />
		CancelTasksResponse CancelTasks(ICancelTasksRequest request);

		/// <inheritdoc />
		Task<CancelTasksResponse> CancelTasksAsync(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<CancelTasksResponse> CancelTasksAsync(ICancelTasksRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CancelTasksResponse CancelTasks(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null) =>
			CancelTasks(selector.InvokeOrDefault(new CancelTasksDescriptor()));

		/// <inheritdoc />
		public CancelTasksResponse CancelTasks(ICancelTasksRequest request) =>
			DoRequest<ICancelTasksRequest, CancelTasksResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<CancelTasksResponse> CancelTasksAsync(Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null,
			CancellationToken ct = default
		) =>
			CancelTasksAsync(selector.InvokeOrDefault(new CancelTasksDescriptor()), ct);

		/// <inheritdoc />
		public Task<CancelTasksResponse> CancelTasksAsync(ICancelTasksRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ICancelTasksRequest, CancelTasksResponse, CancelTasksResponse>(request, request.RequestParameters, ct);
	}
}
