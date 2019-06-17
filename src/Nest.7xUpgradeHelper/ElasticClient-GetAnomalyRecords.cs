using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieve anomaly records for a machine learning job.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetAnomalyRecordsResponse GetAnomalyRecords(this IElasticClient client, Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null
		)
			=> client.MachineLearning.GetAnomalyRecords(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetAnomalyRecordsResponse GetAnomalyRecords(this IElasticClient client, IGetAnomalyRecordsRequest request)
			=> client.MachineLearning.GetAnomalyRecords(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(this IElasticClient client, Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetAnomalyRecordsAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(this IElasticClient client, IGetAnomalyRecordsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetAnomalyRecordsAsync(request, ct);
	}
}
