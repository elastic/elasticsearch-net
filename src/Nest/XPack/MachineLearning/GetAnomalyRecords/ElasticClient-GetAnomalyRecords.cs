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
		IGetAnomalyRecordsResponse GetAnomalyRecords(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null);

		/// <inheritdoc />
		IGetAnomalyRecordsResponse GetAnomalyRecords(IGetAnomalyRecordsRequest request);

		/// <inheritdoc />
		Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(IGetAnomalyRecordsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetAnomalyRecordsResponse GetAnomalyRecords(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null) =>
			GetAnomalyRecords(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId)));

		/// <inheritdoc />
		public IGetAnomalyRecordsResponse GetAnomalyRecords(IGetAnomalyRecordsRequest request) =>
			Dispatch2<IGetAnomalyRecordsRequest, GetAnomalyRecordsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(
			Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null,
			CancellationToken ct = default
		) => GetAnomalyRecordsAsync(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(IGetAnomalyRecordsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetAnomalyRecordsRequest, IGetAnomalyRecordsResponse, GetAnomalyRecordsResponse>(request, request.RequestParameters, ct);
	}
}
