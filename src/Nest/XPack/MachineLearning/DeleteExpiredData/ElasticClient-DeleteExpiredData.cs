using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes expired machine learning data.
		/// </summary>
		DeleteExpiredDataResponse DeleteExpiredData(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null);

		/// <inheritdoc />
		DeleteExpiredDataResponse DeleteExpiredData(IDeleteExpiredDataRequest request);

		/// <inheritdoc />
		Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(IDeleteExpiredDataRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteExpiredDataResponse DeleteExpiredData(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null) =>
			DeleteExpiredData(selector.InvokeOrDefault(new DeleteExpiredDataDescriptor()));

		/// <inheritdoc />
		public DeleteExpiredDataResponse DeleteExpiredData(IDeleteExpiredDataRequest request) =>
			DoRequest<IDeleteExpiredDataRequest, DeleteExpiredDataResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(
			Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null,
			CancellationToken ct = default
		) => DeleteExpiredDataAsync(selector.InvokeOrDefault(new DeleteExpiredDataDescriptor()), ct);

		/// <inheritdoc />
		public Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(IDeleteExpiredDataRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteExpiredDataRequest, DeleteExpiredDataResponse, DeleteExpiredDataResponse>(request, request.RequestParameters, ct);
	}
}
