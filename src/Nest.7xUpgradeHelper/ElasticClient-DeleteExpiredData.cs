using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.DeleteExpiredData(), please update this usage.")]
		public static DeleteExpiredDataResponse DeleteExpiredData(this IElasticClient client,
			Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null
		)
			=> client.MachineLearning.DeleteExpiredData(selector);

		[Obsolete("Moved to client.MachineLearning.DeleteExpiredData(), please update this usage.")]
		public static DeleteExpiredDataResponse DeleteExpiredData(this IElasticClient client, IDeleteExpiredDataRequest request)
			=> client.MachineLearning.DeleteExpiredData(request);

		[Obsolete("Moved to client.MachineLearning.DeleteExpiredDataAsync(), please update this usage.")]
		public static Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(this IElasticClient client,
			Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteExpiredDataAsync(selector, ct);

		[Obsolete("Moved to client.MachineLearning.DeleteExpiredDataAsync(), please update this usage.")]
		public static Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(this IElasticClient client, IDeleteExpiredDataRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteExpiredDataAsync(request, ct);
	}
}
