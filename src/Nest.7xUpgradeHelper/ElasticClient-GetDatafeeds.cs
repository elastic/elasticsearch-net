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
public static GetDatafeedsResponse GetDatafeeds(this IElasticClient client, Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null
		)
			=> client.MachineLearning.GetDatafeeds(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetDatafeedsResponse GetDatafeeds(this IElasticClient client, IGetDatafeedsRequest request)
			=> client.MachineLearning.GetDatafeeds(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetDatafeedsResponse> GetDatafeedsAsync(this IElasticClient client,
			Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetDatafeedsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetDatafeedsResponse> GetDatafeedsAsync(this IElasticClient client, IGetDatafeedsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetDatafeedsAsync(request, ct);
	}
}
