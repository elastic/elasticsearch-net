using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.ValidateDetector(), please update this usage.")]
		public static ValidateDetectorResponse ValidateDetector<T>(this IElasticClient client,
			Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector
		) where T : class
			=> client.MachineLearning.ValidateDetector(selector);

		[Obsolete("Moved to client.MachineLearning.ValidateDetector(), please update this usage.")]
		public static ValidateDetectorResponse ValidateDetector(this IElasticClient client, IValidateDetectorRequest request)
			=> client.MachineLearning.ValidateDetector(request);

		[Obsolete("Moved to client.MachineLearning.ValidateDetectorAsync(), please update this usage.")]
		public static Task<ValidateDetectorResponse> ValidateDetectorAsync<T>(this IElasticClient client,
			Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.ValidateDetectorAsync(selector, ct);

		[Obsolete("Moved to client.MachineLearning.ValidateDetectorAsync(), please update this usage.")]
		public static Task<ValidateDetectorResponse> ValidateDetectorAsync(this IElasticClient client, IValidateDetectorRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.ValidateDetectorAsync(request, ct);
	}
}
