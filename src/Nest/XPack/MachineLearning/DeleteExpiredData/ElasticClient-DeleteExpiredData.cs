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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IDeleteExpiredDataResponse> DeleteExpiredDataAsync(IDeleteExpiredDataRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteExpiredDataResponse DeleteExpiredData(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null) =>
			DeleteExpiredData(selector.InvokeOrDefault(new DeleteExpiredDataDescriptor()));

		/// <inheritdoc />
		public IDeleteExpiredDataResponse DeleteExpiredData(IDeleteExpiredDataRequest request) =>
			Dispatcher.Dispatch<IDeleteExpiredDataRequest, DeleteExpiredDataRequestParameters, DeleteExpiredDataResponse>(
				request,
				(p, d) => LowLevelDispatch.MlDeleteExpiredDataDispatch<DeleteExpiredDataResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeleteExpiredDataResponse> DeleteExpiredDataAsync(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteExpiredDataAsync(selector.InvokeOrDefault(new DeleteExpiredDataDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IDeleteExpiredDataResponse> DeleteExpiredDataAsync(IDeleteExpiredDataRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IDeleteExpiredDataRequest, DeleteExpiredDataRequestParameters, DeleteExpiredDataResponse, IDeleteExpiredDataResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.MlDeleteExpiredDataDispatchAsync<DeleteExpiredDataResponse>(p, c)
				);
	}
}
