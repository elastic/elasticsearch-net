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

		/// <inheritdoc/>
		IGetDatafeedsResponse GetDatafeeds(IGetDatafeedsRequest request);

		/// <inheritdoc/>
		Task<IGetDatafeedsResponse> GetDatafeedsAsync(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetDatafeedsResponse> GetDatafeedsAsync(IGetDatafeedsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetDatafeedsResponse GetDatafeeds(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null) =>
			this.GetDatafeeds(selector.InvokeOrDefault(new GetDatafeedsDescriptor()));

		/// <inheritdoc/>
		public IGetDatafeedsResponse GetDatafeeds(IGetDatafeedsRequest request) =>
			this.Dispatcher.Dispatch<IGetDatafeedsRequest, GetDatafeedsRequestParameters, GetDatafeedsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlGetDatafeedsDispatch<GetDatafeedsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetDatafeedsResponse> GetDatafeedsAsync(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetDatafeedsAsync(selector.InvokeOrDefault(new GetDatafeedsDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetDatafeedsResponse> GetDatafeedsAsync(IGetDatafeedsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetDatafeedsRequest, GetDatafeedsRequestParameters, GetDatafeedsResponse, IGetDatafeedsResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlGetDatafeedsDispatchAsync<GetDatafeedsResponse>(p, c)
			);
	}
}
