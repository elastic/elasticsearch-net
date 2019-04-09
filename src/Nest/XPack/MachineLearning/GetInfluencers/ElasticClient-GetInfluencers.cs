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
		IGetInfluencersResponse GetInfluencers(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null);

		/// <inheritdoc />
		IGetInfluencersResponse GetInfluencers(IGetInfluencersRequest request);

		/// <inheritdoc />
		Task<IGetInfluencersResponse> GetInfluencersAsync(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetInfluencersResponse> GetInfluencersAsync(IGetInfluencersRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetInfluencersResponse GetInfluencers(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null) =>
			GetInfluencers(selector.InvokeOrDefault(new GetInfluencersDescriptor(jobId)));

		/// <inheritdoc />
		public IGetInfluencersResponse GetInfluencers(IGetInfluencersRequest request) =>
			Dispatch2<IGetInfluencersRequest, GetInfluencersResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetInfluencersResponse> GetInfluencersAsync(
			Id jobId,
			Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null,
			CancellationToken ct = default
		) => GetInfluencersAsync(selector.InvokeOrDefault(new GetInfluencersDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<IGetInfluencersResponse> GetInfluencersAsync(IGetInfluencersRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetInfluencersRequest, IGetInfluencersResponse, GetInfluencersResponse>(request, request.RequestParameters, ct);
	}
}
