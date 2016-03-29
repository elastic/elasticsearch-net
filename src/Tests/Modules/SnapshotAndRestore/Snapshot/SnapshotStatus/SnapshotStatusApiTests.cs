using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.SnapshotStatus
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SnapshotStatusApiTests : ApiTestBase<ISnapshotStatusResponse, ISnapshotStatusRequest, SnapshotStatusDescriptor, SnapshotStatusRequest>
	{
		public SnapshotStatusApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _repos = "repository1";
		private static readonly string _snapshot = "snapshot1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.SnapshotStatus(f),
			fluentAsync: (client, f) => client.SnapshotStatusAsync(f),
			request: (client, r) => client.SnapshotStatus(r),
			requestAsync: (client, r) => client.SnapshotStatusAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_snapshot/{_repos}/{_snapshot}/_status";

		protected override bool SupportsDeserialization => false;

		protected override Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> Fluent => d => d
			.RepositoryName(_repos)
			.Snapshot(_snapshot)
		;

		protected override SnapshotStatusRequest Initializer => new SnapshotStatusRequest(_repos, _snapshot);
	}
}
