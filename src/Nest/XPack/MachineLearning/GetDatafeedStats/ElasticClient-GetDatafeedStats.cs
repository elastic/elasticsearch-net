using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves configuration information for machine learning datafeeds.
		/// </summary>
		IGetDatafeedStatsResponse GetDatafeedStats(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null);

		/// <inheritdoc/>
		IGetDatafeedStatsResponse GetDatafeedStats(IGetDatafeedStatsRequest request);

		/// <inheritdoc/>
		Task<IGetDatafeedStatsResponse> GetDatafeedStatsAsync(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetDatafeedStatsResponse> GetDatafeedStatsAsync(IGetDatafeedStatsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetDatafeedStatsResponse GetDatafeedStats(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null) =>
			this.GetDatafeedStats(selector.InvokeOrDefault(new GetDatafeedStatsDescriptor()));

		/// <inheritdoc/>
		public IGetDatafeedStatsResponse GetDatafeedStats(IGetDatafeedStatsRequest request) =>
			this.Dispatcher.Dispatch<IGetDatafeedStatsRequest, GetDatafeedStatsRequestParameters, GetDatafeedStatsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlGetDatafeedStatsDispatch<GetDatafeedStatsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetDatafeedStatsResponse> GetDatafeedStatsAsync(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetDatafeedStatsAsync(selector.InvokeOrDefault(new GetDatafeedStatsDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetDatafeedStatsResponse> GetDatafeedStatsAsync(IGetDatafeedStatsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetDatafeedStatsRequest, GetDatafeedStatsRequestParameters, GetDatafeedStatsResponse, IGetDatafeedStatsResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlGetDatafeedStatsDispatchAsync<GetDatafeedStatsResponse>(p, c)
			);
	}
}
