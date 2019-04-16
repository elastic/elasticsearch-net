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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetInfluencersResponse> GetInfluencersAsync(IGetInfluencersRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetInfluencersResponse GetInfluencers(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null) =>
			GetInfluencers(selector.InvokeOrDefault(new GetInfluencersDescriptor(jobId)));

		/// <inheritdoc />
		public IGetInfluencersResponse GetInfluencers(IGetInfluencersRequest request) =>
			Dispatcher.Dispatch<IGetInfluencersRequest, GetInfluencersRequestParameters, GetInfluencersResponse>(
				request,
				LowLevelDispatch.MlGetInfluencersDispatch<GetInfluencersResponse>
			);

		/// <inheritdoc />
		public Task<IGetInfluencersResponse> GetInfluencersAsync(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetInfluencersAsync(selector.InvokeOrDefault(new GetInfluencersDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetInfluencersResponse> GetInfluencersAsync(IGetInfluencersRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetInfluencersRequest, GetInfluencersRequestParameters, GetInfluencersResponse, IGetInfluencersResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.MlGetInfluencersDispatchAsync<GetInfluencersResponse>
			);
	}
}
