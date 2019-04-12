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
		IPostJobDataResponse PostJobData(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector);

		/// <inheritdoc />
		IPostJobDataResponse PostJobData(IPostJobDataRequest request);

		/// <inheritdoc />
		Task<IPostJobDataResponse> PostJobDataAsync(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IPostJobDataResponse> PostJobDataAsync(IPostJobDataRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPostJobDataResponse PostJobData(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector) =>
			PostJobData(selector.InvokeOrDefault(new PostJobDataDescriptor(jobId)));

		/// <inheritdoc />
		public IPostJobDataResponse PostJobData(IPostJobDataRequest request) =>
			DoRequest<IPostJobDataRequest, PostJobDataResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPostJobDataResponse> PostJobDataAsync(
			Id jobId,
			Func<PostJobDataDescriptor, IPostJobDataRequest> selector,
			CancellationToken ct = default
		) => PostJobDataAsync(selector.InvokeOrDefault(new PostJobDataDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<IPostJobDataResponse> PostJobDataAsync(IPostJobDataRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPostJobDataRequest, IPostJobDataResponse, PostJobDataResponse>
				(request, request.RequestParameters, ct);
	}
}
