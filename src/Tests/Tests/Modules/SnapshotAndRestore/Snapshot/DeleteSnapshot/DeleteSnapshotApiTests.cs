using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.DeleteSnapshot
{
	public class DeleteSnapshotApiTests : ApiTestBase<WritableCluster, IDeleteSnapshotResponse, IDeleteSnapshotRequest, DeleteSnapshotDescriptor, DeleteSnapshotRequest>
	{
		public DeleteSnapshotApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _repos = "repository1";
		private static readonly string _snapshot = "snapshot1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteSnapshot(_repos, _snapshot, f),
			fluentAsync: (client, f) => client.DeleteSnapshotAsync(_repos, _snapshot, f),
			request: (client, r) => client.DeleteSnapshot(r),
			requestAsync: (client, r) => client.DeleteSnapshotAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_snapshot/{_repos}/{_snapshot}";

		protected override bool SupportsDeserialization => false;

		protected override DeleteSnapshotDescriptor NewDescriptor() => new DeleteSnapshotDescriptor(_repos, _snapshot);

		protected override Func<DeleteSnapshotDescriptor, IDeleteSnapshotRequest> Fluent => d => d;

		protected override DeleteSnapshotRequest Initializer => new DeleteSnapshotRequest(_repos, _snapshot);
	}
}
