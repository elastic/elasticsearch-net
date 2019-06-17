using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes expired machine learning data.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteExpiredDataResponse DeleteExpiredData(this IElasticClient client,
			Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null
		)
			=> client.MachineLearning.DeleteExpiredData(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteExpiredDataResponse DeleteExpiredData(this IElasticClient client, IDeleteExpiredDataRequest request)
			=> client.MachineLearning.DeleteExpiredData(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(this IElasticClient client,
			Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteExpiredDataAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(this IElasticClient client, IDeleteExpiredDataRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteExpiredDataAsync(request);
	}
}
