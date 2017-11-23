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

		/// <inheritdoc/>
		IGetAnomalyRecordsResponse GetAnomalyRecords(IGetAnomalyRecordsRequest request);

		/// <inheritdoc/>
		Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(IGetAnomalyRecordsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetAnomalyRecordsResponse GetAnomalyRecords(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null) =>
			this.GetAnomalyRecords(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId)));

		/// <inheritdoc/>
		public IGetAnomalyRecordsResponse GetAnomalyRecords(IGetAnomalyRecordsRequest request) =>
			this.Dispatcher.Dispatch<IGetAnomalyRecordsRequest, GetAnomalyRecordsRequestParameters, GetAnomalyRecordsResponse>(
				request,
				this.LowLevelDispatch.XpackMlGetRecordsDispatch<GetAnomalyRecordsResponse>
			);

		/// <inheritdoc/>
		public Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetAnomalyRecordsAsync(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetAnomalyRecordsResponse> GetAnomalyRecordsAsync(IGetAnomalyRecordsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetAnomalyRecordsRequest, GetAnomalyRecordsRequestParameters, GetAnomalyRecordsResponse, IGetAnomalyRecordsResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlGetRecordsDispatchAsync<GetAnomalyRecordsResponse>
			);
	}
}
