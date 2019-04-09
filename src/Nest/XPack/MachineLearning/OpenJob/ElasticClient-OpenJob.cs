using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Opens a machine learning job.
		/// A job must be opened in order for it to be ready to receive and analyze data.
		/// A job can be opened and closed multiple times throughout its lifecycle.
		/// </summary>
		IOpenJobResponse OpenJob(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null);

		/// <inheritdoc />
		IOpenJobResponse OpenJob(IOpenJobRequest request);

		/// <inheritdoc />
		Task<IOpenJobResponse> OpenJobAsync(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IOpenJobResponse> OpenJobAsync(IOpenJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IOpenJobResponse OpenJob(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null) =>
			OpenJob(selector.InvokeOrDefault(new OpenJobDescriptor(jobId)));

		/// <inheritdoc />
		public IOpenJobResponse OpenJob(IOpenJobRequest request) =>
			Dispatch2<IOpenJobRequest, OpenJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IOpenJobResponse> OpenJobAsync(
			Id jobId,
			Func<OpenJobDescriptor, IOpenJobRequest> selector = null,
			CancellationToken ct = default
		) => OpenJobAsync(selector.InvokeOrDefault(new OpenJobDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<IOpenJobResponse> OpenJobAsync(IOpenJobRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IOpenJobRequest, IOpenJobResponse, OpenJobResponse>(request, request.RequestParameters, ct);
	}
}
