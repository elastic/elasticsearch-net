using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Start a machine learning datafeed.
		/// A datafeed must be started in order to retrieve data from Elasticsearch. A datafeed can be started and stopped multiple times throughout
		/// its lifecycle.
		/// </summary>
		StartDatafeedResponse StartDatafeed(Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null);

		/// <inheritdoc />
		StartDatafeedResponse StartDatafeed(IStartDatafeedRequest request);

		/// <inheritdoc />
		Task<StartDatafeedResponse> StartDatafeedAsync(Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<StartDatafeedResponse> StartDatafeedAsync(IStartDatafeedRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public StartDatafeedResponse StartDatafeed(Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null) =>
			StartDatafeed(selector.InvokeOrDefault(new StartDatafeedDescriptor(datafeedId)));

		/// <inheritdoc />
		public StartDatafeedResponse StartDatafeed(IStartDatafeedRequest request) =>
			DoRequest<IStartDatafeedRequest, StartDatafeedResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<StartDatafeedResponse> StartDatafeedAsync(
			Id datafeedId,
			Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			StartDatafeedAsync(selector.InvokeOrDefault(new StartDatafeedDescriptor(datafeedId)), cancellationToken);

		/// <inheritdoc />
		public Task<StartDatafeedResponse> StartDatafeedAsync(IStartDatafeedRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStartDatafeedRequest, StartDatafeedResponse, StartDatafeedResponse>
				(request, request.RequestParameters, ct);
	}
}
