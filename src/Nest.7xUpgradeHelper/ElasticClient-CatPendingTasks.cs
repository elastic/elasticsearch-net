using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatPendingTasksRecord> CatPendingTasks(this IElasticClient client,Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatPendingTasksRecord> CatPendingTasks(this IElasticClient client,ICatPendingTasksRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(this IElasticClient client,
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(this IElasticClient client,ICatPendingTasksRequest request,
			CancellationToken ct = default
		);
	}

}
