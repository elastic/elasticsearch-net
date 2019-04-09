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
		IDeleteExpiredDataResponse DeleteExpiredData(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null);

		/// <inheritdoc />
		IDeleteExpiredDataResponse DeleteExpiredData(IDeleteExpiredDataRequest request);

		/// <inheritdoc />
		Task<IDeleteExpiredDataResponse> DeleteExpiredDataAsync(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteExpiredDataResponse> DeleteExpiredDataAsync(IDeleteExpiredDataRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteExpiredDataResponse DeleteExpiredData(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null) =>
			DeleteExpiredData(selector.InvokeOrDefault(new DeleteExpiredDataDescriptor()));

		/// <inheritdoc />
		public IDeleteExpiredDataResponse DeleteExpiredData(IDeleteExpiredDataRequest request) =>
			Dispatch2<IDeleteExpiredDataRequest, DeleteExpiredDataResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteExpiredDataResponse> DeleteExpiredDataAsync(
			Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null,
			CancellationToken ct = default
		) => DeleteExpiredDataAsync(selector.InvokeOrDefault(new DeleteExpiredDataDescriptor()), ct);

		/// <inheritdoc />
		public Task<IDeleteExpiredDataResponse> DeleteExpiredDataAsync(IDeleteExpiredDataRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteExpiredDataRequest, IDeleteExpiredDataResponse, DeleteExpiredDataResponse>(request, request.RequestParameters, ct);
	}
}
