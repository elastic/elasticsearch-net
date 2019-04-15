using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		GetTaskResponse GetTask(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null);

		GetTaskResponse GetTask(IGetTaskRequest request);

		Task<GetTaskResponse> GetTaskAsync(
			TaskId id,
			Func<GetTaskDescriptor, IGetTaskRequest> selector = null,
			CancellationToken ct = default
		);

		Task<GetTaskResponse> GetTaskAsync(IGetTaskRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		public GetTaskResponse GetTask(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null) =>
			GetTask(selector.InvokeOrDefault(new GetTaskDescriptor(id)));

		public GetTaskResponse GetTask(IGetTaskRequest request) =>
			DoRequest<IGetTaskRequest, GetTaskResponse>(request, request.RequestParameters);

		public Task<GetTaskResponse> GetTaskAsync(
			TaskId id,
			Func<GetTaskDescriptor, IGetTaskRequest> selector = null,
			CancellationToken ct = default
		) => GetTaskAsync(selector.InvokeOrDefault(new GetTaskDescriptor(id)), ct);

		public Task<GetTaskResponse> GetTaskAsync(IGetTaskRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetTaskRequest, GetTaskResponse>(request, request.RequestParameters, ct);
	}
}
