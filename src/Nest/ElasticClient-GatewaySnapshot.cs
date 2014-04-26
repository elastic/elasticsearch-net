using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IShardsOperationResponse GatewaySnapshot(Func<GatewaySnapshotDescriptor, GatewaySnapshotDescriptor> snapshotSelector = null)
		{
			snapshotSelector = snapshotSelector ?? (s => s);
			return this.Dispatch<GatewaySnapshotDescriptor, GatewaySnapshotRequestParameters, ShardsOperationResponse>(
				snapshotSelector,
				(p, d) => this.RawDispatch.IndicesSnapshotIndexDispatch<ShardsOperationResponse>(p)
			);
		}
			
		/// <inheritdoc />
		public Task<IShardsOperationResponse> GatewaySnapshotAsync(Func<GatewaySnapshotDescriptor, GatewaySnapshotDescriptor> snapshotSelector = null)
		{
			snapshotSelector = snapshotSelector ?? (s => s);
			return this.DispatchAsync<GatewaySnapshotDescriptor, GatewaySnapshotRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				snapshotSelector,
				(p, d) => this.RawDispatch.IndicesSnapshotIndexDispatchAsync<ShardsOperationResponse>(p)
			);
		}
	}
}