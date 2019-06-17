using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Update a machine learning datafeed.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpdateDatafeedResponse UpdateDatafeed<T>(this IElasticClient client, Id datafeedId,
			Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null
		)
			where T : class
			=> client.MachineLearning.UpdateDatafeed(datafeedId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpdateDatafeedResponse UpdateDatafeed(this IElasticClient client, IUpdateDatafeedRequest request)
			=> client.MachineLearning.UpdateDatafeed(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpdateDatafeedResponse> UpdateDatafeedAsync<T>(this IElasticClient client, Id datafeedId,
			Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.UpdateDatafeedAsync(datafeedId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpdateDatafeedResponse> UpdateDatafeedAsync(this IElasticClient client, IUpdateDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.UpdateDatafeedAsync(request, ct);
	}
}
