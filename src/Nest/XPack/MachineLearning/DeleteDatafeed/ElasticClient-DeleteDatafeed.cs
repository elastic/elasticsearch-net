using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes an existing datafeed for a Machine Learning job.
		/// </summary>
		IDeleteDatafeedResponse DeleteDatafeed(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null);

		/// <inheritdoc/>
		IDeleteDatafeedResponse DeleteDatafeed(IDeleteDatafeedRequest request);

		/// <inheritdoc/>
		Task<IDeleteDatafeedResponse> DeleteDatafeedAsync(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteDatafeedResponse> DeleteDatafeedAsync(IDeleteDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteDatafeedResponse DeleteDatafeed(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null) =>
			this.DeleteDatafeed(selector.InvokeOrDefault(new DeleteDatafeedDescriptor(datafeedId)));

		/// <inheritdoc/>
		public IDeleteDatafeedResponse DeleteDatafeed(IDeleteDatafeedRequest request) =>
			this.Dispatcher.Dispatch<IDeleteDatafeedRequest, DeleteDatafeedRequestParameters, DeleteDatafeedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlDeleteDatafeedDispatch<DeleteDatafeedResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteDatafeedResponse> DeleteDatafeedAsync(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteDatafeedAsync(selector.InvokeOrDefault(new DeleteDatafeedDescriptor(datafeedId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteDatafeedResponse> DeleteDatafeedAsync(IDeleteDatafeedRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteDatafeedRequest, DeleteDatafeedRequestParameters, DeleteDatafeedResponse, IDeleteDatafeedResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlDeleteDatafeedDispatchAsync<DeleteDatafeedResponse>(p, c)
			);
	}
}
