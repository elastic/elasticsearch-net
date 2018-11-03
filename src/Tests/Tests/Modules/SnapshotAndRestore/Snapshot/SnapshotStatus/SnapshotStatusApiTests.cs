using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.SnapshotStatus
{
	public class SnapshotStatusApiTests
		: ApiTestBase<ReadOnlyCluster, ISnapshotStatusResponse, ISnapshotStatusRequest, SnapshotStatusDescriptor, SnapshotStatusRequest>
	{
		private static readonly string _repos = "repository1";
		private static readonly string _snapshot = "snapshot1";

		public SnapshotStatusApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> Fluent => d => d
			.RepositoryName(_repos)
			.Snapshot(_snapshot);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override SnapshotStatusRequest Initializer => new SnapshotStatusRequest(_repos, _snapshot);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_repos}/{_snapshot}/_status";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.SnapshotStatus(f),
			(client, f) => client.SnapshotStatusAsync(f),
			(client, r) => client.SnapshotStatus(r),
			(client, r) => client.SnapshotStatusAsync(r)
		);
	}
}
