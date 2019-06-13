using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a machine learning job.
		/// </summary>
		public static PutJobResponse PutJob<T>(this IElasticClient client,Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector) where T : class;

		/// <inheritdoc />
		public static PutJobResponse PutJob(this IElasticClient client,IPutJobRequest request);

		/// <inheritdoc />
		public static Task<PutJobResponse> PutJobAsync<T>(this IElasticClient client,Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		public static Task<PutJobResponse> PutJobAsync(this IElasticClient client,IPutJobRequest request, CancellationToken ct = default);
	}

}
