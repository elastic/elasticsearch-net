using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieve anomaly records for a machine learning job.
		/// </summary>
		public static GetAnomalyRecordsResponse GetAnomalyRecords(this IElasticClient client,Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null);

		/// <inheritdoc />
		public static GetAnomalyRecordsResponse GetAnomalyRecords(this IElasticClient client,IGetAnomalyRecordsRequest request);

		/// <inheritdoc />
		public static Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(this IElasticClient client,Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(this IElasticClient client,IGetAnomalyRecordsRequest request,
			CancellationToken ct = default
		);
	}

}
