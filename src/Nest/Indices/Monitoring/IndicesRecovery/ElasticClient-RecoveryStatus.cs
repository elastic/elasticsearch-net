using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IRecoveryStatusResponse RecoveryStatus(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null);

		/// <inheritdoc />
		IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest request);

		/// <inheritdoc />
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRecoveryStatusResponse RecoveryStatus(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null) =>
			RecoveryStatus(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)));

		/// <inheritdoc />
		public IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest request) =>
			Dispatcher.Dispatch<IRecoveryStatusRequest, RecoveryStatusRequestParameters, RecoveryStatusResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesRecoveryDispatch<RecoveryStatusResponse>(p)
			);

		/// <inheritdoc />
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices,
			Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			RecoveryStatusAsync(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IRecoveryStatusRequest, RecoveryStatusRequestParameters, RecoveryStatusResponse, IRecoveryStatusResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesRecoveryDispatchAsync<RecoveryStatusResponse>(p, c)
			);
	}
}
