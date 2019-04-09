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
		IGetDatafeedsResponse GetDatafeeds(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null);

		/// <inheritdoc />
		IGetDatafeedsResponse GetDatafeeds(IGetDatafeedsRequest request);

		/// <inheritdoc />
		Task<IGetDatafeedsResponse> GetDatafeedsAsync(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetDatafeedsResponse> GetDatafeedsAsync(IGetDatafeedsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetDatafeedsResponse GetDatafeeds(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null) =>
			GetDatafeeds(selector.InvokeOrDefault(new GetDatafeedsDescriptor()));

		/// <inheritdoc />
		public IGetDatafeedsResponse GetDatafeeds(IGetDatafeedsRequest request) =>
			Dispatch2<IGetDatafeedsRequest, GetDatafeedsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetDatafeedsResponse> GetDatafeedsAsync(
			Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null,
			CancellationToken ct = default
		) => GetDatafeedsAsync(selector.InvokeOrDefault(new GetDatafeedsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetDatafeedsResponse> GetDatafeedsAsync(IGetDatafeedsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetDatafeedsRequest, IGetDatafeedsResponse, GetDatafeedsResponse>(request, request.RequestParameters, ct);
	}
}
