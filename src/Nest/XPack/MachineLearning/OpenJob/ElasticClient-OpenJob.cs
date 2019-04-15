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
		OpenJobResponse OpenJob(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null);

		/// <inheritdoc />
		OpenJobResponse OpenJob(IOpenJobRequest request);

		/// <inheritdoc />
		Task<OpenJobResponse> OpenJobAsync(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<OpenJobResponse> OpenJobAsync(IOpenJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public OpenJobResponse OpenJob(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null) =>
			OpenJob(selector.InvokeOrDefault(new OpenJobDescriptor(jobId)));

		/// <inheritdoc />
		public OpenJobResponse OpenJob(IOpenJobRequest request) =>
			DoRequest<IOpenJobRequest, OpenJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<OpenJobResponse> OpenJobAsync(
			Id jobId,
			Func<OpenJobDescriptor, IOpenJobRequest> selector = null,
			CancellationToken ct = default
		) => OpenJobAsync(selector.InvokeOrDefault(new OpenJobDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<OpenJobResponse> OpenJobAsync(IOpenJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IOpenJobRequest, OpenJobResponse, OpenJobResponse>(request, request.RequestParameters, ct);
	}
}
