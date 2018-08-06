using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.GetSnapshot
{
	public class GetSnapshotApiTests : ApiTestBase<ReadOnlyCluster, IGetSnapshotResponse, IGetSnapshotRequest, GetSnapshotDescriptor, GetSnapshotRequest>
	{
		public GetSnapshotApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _repos = "repository1";
		private static readonly string _snapshot = "snapshot1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetSnapshot(_repos, _snapshot, f),
			fluentAsync: (client, f) => client.GetSnapshotAsync(_repos, _snapshot, f),
			request: (client, r) => client.GetSnapshot(r),
			requestAsync: (client, r) => client.GetSnapshotAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_snapshot/{_repos}/{_snapshot}";

		protected override bool SupportsDeserialization => false;

		protected override GetSnapshotDescriptor NewDescriptor() => new GetSnapshotDescriptor(_repos, _snapshot);

		protected override Func<GetSnapshotDescriptor, IGetSnapshotRequest> Fluent => d => d;

		protected override GetSnapshotRequest Initializer => new GetSnapshotRequest(_repos, _snapshot);
	}
}
