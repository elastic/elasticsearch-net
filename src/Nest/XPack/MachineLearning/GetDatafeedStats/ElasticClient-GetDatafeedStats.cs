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
		GetDatafeedStatsResponse GetDatafeedStats(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null);

		/// <inheritdoc />
		GetDatafeedStatsResponse GetDatafeedStats(IGetDatafeedStatsRequest request);

		/// <inheritdoc />
		Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(IGetDatafeedStatsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetDatafeedStatsResponse GetDatafeedStats(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null) =>
			GetDatafeedStats(selector.InvokeOrDefault(new GetDatafeedStatsDescriptor()));

		/// <inheritdoc />
		public GetDatafeedStatsResponse GetDatafeedStats(IGetDatafeedStatsRequest request) =>
			DoRequest<IGetDatafeedStatsRequest, GetDatafeedStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(
			Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null,
			CancellationToken ct = default
		) => GetDatafeedStatsAsync(selector.InvokeOrDefault(new GetDatafeedStatsDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(IGetDatafeedStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetDatafeedStatsRequest, GetDatafeedStatsResponse>(request, request.RequestParameters, ct);
	}
}
