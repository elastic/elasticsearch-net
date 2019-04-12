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
		IGetJobsResponse GetJobs(Func<GetJobsDescriptor, IGetJobsRequest> selector = null);

		/// <inheritdoc />
		IGetJobsResponse GetJobs(IGetJobsRequest request);

		/// <inheritdoc />
		Task<IGetJobsResponse> GetJobsAsync(Func<GetJobsDescriptor, IGetJobsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetJobsResponse> GetJobsAsync(IGetJobsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetJobsResponse GetJobs(Func<GetJobsDescriptor, IGetJobsRequest> selector = null) =>
			GetJobs(selector.InvokeOrDefault(new GetJobsDescriptor()));

		/// <inheritdoc />
		public IGetJobsResponse GetJobs(IGetJobsRequest request) =>
			DoRequest<IGetJobsRequest, GetJobsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetJobsResponse> GetJobsAsync(
			Func<GetJobsDescriptor, IGetJobsRequest> selector = null,
			CancellationToken ct = default
		) => GetJobsAsync(selector.InvokeOrDefault(new GetJobsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetJobsResponse> GetJobsAsync(IGetJobsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetJobsRequest, IGetJobsResponse, GetJobsResponse>(request, request.RequestParameters, ct);
	}
}
