using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Validates a detector for a machine learning job
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ValidateDetectorResponse ValidateDetector<T>(this IElasticClient client,
			Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector
		) where T : class
			=> client.MachineLearning.ValidateDetector(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ValidateDetectorResponse ValidateDetector(this IElasticClient client, IValidateDetectorRequest request)
			=> client.MachineLearning.ValidateDetector(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ValidateDetectorResponse> ValidateDetectorAsync<T>(this IElasticClient client,
			Func<ValidateDetectorDescriptor<T>, IValidateDetectorRequest> selector,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.ValidateDetectorAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ValidateDetectorResponse> ValidateDetectorAsync(this IElasticClient client, IValidateDetectorRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.ValidateDetectorAsync(request, ct);
	}
}
