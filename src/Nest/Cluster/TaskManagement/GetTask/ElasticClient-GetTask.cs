using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IGetTaskResponse GetTask(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null);

		IGetTaskResponse GetTask(IGetTaskRequest request);

		Task<IGetTaskResponse> GetTaskAsync(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null,
			CancellationToken ct = default
		);

		Task<IGetTaskResponse> GetTaskAsync(IGetTaskRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		public IGetTaskResponse GetTask(TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null) =>
			GetTask(selector.InvokeOrDefault(new GetTaskDescriptor(id)));

		public IGetTaskResponse GetTask(IGetTaskRequest request) =>
			DoRequest<IGetTaskRequest, GetTaskResponse>(request, request.RequestParameters);

		public Task<IGetTaskResponse> GetTaskAsync(
			TaskId id,
			Func<GetTaskDescriptor, IGetTaskRequest> selector = null,
			CancellationToken ct = default
		) => GetTaskAsync(selector.InvokeOrDefault(new GetTaskDescriptor(id)), ct);

		public Task<IGetTaskResponse> GetTaskAsync(IGetTaskRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetTaskRequest, IGetTaskResponse, GetTaskResponse>(request, request.RequestParameters, ct);
	}
}
