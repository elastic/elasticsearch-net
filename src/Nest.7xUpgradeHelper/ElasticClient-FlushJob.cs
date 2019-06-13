using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Forces any buffered data to be processed by the machine learning job.
		/// </summary>
		public static FlushJobResponse FlushJob(this IElasticClient client,Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null);

		/// <inheritdoc />
		public static FlushJobResponse FlushJob(this IElasticClient client,IFlushJobRequest request);

		/// <inheritdoc />
		public static Task<FlushJobResponse> FlushJobAsync(this IElasticClient client,Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<FlushJobResponse> FlushJobAsync(this IElasticClient client,IFlushJobRequest request, CancellationToken ct = default);
	}

}
