using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IIndicesShardResponse Snapshot(Func<SnapshotDescriptor, SnapshotDescriptor> snapshotSelector = null)
		{
			snapshotSelector = snapshotSelector ?? (s => s);
			return this.Dispatch<SnapshotDescriptor, SnapshotQueryString, IndicesShardResponse>(
				snapshotSelector,
				(p, d) => this.RawDispatch.IndicesSnapshotIndexDispatch(p)
			);
		}

		public Task<IIndicesShardResponse> SnapshotAsync(Func<SnapshotDescriptor, SnapshotDescriptor> snapshotSelector = null)
		{
			snapshotSelector = snapshotSelector ?? (s => s);
			return this.DispatchAsync<SnapshotDescriptor, SnapshotQueryString, IndicesShardResponse, IIndicesShardResponse>(
				snapshotSelector,
				(p, d) => this.RawDispatch.IndicesSnapshotIndexDispatchAsync(p)
			);
		}

	}
}
