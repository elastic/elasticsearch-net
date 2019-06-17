using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Validates a machine learning job
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ValidateJobResponse ValidateJob<T>(this IElasticClient client, Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector)
			where T : class
			=> client.MachineLearning.ValidateJob(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ValidateJobResponse ValidateJob(this IElasticClient client, IValidateJobRequest request)
			=> client.MachineLearning.ValidateJob(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ValidateJobResponse> ValidateJobAsync<T>(this IElasticClient client,
			Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.ValidateJobAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ValidateJobResponse> ValidateJobAsync(this IElasticClient client, IValidateJobRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.ValidateJobAsync(request, ct);
	}
}
