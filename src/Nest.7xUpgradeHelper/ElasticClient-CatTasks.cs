using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatTasksRecord> CatTasks(this IElasticClient client,Func<CatTasksDescriptor, ICatTasksRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatTasksRecord> CatTasks(this IElasticClient client,ICatTasksRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatTasksRecord>> CatTasksAsync(this IElasticClient client,
			Func<CatTasksDescriptor, ICatTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatTasksRecord>> CatTasksAsync(this IElasticClient client,ICatTasksRequest request, CancellationToken ct = default);
	}

}
