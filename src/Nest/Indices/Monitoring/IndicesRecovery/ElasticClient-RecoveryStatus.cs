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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRecoveryStatusResponse RecoveryStatus(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null) =>
			RecoveryStatus(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)));

		/// <inheritdoc />
		public IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest request) =>
			Dispatch2<IRecoveryStatusRequest, RecoveryStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(
			Indices indices,
			Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null,
			CancellationToken ct = default
		) =>
			RecoveryStatusAsync(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IRecoveryStatusRequest, IRecoveryStatusResponse, RecoveryStatusResponse>(request, request.RequestParameters, ct);
	}
}
