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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(IGetAnomalyRecordsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetAnomalyRecordsResponse GetAnomalyRecords(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null) =>
			GetAnomalyRecords(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId)));

		/// <inheritdoc />
		public IGetAnomalyRecordsResponse GetAnomalyRecords(IGetAnomalyRecordsRequest request) =>
			Dispatcher.Dispatch<IGetAnomalyRecordsRequest, GetAnomalyRecordsRequestParameters, GetAnomalyRecordsResponse>(
				request,
				LowLevelDispatch.MlGetRecordsDispatch<GetAnomalyRecordsResponse>
			);

		/// <inheritdoc />
		public Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(Id jobId,
			Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetAnomalyRecordsAsync(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(IGetAnomalyRecordsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IGetAnomalyRecordsRequest, GetAnomalyRecordsRequestParameters, GetAnomalyRecordsResponse, IGetAnomalyRecordsResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.MlGetRecordsDispatchAsync<GetAnomalyRecordsResponse>
				);
	}
}
