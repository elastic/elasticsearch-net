using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		RecoveryStatusResponse RecoveryStatus(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null);

		/// <inheritdoc />
		RecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest request);

		/// <inheritdoc />
		Task<RecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<RecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public RecoveryStatusResponse RecoveryStatus(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null) =>
			RecoveryStatus(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)));

		/// <inheritdoc />
		public RecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest request) =>
			DoRequest<IRecoveryStatusRequest, RecoveryStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<RecoveryStatusResponse> RecoveryStatusAsync(
			Indices indices,
			Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null,
			CancellationToken ct = default
		) =>
			RecoveryStatusAsync(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<RecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IRecoveryStatusRequest, RecoveryStatusResponse>(request, request.RequestParameters, ct);
	}
}
