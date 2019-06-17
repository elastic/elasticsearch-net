using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a machine learning datafeed.
		/// You must create a job before you create a datafeed. You can associate only one datafeed to each job.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutDatafeedResponse PutDatafeed<T>(this IElasticClient client, Id datafeedId,
			Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null
		) where T : class
			=> client.MachineLearning.PutDatafeed(datafeedId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutDatafeedResponse PutDatafeed(this IElasticClient client, IPutDatafeedRequest request)
			=> client.MachineLearning.PutDatafeed(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutDatafeedResponse> PutDatafeedAsync<T>(this IElasticClient client, Id datafeedId,
			Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.PutDatafeedAsync(datafeedId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutDatafeedResponse> PutDatafeedAsync(this IElasticClient client, IPutDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutDatafeedAsync(request, ct);
	}
}
