using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using RecoveryStatusConverter = Func<IApiCallDetails, Stream, RecoveryStatusResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IRecoveryStatusResponse RecoveryStatus(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null);

		/// <inheritdoc/>
		IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest request);

		/// <inheritdoc/>
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null);

		/// <inheritdoc/>
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request);
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
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null) => 
			this.RecoveryStatusAsync(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest request) => 
			this.Dispatcher.DispatchAsync<IRecoveryStatusRequest, RecoveryStatusRequestParameters, RecoveryStatusResponse, IRecoveryStatusResponse>(
				request,
				new RecoveryStatusConverter(DeserializeRecoveryStatusResponse),
				(p, d) => this.LowLevelDispatch.IndicesRecoveryDispatchAsync<RecoveryStatusResponse>(p)
			);

		private RecoveryStatusResponse DeserializeRecoveryStatusResponse(IApiCallDetails response, Stream stream)
		{
			if (!response.Success) return CreateInvalidInstance<RecoveryStatusResponse>(response);
			var indices = this.Serializer.Deserialize<Dictionary<string, RecoveryStatus>>(stream);
			return new RecoveryStatusResponse { Indices = indices };
		}
	}
}