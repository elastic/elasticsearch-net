using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Stop a machine learning data feed.
		/// A datafeed that is stopped ceases to retrieve data from Elasticsearch. A datafeed can be started and stopped multiple times throughout its
		/// lifecycle.
		/// </summary>
		IStopDatafeedResponse StopDatafeed(Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null);

		/// <inheritdoc />
		IStopDatafeedResponse StopDatafeed(IStopDatafeedRequest request);

		/// <inheritdoc />
		Task<IStopDatafeedResponse> StopDatafeedAsync(Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IStopDatafeedResponse> StopDatafeedAsync(IStopDatafeedRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IStopDatafeedResponse StopDatafeed(Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null) =>
			StopDatafeed(selector.InvokeOrDefault(new StopDatafeedDescriptor(datafeedId)));

		/// <inheritdoc />
		public IStopDatafeedResponse StopDatafeed(IStopDatafeedRequest request) =>
			DoRequest<IStopDatafeedRequest, StopDatafeedResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IStopDatafeedResponse> StopDatafeedAsync(
			Id datafeedId,
			Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null,
			CancellationToken ct = default
		) => StopDatafeedAsync(selector.InvokeOrDefault(new StopDatafeedDescriptor(datafeedId)), ct);

		/// <inheritdoc />
		public Task<IStopDatafeedResponse> StopDatafeedAsync(IStopDatafeedRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStopDatafeedRequest, IStopDatafeedResponse, StopDatafeedResponse>
				(request, request.RequestParameters, ct);
	}
}
