using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetAnomalyRecords(), please update this usage.")]
		public static GetAnomalyRecordsResponse GetAnomalyRecords(this IElasticClient client, Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null
		)
			=> client.MachineLearning.GetAnomalyRecords(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.GetAnomalyRecords(), please update this usage.")]
		public static GetAnomalyRecordsResponse GetAnomalyRecords(this IElasticClient client, IGetAnomalyRecordsRequest request)
			=> client.MachineLearning.GetAnomalyRecords(request);

		[Obsolete("Moved to client.MachineLearning.GetAnomalyRecordsAsync(), please update this usage.")]
		public static Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(this IElasticClient client, Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetAnomalyRecordsAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetAnomalyRecordsAsync(), please update this usage.")]
		public static Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(this IElasticClient client, IGetAnomalyRecordsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetAnomalyRecordsAsync(request, ct);
	}
}
