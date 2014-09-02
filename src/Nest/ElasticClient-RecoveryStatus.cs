using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using RecoveryStatusConverter = Func<IElasticsearchResponse, Stream, RecoveryStatusResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRecoveryStatusResponse RecoveryStatus(Func<RecoveryStatusDescriptor, RecoveryStatusDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<RecoveryStatusDescriptor, RecoveryStatusRequestParameters, RecoveryStatusResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesRecoveryDispatch<RecoveryStatusResponse>(
					p.DeserializationState(new RecoveryStatusConverter(DeserializeRecoveryStatusResponse))
				)
			);
		}

		/// <inheritdoc />
		public IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest statusRequest)
		{
			return this.Dispatch<IRecoveryStatusRequest, RecoveryStatusRequestParameters, RecoveryStatusResponse>(
				statusRequest,
				(p, d) => this.RawDispatch.IndicesRecoveryDispatch<RecoveryStatusResponse>(
					p.DeserializationState(new RecoveryStatusConverter(DeserializeRecoveryStatusResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(Func<RecoveryStatusDescriptor, RecoveryStatusDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<RecoveryStatusDescriptor, RecoveryStatusRequestParameters, RecoveryStatusResponse, IRecoveryStatusResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesRecoveryDispatchAsync<RecoveryStatusResponse>(
					p.DeserializationState(new RecoveryStatusConverter(DeserializeRecoveryStatusResponse))
				)
			);
		}

		/// <inheritdoc />
		public Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest statusRequest)
		{
			return this.DispatchAsync<IRecoveryStatusRequest, RecoveryStatusRequestParameters, RecoveryStatusResponse, IRecoveryStatusResponse>(
				statusRequest,
				(p, d) => this.RawDispatch.IndicesRecoveryDispatchAsync<RecoveryStatusResponse>(
					p.DeserializationState(new RecoveryStatusConverter(DeserializeRecoveryStatusResponse))
				)
			);
		}

		private RecoveryStatusResponse DeserializeRecoveryStatusResponse(IElasticsearchResponse response, Stream stream)
		{
			if (!response.Success) return CreateInvalidInstance<RecoveryStatusResponse>(response);
			var indices = this.Serializer.Deserialize<Dictionary<string, RecoveryStatus>>(stream);
			return new RecoveryStatusResponse { IsValid = true, Indices = indices };
		}
	}
}