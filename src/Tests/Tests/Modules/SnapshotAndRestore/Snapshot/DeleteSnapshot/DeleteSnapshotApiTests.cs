using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.DeleteSnapshot
{
	public class DeleteSnapshotApiTests
		: ApiTestBase<WritableCluster, IDeleteSnapshotResponse, IDeleteSnapshotRequest, DeleteSnapshotDescriptor, DeleteSnapshotRequest>
	{
		private static readonly string _repos = "repository1";
		private static readonly string _snapshot = "snapshot1";

		public DeleteSnapshotApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteSnapshotRequest Initializer => new DeleteSnapshotRequest(_repos, _snapshot);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_repos}/{_snapshot}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteSnapshot(_repos, _snapshot, f),
			(client, f) => client.DeleteSnapshotAsync(_repos, _snapshot, f),
			(client, r) => client.DeleteSnapshot(r),
			(client, r) => client.DeleteSnapshotAsync(r)
		);

		protected override DeleteSnapshotDescriptor NewDescriptor() => new DeleteSnapshotDescriptor(_repos, _snapshot);
	}
}
