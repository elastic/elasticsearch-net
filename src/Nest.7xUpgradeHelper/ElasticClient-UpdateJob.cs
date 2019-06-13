using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Updates a machine learning job.
		/// </summary>
		public static UpdateJobResponse UpdateJob<T>(this IElasticClient client,Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null) where T : class;

		/// <inheritdoc />
		public static UpdateJobResponse UpdateJob(this IElasticClient client,IUpdateJobRequest request);

		/// <inheritdoc />
		public static Task<UpdateJobResponse> UpdateJobAsync<T>(this IElasticClient client,Id jobId, Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		public static Task<UpdateJobResponse> UpdateJobAsync(this IElasticClient client,IUpdateJobRequest request, CancellationToken ct = default);
	}

}
