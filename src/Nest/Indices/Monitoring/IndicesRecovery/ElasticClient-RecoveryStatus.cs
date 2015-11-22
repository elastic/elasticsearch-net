using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using RecoveryStatusConverter = Func<IApiCallDetails, Stream, RecoveryStatusResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IRecoveryStatusResponse RecoveryStatus(Indices infices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null);

		/// <inheritdoc/>
		IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest statusRequest);

		/// <inheritdoc/>
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null);

		/// <inheritdoc/>
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest statusRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRecoveryStatusResponse RecoveryStatus(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null) =>
			this.RecoveryStatus(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest statusRequest) => 
			this.Dispatcher.Dispatch<IRecoveryStatusRequest, RecoveryStatusRequestParameters, RecoveryStatusResponse>(
				statusRequest,
				new RecoveryStatusConverter(DeserializeRecoveryStatusResponse),
				(p, d) => this.LowLevelDispatch.IndicesRecoveryDispatch<RecoveryStatusResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null) => 
			this.RecoveryStatusAsync(selector.InvokeOrDefault(new RecoveryStatusDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest statusRequest) => 
			this.Dispatcher.DispatchAsync<IRecoveryStatusRequest, RecoveryStatusRequestParameters, RecoveryStatusResponse, IRecoveryStatusResponse>(
				statusRequest,
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