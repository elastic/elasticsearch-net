using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.Info(), please update this usage.")]
		public static MachineLearningInfoResponse MachineLearningInfo(this IElasticClient client,
			Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null
		)
			=> client.MachineLearning.Info(selector);

		[Obsolete("Moved to client.MachineLearning.Info(), please update this usage.")]
		public static MachineLearningInfoResponse MachineLearningInfo(this IElasticClient client, IMachineLearningInfoRequest request)
			=> client.MachineLearning.Info(request);

		[Obsolete("Moved to client.MachineLearning.InfoAsync(), please update this usage.")]
		public static Task<MachineLearningInfoResponse> MachineLearningInfoAsync(this IElasticClient client,
			Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.InfoAsync(selector, ct);

		[Obsolete("Moved to client.MachineLearning.InfoAsync(), please update this usage.")]
		public static Task<MachineLearningInfoResponse> MachineLearningInfoAsync(this IElasticClient client, IMachineLearningInfoRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.InfoAsync(request, ct);
	}
}
