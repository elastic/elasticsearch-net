using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes an existing datafeed for a machine learning job.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteDatafeedResponse DeleteDatafeed(this IElasticClient client, Id datafeedId,
			Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null
		)
			=> client.MachineLearning.DeleteDatafeed(datafeedId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteDatafeedResponse DeleteDatafeed(this IElasticClient client, IDeleteDatafeedRequest request)
			=> client.MachineLearning.DeleteDatafeed(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteDatafeedResponse> DeleteDatafeedAsync(this IElasticClient client, Id datafeedId,
			Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteDatafeedAsync(datafeedId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteDatafeedResponse> DeleteDatafeedAsync(this IElasticClient client, IDeleteDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteDatafeedAsync(request, ct);
	}
}
