using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Start a machine learning datafeed.
		/// A datafeed must be started in order to retrieve data from Elasticsearch. A datafeed can be started and stopped multiple
		/// times throughout
		/// its lifecycle.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartDatafeedResponse StartDatafeed(this IElasticClient client, Id datafeedId,
			Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null
		)
			=> client.MachineLearning.StartDatafeed(datafeedId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartDatafeedResponse StartDatafeed(this IElasticClient client, IStartDatafeedRequest request)
			=> client.MachineLearning.StartDatafeed(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartDatafeedResponse> StartDatafeedAsync(this IElasticClient client, Id datafeedId,
			Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.StartDatafeedAsync(datafeedId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartDatafeedResponse> StartDatafeedAsync(this IElasticClient client, IStartDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.StartDatafeedAsync(request, ct);
	}
}
