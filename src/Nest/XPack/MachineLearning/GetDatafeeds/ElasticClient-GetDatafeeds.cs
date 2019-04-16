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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetDatafeedsResponse> GetDatafeedsAsync(IGetDatafeedsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetDatafeedsResponse GetDatafeeds(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null) =>
			GetDatafeeds(selector.InvokeOrDefault(new GetDatafeedsDescriptor()));

		/// <inheritdoc />
		public IGetDatafeedsResponse GetDatafeeds(IGetDatafeedsRequest request) =>
			Dispatcher.Dispatch<IGetDatafeedsRequest, GetDatafeedsRequestParameters, GetDatafeedsResponse>(
				request,
				(p, d) => LowLevelDispatch.MlGetDatafeedsDispatch<GetDatafeedsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetDatafeedsResponse> GetDatafeedsAsync(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetDatafeedsAsync(selector.InvokeOrDefault(new GetDatafeedsDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetDatafeedsResponse> GetDatafeedsAsync(IGetDatafeedsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetDatafeedsRequest, GetDatafeedsRequestParameters, GetDatafeedsResponse, IGetDatafeedsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.MlGetDatafeedsDispatchAsync<GetDatafeedsResponse>(p, c)
			);
	}
}
