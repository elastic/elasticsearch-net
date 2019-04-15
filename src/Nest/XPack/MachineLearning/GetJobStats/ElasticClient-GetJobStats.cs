using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves results for machine learning job influencers.
		/// </summary>
		GetJobStatsResponse GetJobStats(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null);

		/// <inheritdoc />
		GetJobStatsResponse GetJobStats(IGetJobStatsRequest request);

		/// <inheritdoc />
		Task<GetJobStatsResponse> GetJobStatsAsync(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetJobStatsResponse> GetJobStatsAsync(IGetJobStatsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetJobStatsResponse GetJobStats(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null) =>
			GetJobStats(selector.InvokeOrDefault(new GetJobStatsDescriptor()));

		/// <inheritdoc />
		public GetJobStatsResponse GetJobStats(IGetJobStatsRequest request) =>
			DoRequest<IGetJobStatsRequest, GetJobStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetJobStatsResponse> GetJobStatsAsync(
			Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null,
			CancellationToken ct = default
		) => GetJobStatsAsync(selector.InvokeOrDefault(new GetJobStatsDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetJobStatsResponse> GetJobStatsAsync(IGetJobStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetJobStatsRequest, GetJobStatsResponse, GetJobStatsResponse>(request, request.RequestParameters, ct);
	}
}
