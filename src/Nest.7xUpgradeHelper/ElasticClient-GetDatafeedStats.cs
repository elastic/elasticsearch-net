using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves configuration information for machine learning datafeeds.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetDatafeedStatsResponse GetDatafeedStats(this IElasticClient client,
			Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null
		)
			=> client.MachineLearning.GetDatafeedStats(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetDatafeedStatsResponse GetDatafeedStats(this IElasticClient client, IGetDatafeedStatsRequest request)
			=> client.MachineLearning.GetDatafeedStats(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(this IElasticClient client,
			Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetDatafeedStatsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(this IElasticClient client, IGetDatafeedStatsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetDatafeedStatsAsync(request, ct);
	}
}
