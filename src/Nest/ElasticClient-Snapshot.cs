using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IIndicesShardResponse Snapshot(Func<SnapshotDescriptor, SnapshotDescriptor> snapShotSelector)
		{
			snapShotSelector.ThrowIfNull("snapShotSelector");
			var descriptor = snapShotSelector(new SnapshotDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesSnapshotIndexDispatch(pathInfo)
				.Deserialize<IndicesShardResponse>();
		}

		public Task<IIndicesShardResponse> SnapshotAsync(Func<SnapshotDescriptor, SnapshotDescriptor> snapShotSelector)
		{
			snapShotSelector.ThrowIfNull("snapShotSelector");
			var descriptor = snapShotSelector(new SnapshotDescriptor());
			var pathInfo = descriptor.ToPathInfo(this._connectionSettings);
			return this.RawDispatch.IndicesSnapshotIndexDispatchAsync(pathInfo)
				.ContinueWith<IIndicesShardResponse>(r=>r.Result.Deserialize<IndicesShardResponse>());
		}

	}
}
