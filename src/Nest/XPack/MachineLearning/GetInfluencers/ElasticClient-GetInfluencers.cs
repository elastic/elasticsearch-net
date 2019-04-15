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
		GetInfluencersResponse GetInfluencers(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null);

		/// <inheritdoc />
		GetInfluencersResponse GetInfluencers(IGetInfluencersRequest request);

		/// <inheritdoc />
		Task<GetInfluencersResponse> GetInfluencersAsync(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetInfluencersResponse> GetInfluencersAsync(IGetInfluencersRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetInfluencersResponse GetInfluencers(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null) =>
			GetInfluencers(selector.InvokeOrDefault(new GetInfluencersDescriptor(jobId)));

		/// <inheritdoc />
		public GetInfluencersResponse GetInfluencers(IGetInfluencersRequest request) =>
			DoRequest<IGetInfluencersRequest, GetInfluencersResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetInfluencersResponse> GetInfluencersAsync(
			Id jobId,
			Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null,
			CancellationToken ct = default
		) => GetInfluencersAsync(selector.InvokeOrDefault(new GetInfluencersDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<GetInfluencersResponse> GetInfluencersAsync(IGetInfluencersRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetInfluencersRequest, GetInfluencersResponse>(request, request.RequestParameters, ct);
	}
}
