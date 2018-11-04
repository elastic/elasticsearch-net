using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.GetSnapshot
{
	public class GetSnapshotApiTests
		: ApiTestBase<ReadOnlyCluster, IGetSnapshotResponse, IGetSnapshotRequest, GetSnapshotDescriptor, GetSnapshotRequest>
	{
		private static readonly string _repos = "repository1";
		private static readonly string _snapshot = "snapshot1";

		public GetSnapshotApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetSnapshotDescriptor, IGetSnapshotRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetSnapshotRequest Initializer => new GetSnapshotRequest(_repos, _snapshot);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_repos}/{_snapshot}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetSnapshot(_repos, _snapshot, f),
			(client, f) => client.GetSnapshotAsync(_repos, _snapshot, f),
			(client, r) => client.GetSnapshot(r),
			(client, r) => client.GetSnapshotAsync(r)
		);

		protected override GetSnapshotDescriptor NewDescriptor() => new GetSnapshotDescriptor(_repos, _snapshot);
	}
}
