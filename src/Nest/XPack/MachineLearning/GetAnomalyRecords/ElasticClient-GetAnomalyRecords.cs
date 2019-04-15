using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieve anomaly records for a machine learning job.
		/// </summary>
		GetAnomalyRecordsResponse GetAnomalyRecords(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null);

		/// <inheritdoc />
		GetAnomalyRecordsResponse GetAnomalyRecords(IGetAnomalyRecordsRequest request);

		/// <inheritdoc />
		Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(IGetAnomalyRecordsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetAnomalyRecordsResponse GetAnomalyRecords(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null) =>
			GetAnomalyRecords(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId)));

		/// <inheritdoc />
		public GetAnomalyRecordsResponse GetAnomalyRecords(IGetAnomalyRecordsRequest request) =>
			DoRequest<IGetAnomalyRecordsRequest, GetAnomalyRecordsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(
			Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null,
			CancellationToken ct = default
		) => GetAnomalyRecordsAsync(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(IGetAnomalyRecordsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetAnomalyRecordsRequest, GetAnomalyRecordsResponse>(request, request.RequestParameters, ct);
	}
}
