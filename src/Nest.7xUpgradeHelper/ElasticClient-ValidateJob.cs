using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.ValidateJob(), please update this usage.")]
		public static ValidateJobResponse ValidateJob<T>(this IElasticClient client, Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector)
			where T : class
			=> client.MachineLearning.ValidateJob(selector);

		[Obsolete("Moved to client.MachineLearning.ValidateJob(), please update this usage.")]
		public static ValidateJobResponse ValidateJob(this IElasticClient client, IValidateJobRequest request)
			=> client.MachineLearning.ValidateJob(request);

		[Obsolete("Moved to client.MachineLearning.ValidateJobAsync(), please update this usage.")]
		public static Task<ValidateJobResponse> ValidateJobAsync<T>(this IElasticClient client,
			Func<ValidateJobDescriptor<T>, IValidateJobRequest> selector,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.ValidateJobAsync(selector, ct);

		[Obsolete("Moved to client.MachineLearning.ValidateJobAsync(), please update this usage.")]
		public static Task<ValidateJobResponse> ValidateJobAsync(this IElasticClient client, IValidateJobRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.ValidateJobAsync(request, ct);
	}
}
