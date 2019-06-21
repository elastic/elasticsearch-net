using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.PutJob(), please update this usage.")]
		public static PutJobResponse PutJob<T>(this IElasticClient client, Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector)
			where T : class
			=> client.MachineLearning.PutJob(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.PutJob(), please update this usage.")]
		public static PutJobResponse PutJob(this IElasticClient client, IPutJobRequest request)
			=> client.MachineLearning.PutJob(request);

		[Obsolete("Moved to client.MachineLearning.PutJobAsync(), please update this usage.")]
		public static Task<PutJobResponse> PutJobAsync<T>(this IElasticClient client, Id jobId, Func<PutJobDescriptor<T>, IPutJobRequest> selector,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.PutJobAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.PutJobAsync(), please update this usage.")]
		public static Task<PutJobResponse> PutJobAsync(this IElasticClient client, IPutJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.PutJobAsync(request, ct);
	}
}
