using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes an existing datafeed for a machine learning job.
		/// </summary>
		IDeleteDatafeedResponse DeleteDatafeed(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null);

		/// <inheritdoc />
		IDeleteDatafeedResponse DeleteDatafeed(IDeleteDatafeedRequest request);

		/// <inheritdoc />
		Task<IDeleteDatafeedResponse> DeleteDatafeedAsync(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteDatafeedResponse> DeleteDatafeedAsync(IDeleteDatafeedRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteDatafeedResponse DeleteDatafeed(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null) =>
			DeleteDatafeed(selector.InvokeOrDefault(new DeleteDatafeedDescriptor(datafeedId)));

		/// <inheritdoc />
		public IDeleteDatafeedResponse DeleteDatafeed(IDeleteDatafeedRequest request) =>
			Dispatch2<IDeleteDatafeedRequest, DeleteDatafeedResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteDatafeedResponse> DeleteDatafeedAsync(
			Id datafeedId,
			Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null,
			CancellationToken ct = default
		) => DeleteDatafeedAsync(selector.InvokeOrDefault(new DeleteDatafeedDescriptor(datafeedId)), ct);

		/// <inheritdoc />
		public Task<IDeleteDatafeedResponse> DeleteDatafeedAsync(IDeleteDatafeedRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteDatafeedRequest, IDeleteDatafeedResponse, DeleteDatafeedResponse>(request, request.RequestParameters, ct);
	}
}
