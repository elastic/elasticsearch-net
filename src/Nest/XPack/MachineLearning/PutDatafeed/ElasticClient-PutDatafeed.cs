using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a Machine Learning datafeed.
		/// You must create a job before you create a datafeed. You can associate only one datafeed to each job.
		/// </summary>
		IPutDatafeedResponse PutDatafeed<T>(Id datafeedId, Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null) where T : class;

		/// <inheritdoc/>
		IPutDatafeedResponse PutDatafeed(IPutDatafeedRequest request);

		/// <inheritdoc/>
		Task<IPutDatafeedResponse> PutDatafeedAsync<T>(Id datafeedId, Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

		/// <inheritdoc/>
		Task<IPutDatafeedResponse> PutDatafeedAsync(IPutDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutDatafeedResponse PutDatafeed<T>(Id datafeedId, Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null) where T : class =>
			this.PutDatafeed(selector.InvokeOrDefault(new PutDatafeedDescriptor<T>(datafeedId)));

		/// <inheritdoc/>
		public IPutDatafeedResponse PutDatafeed(IPutDatafeedRequest request) =>
			this.Dispatcher.Dispatch<IPutDatafeedRequest, PutDatafeedRequestParameters, PutDatafeedResponse>(
				request,
				this.LowLevelDispatch.XpackMlPutDatafeedDispatch<PutDatafeedResponse>
			);

		/// <inheritdoc/>
		public Task<IPutDatafeedResponse> PutDatafeedAsync<T>(Id datafeedId, Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.PutDatafeedAsync(selector.InvokeOrDefault(new PutDatafeedDescriptor<T>(datafeedId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPutDatafeedResponse> PutDatafeedAsync(IPutDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutDatafeedRequest, PutDatafeedRequestParameters, PutDatafeedResponse, IPutDatafeedResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlPutDatafeedDispatchAsync<PutDatafeedResponse>
			);
	}
}
