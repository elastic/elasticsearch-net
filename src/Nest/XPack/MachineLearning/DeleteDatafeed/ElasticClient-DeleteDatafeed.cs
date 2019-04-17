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
		DeleteDatafeedResponse DeleteDatafeed(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null);

		/// <inheritdoc />
		DeleteDatafeedResponse DeleteDatafeed(IDeleteDatafeedRequest request);

		/// <inheritdoc />
		Task<DeleteDatafeedResponse> DeleteDatafeedAsync(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteDatafeedResponse> DeleteDatafeedAsync(IDeleteDatafeedRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteDatafeedResponse DeleteDatafeed(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null) =>
			DeleteDatafeed(selector.InvokeOrDefault(new DeleteDatafeedDescriptor(datafeedId)));

		/// <inheritdoc />
		public DeleteDatafeedResponse DeleteDatafeed(IDeleteDatafeedRequest request) =>
			DoRequest<IDeleteDatafeedRequest, DeleteDatafeedResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteDatafeedResponse> DeleteDatafeedAsync(
			Id datafeedId,
			Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null,
			CancellationToken ct = default
		) => DeleteDatafeedAsync(selector.InvokeOrDefault(new DeleteDatafeedDescriptor(datafeedId)), ct);

		/// <inheritdoc />
		public Task<DeleteDatafeedResponse> DeleteDatafeedAsync(IDeleteDatafeedRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteDatafeedRequest, DeleteDatafeedResponse>(request, request.RequestParameters, ct);
	}
}
