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

		/// <inheritdoc/>
		IGetJobStatsResponse GetJobStats(IGetJobStatsRequest request);

		/// <inheritdoc/>
		Task<IGetJobStatsResponse> GetJobStatsAsync(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetJobStatsResponse> GetJobStatsAsync(IGetJobStatsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetJobStatsResponse GetJobStats(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null) =>
			this.GetJobStats(selector.InvokeOrDefault(new GetJobStatsDescriptor()));

		/// <inheritdoc/>
		public IGetJobStatsResponse GetJobStats(IGetJobStatsRequest request) =>
			this.Dispatcher.Dispatch<IGetJobStatsRequest, GetJobStatsRequestParameters, GetJobStatsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlGetJobStatsDispatch<GetJobStatsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetJobStatsResponse> GetJobStatsAsync(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetJobStatsAsync(selector.InvokeOrDefault(new GetJobStatsDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetJobStatsResponse> GetJobStatsAsync(IGetJobStatsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetJobStatsRequest, GetJobStatsRequestParameters, GetJobStatsResponse, IGetJobStatsResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlGetJobStatsDispatchAsync<GetJobStatsResponse>(p, c)
			);
	}
}
