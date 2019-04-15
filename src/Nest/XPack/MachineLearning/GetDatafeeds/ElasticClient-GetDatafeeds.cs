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
		GetDatafeedsResponse GetDatafeeds(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null);

		/// <inheritdoc />
		GetDatafeedsResponse GetDatafeeds(IGetDatafeedsRequest request);

		/// <inheritdoc />
		Task<GetDatafeedsResponse> GetDatafeedsAsync(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetDatafeedsResponse> GetDatafeedsAsync(IGetDatafeedsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetDatafeedsResponse GetDatafeeds(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null) =>
			GetDatafeeds(selector.InvokeOrDefault(new GetDatafeedsDescriptor()));

		/// <inheritdoc />
		public GetDatafeedsResponse GetDatafeeds(IGetDatafeedsRequest request) =>
			DoRequest<IGetDatafeedsRequest, GetDatafeedsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetDatafeedsResponse> GetDatafeedsAsync(
			Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null,
			CancellationToken ct = default
		) => GetDatafeedsAsync(selector.InvokeOrDefault(new GetDatafeedsDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetDatafeedsResponse> GetDatafeedsAsync(IGetDatafeedsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetDatafeedsRequest, GetDatafeedsResponse>(request, request.RequestParameters, ct);
	}
}
