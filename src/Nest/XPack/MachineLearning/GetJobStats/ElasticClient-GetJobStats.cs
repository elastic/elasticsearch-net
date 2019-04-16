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
		IGetJobStatsResponse GetJobStats(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null);

		/// <inheritdoc />
		IGetJobStatsResponse GetJobStats(IGetJobStatsRequest request);

		/// <inheritdoc />
		Task<IGetJobStatsResponse> GetJobStatsAsync(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetJobStatsResponse> GetJobStatsAsync(IGetJobStatsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetJobStatsResponse GetJobStats(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null) =>
			GetJobStats(selector.InvokeOrDefault(new GetJobStatsDescriptor()));

		/// <inheritdoc />
		public IGetJobStatsResponse GetJobStats(IGetJobStatsRequest request) =>
			DoRequest<IGetJobStatsRequest, GetJobStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetJobStatsResponse> GetJobStatsAsync(
			Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null,
			CancellationToken ct = default
		) => GetJobStatsAsync(selector.InvokeOrDefault(new GetJobStatsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetJobStatsResponse> GetJobStatsAsync(IGetJobStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetJobStatsRequest, IGetJobStatsResponse, GetJobStatsResponse>(request, request.RequestParameters, ct);
	}
}
