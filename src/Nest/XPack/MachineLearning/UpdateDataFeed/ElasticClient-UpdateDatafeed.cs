using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Update a Machine Learning datafeed.
		/// </summary>
		IUpdateDatafeedResponse UpdateDatafeed<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null) where T : class;

		/// <inheritdoc/>
		IUpdateDatafeedResponse UpdateDatafeed(IUpdateDatafeedRequest request);

		/// <inheritdoc/>
		Task<IUpdateDatafeedResponse> UpdateDatafeedAsync<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

		/// <inheritdoc/>
		Task<IUpdateDatafeedResponse> UpdateDatafeedAsync(IUpdateDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IUpdateDatafeedResponse UpdateDatafeed<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null) where T : class =>
			this.UpdateDatafeed(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<T>(datafeedId)));

		/// <inheritdoc/>
		public IUpdateDatafeedResponse UpdateDatafeed(IUpdateDatafeedRequest request) =>
			this.Dispatcher.Dispatch<IUpdateDatafeedRequest, UpdateDatafeedRequestParameters, UpdateDatafeedResponse>(
				request,
				this.LowLevelDispatch.XpackMlUpdateDatafeedDispatch<UpdateDatafeedResponse>
			);

		/// <inheritdoc/>
		public Task<IUpdateDatafeedResponse> UpdateDatafeedAsync<T>(Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.UpdateDatafeedAsync(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<T>(datafeedId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IUpdateDatafeedResponse> UpdateDatafeedAsync(IUpdateDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IUpdateDatafeedRequest, UpdateDatafeedRequestParameters, UpdateDatafeedResponse, IUpdateDatafeedResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlUpdateDatafeedDispatchAsync<UpdateDatafeedResponse>
			);
	}
}
