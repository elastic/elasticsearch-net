using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves machine learning job configuration information
		/// </summary>
		GetJobsResponse GetJobs(Func<GetJobsDescriptor, IGetJobsRequest> selector = null);

		/// <inheritdoc />
		GetJobsResponse GetJobs(IGetJobsRequest request);

		/// <inheritdoc />
		Task<GetJobsResponse> GetJobsAsync(Func<GetJobsDescriptor, IGetJobsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetJobsResponse> GetJobsAsync(IGetJobsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetJobsResponse GetJobs(Func<GetJobsDescriptor, IGetJobsRequest> selector = null) =>
			GetJobs(selector.InvokeOrDefault(new GetJobsDescriptor()));

		/// <inheritdoc />
		public GetJobsResponse GetJobs(IGetJobsRequest request) =>
			DoRequest<IGetJobsRequest, GetJobsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetJobsResponse> GetJobsAsync(
			Func<GetJobsDescriptor, IGetJobsRequest> selector = null,
			CancellationToken ct = default
		) => GetJobsAsync(selector.InvokeOrDefault(new GetJobsDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetJobsResponse> GetJobsAsync(IGetJobsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetJobsRequest, GetJobsResponse, GetJobsResponse>(request, request.RequestParameters, ct);
	}
}
