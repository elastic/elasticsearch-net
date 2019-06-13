using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		public static GetTaskResponse GetTask(this IElasticClient client,TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null);

		public static GetTaskResponse GetTask(this IElasticClient client,IGetTaskRequest request);

		public static Task<GetTaskResponse> GetTaskAsync(this IElasticClient client,
			TaskId id,
			Func<GetTaskDescriptor, IGetTaskRequest> selector = null,
			CancellationToken ct = default
		);

		public static Task<GetTaskResponse> GetTaskAsync(this IElasticClient client,IGetTaskRequest request, CancellationToken ct = default);
	}

}
