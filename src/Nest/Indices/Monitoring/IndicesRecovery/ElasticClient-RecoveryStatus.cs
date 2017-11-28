using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using System.Threading;
	using RecoveryStatusConverter = Func<IApiCallDetails, Stream, RecoveryStatusResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IRecoveryStatusResponse RecoveryStatus(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null);

		/// <inheritdoc/>
		IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest request);

		/// <inheritdoc/>
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRecoveryStatusResponse RecoveryStatus(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null) =>
			this.RecoveryStatus(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest request) =>
			this.Dispatcher.Dispatch<IRecoveryStatusRequest, RecoveryStatusRequestParameters, RecoveryStatusResponse>(
				request,
				new RecoveryStatusConverter(DeserializeRecoveryStatusResponse),
				(p, d) => this.LowLevelDispatch.IndicesRecoveryDispatch<RecoveryStatusResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.RecoveryStatusAsync(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc/>
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IRecoveryStatusRequest, RecoveryStatusRequestParameters, RecoveryStatusResponse, IRecoveryStatusResponse>(
				request,
				cancellationToken,
				new RecoveryStatusConverter(DeserializeRecoveryStatusResponse),
				(p, d, c) => this.LowLevelDispatch.IndicesRecoveryDispatchAsync<RecoveryStatusResponse>(p, c)
			);

		private RecoveryStatusResponse DeserializeRecoveryStatusResponse(IApiCallDetails response, Stream stream)
		{
			if (!response.Success) return CreateInvalidInstance<RecoveryStatusResponse>(response);
			var indices = this.RequestResponseSerializer.Deserialize<Dictionary<string, RecoveryStatus>>(stream);
			return new RecoveryStatusResponse { Indices = indices };
		}
	}
}
