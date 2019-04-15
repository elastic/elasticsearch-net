using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Sends data to a machine learning job for analysis.
		/// </summary>
		PostJobDataResponse PostJobData(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector);

		/// <inheritdoc />
		PostJobDataResponse PostJobData(IPostJobDataRequest request);

		/// <inheritdoc />
		Task<PostJobDataResponse> PostJobDataAsync(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<PostJobDataResponse> PostJobDataAsync(IPostJobDataRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PostJobDataResponse PostJobData(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector) =>
			PostJobData(selector.InvokeOrDefault(new PostJobDataDescriptor(jobId)));

		/// <inheritdoc />
		public PostJobDataResponse PostJobData(IPostJobDataRequest request) =>
			DoRequest<IPostJobDataRequest, PostJobDataResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PostJobDataResponse> PostJobDataAsync(
			Id jobId,
			Func<PostJobDataDescriptor, IPostJobDataRequest> selector,
			CancellationToken ct = default
		) => PostJobDataAsync(selector.InvokeOrDefault(new PostJobDataDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<PostJobDataResponse> PostJobDataAsync(IPostJobDataRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPostJobDataRequest, PostJobDataResponse>
				(request, request.RequestParameters, ct);
	}
}
