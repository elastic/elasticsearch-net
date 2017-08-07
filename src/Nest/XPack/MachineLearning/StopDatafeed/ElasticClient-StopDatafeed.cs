using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Stop a Machine Learning data feed.
		/// A datafeed that is stopped ceases to retrieve data from Elasticsearch. A datafeed can be started and stopped multiple times throughout its lifecycle.
		/// </summary>
		IStopDatafeedResponse StopDatafeed(Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null);

		/// <inheritdoc/>
		IStopDatafeedResponse StopDatafeed(IStopDatafeedRequest request);

		/// <inheritdoc/>
		Task<IStopDatafeedResponse> StopDatafeedAsync(Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IStopDatafeedResponse> StopDatafeedAsync(IStopDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IStopDatafeedResponse StopDatafeed(Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null) =>
			this.StopDatafeed(selector.InvokeOrDefault(new StopDatafeedDescriptor(datafeedId)));

		/// <inheritdoc/>
		public IStopDatafeedResponse StopDatafeed(IStopDatafeedRequest request) =>
			this.Dispatcher.Dispatch<IStopDatafeedRequest, StopDatafeedRequestParameters, StopDatafeedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlStopDatafeedDispatch<StopDatafeedResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IStopDatafeedResponse> StopDatafeedAsync(Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.StopDatafeedAsync(selector.InvokeOrDefault(new StopDatafeedDescriptor(datafeedId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IStopDatafeedResponse> StopDatafeedAsync(IStopDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IStopDatafeedRequest, StopDatafeedRequestParameters, StopDatafeedResponse, IStopDatafeedResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlStopDatafeedDispatchAsync<StopDatafeedResponse>(p, c)
			);
	}
}
