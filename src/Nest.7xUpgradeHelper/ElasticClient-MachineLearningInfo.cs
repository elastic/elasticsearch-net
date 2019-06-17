using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Returns defaults and limits used by machine learning.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static MachineLearningInfoResponse MachineLearningInfo(this IElasticClient client,
			Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null
		)
			=> client.MachineLearning.Info(selector);

		/// <inheritdoc
		///     cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static MachineLearningInfoResponse MachineLearningInfo(this IElasticClient client, IMachineLearningInfoRequest request)
			=> client.MachineLearning.Info(request);

		/// <inheritdoc
		///     cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<MachineLearningInfoResponse> MachineLearningInfoAsync(this IElasticClient client,
			Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.InfoAsync(selector, ct);

		/// <inheritdoc
		///     cref="MachineLearningInfo(System.Func{Nest.MachineLearningInfoDescriptor,Nest.IMachineLearningInfoRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<MachineLearningInfoResponse> MachineLearningInfoAsync(this IElasticClient client, IMachineLearningInfoRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.InfoAsync(request, ct);
	}
}
