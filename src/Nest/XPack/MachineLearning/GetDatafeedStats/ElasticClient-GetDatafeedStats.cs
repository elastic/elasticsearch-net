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

		/// <inheritdoc />
		IGetDatafeedStatsResponse GetDatafeedStats(IGetDatafeedStatsRequest request);

		/// <inheritdoc />
		Task<IGetDatafeedStatsResponse> GetDatafeedStatsAsync(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetDatafeedStatsResponse> GetDatafeedStatsAsync(IGetDatafeedStatsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetDatafeedStatsResponse GetDatafeedStats(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null) =>
			GetDatafeedStats(selector.InvokeOrDefault(new GetDatafeedStatsDescriptor()));

		/// <inheritdoc />
		public IGetDatafeedStatsResponse GetDatafeedStats(IGetDatafeedStatsRequest request) =>
			DoRequest<IGetDatafeedStatsRequest, GetDatafeedStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetDatafeedStatsResponse> GetDatafeedStatsAsync(
			Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null,
			CancellationToken ct = default
		) => GetDatafeedStatsAsync(selector.InvokeOrDefault(new GetDatafeedStatsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetDatafeedStatsResponse> GetDatafeedStatsAsync(IGetDatafeedStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetDatafeedStatsRequest, IGetDatafeedStatsResponse, GetDatafeedStatsResponse>(request, request.RequestParameters, ct);
	}
}
