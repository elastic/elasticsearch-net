using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Closes a machine learning job.
		/// A closed job cannot receive data or perform analysis operations, but you can still explore and navigate results.
		/// </summary>
		CloseJobResponse CloseJob(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null);

		/// <inheritdoc />
		CloseJobResponse CloseJob(ICloseJobRequest request);

		/// <inheritdoc />
		Task<CloseJobResponse> CloseJobAsync(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<CloseJobResponse> CloseJobAsync(ICloseJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CloseJobResponse CloseJob(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null) =>
			CloseJob(selector.InvokeOrDefault(new CloseJobDescriptor(jobId)));

		/// <inheritdoc />
		public CloseJobResponse CloseJob(ICloseJobRequest request) =>
			DoRequest<ICloseJobRequest, CloseJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<CloseJobResponse> CloseJobAsync(
			Id jobId,
			Func<CloseJobDescriptor, ICloseJobRequest> selector = null,
			CancellationToken ct = default
		) => CloseJobAsync(selector.InvokeOrDefault(new CloseJobDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<CloseJobResponse> CloseJobAsync(ICloseJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ICloseJobRequest, CloseJobResponse>(request, request.RequestParameters, ct);
	}
}
