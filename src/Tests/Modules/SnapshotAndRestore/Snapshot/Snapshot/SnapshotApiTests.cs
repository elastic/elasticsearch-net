using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.Snapshot
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SnapshotApiTests : ApiTestBase<ISnapshotResponse, ISnapshotRequest, SnapshotDescriptor, SnapshotRequest>
	{
		public SnapshotApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _repos = "repository1";
		private static readonly string _snapshot = "snapshot1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Snapshot(_repos, _snapshot, f),
			fluentAsync: (client, f) => client.SnapshotAsync(_repos, _snapshot, f),
			request: (client, r) => client.Snapshot(r),
			requestAsync: (client, r) => client.SnapshotAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_snapshot/{_repos}/{_snapshot}?wait_for_completion=true";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			indices = "project",
			include_global_state = true
		};

		protected override SnapshotDescriptor NewDescriptor() => new SnapshotDescriptor(_repos, _snapshot);

		protected override Func<SnapshotDescriptor, ISnapshotRequest> Fluent => d => d
			.Index<Project>()
			.IncludeGlobalState()
			.WaitForCompletion()
		;

		protected override SnapshotRequest Initializer => new SnapshotRequest(_repos, _snapshot)
		{
			Indices = Index<Project>(),
			IncludeGlobalState = true,
			WaitForCompletion = true
		};
	}
}
