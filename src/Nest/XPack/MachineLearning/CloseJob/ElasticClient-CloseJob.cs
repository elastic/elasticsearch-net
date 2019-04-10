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
		ICloseJobResponse CloseJob(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null);

		/// <inheritdoc />
		ICloseJobResponse CloseJob(ICloseJobRequest request);

		/// <inheritdoc />
		Task<ICloseJobResponse> CloseJobAsync(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ICloseJobResponse> CloseJobAsync(ICloseJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICloseJobResponse CloseJob(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null) =>
			CloseJob(selector.InvokeOrDefault(new CloseJobDescriptor(jobId)));

		/// <inheritdoc />
		public ICloseJobResponse CloseJob(ICloseJobRequest request) =>
			DoRequest<ICloseJobRequest, CloseJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ICloseJobResponse> CloseJobAsync(
			Id jobId,
			Func<CloseJobDescriptor, ICloseJobRequest> selector = null,
			CancellationToken ct = default
		) => CloseJobAsync(selector.InvokeOrDefault(new CloseJobDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<ICloseJobResponse> CloseJobAsync(ICloseJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ICloseJobRequest, ICloseJobResponse, CloseJobResponse>(request, request.RequestParameters, ct);
	}
}
