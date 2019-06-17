using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Updates a machine learning job.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpdateJobResponse UpdateJob<T>(this IElasticClient client, Id jobId,
			Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null
		) where T : class
			=> client.MachineLearning.UpdateJob(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpdateJobResponse UpdateJob(this IElasticClient client, IUpdateJobRequest request)
			=> client.MachineLearning.UpdateJob(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpdateJobResponse> UpdateJobAsync<T>(this IElasticClient client, Id jobId,
			Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.UpdateJobAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpdateJobResponse> UpdateJobAsync(this IElasticClient client, IUpdateJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.UpdateJobAsync(request, ct);
	}
}
