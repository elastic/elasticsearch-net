// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.Snapshot
{
	public class SnapshotApiTests : ApiTestBase<ReadOnlyCluster, SnapshotResponse, ISnapshotRequest, SnapshotDescriptor, SnapshotRequest>
	{
		private static readonly string _repos = "repository1";
		private static readonly string _snapshot = "snapshot1";

		public SnapshotApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			indices = "project",
			include_global_state = true
		};

		protected override Func<SnapshotDescriptor, ISnapshotRequest> Fluent => d => d
			.Index<Project>()
			.IncludeGlobalState()
			.WaitForCompletion();

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override SnapshotRequest Initializer => new SnapshotRequest(_repos, _snapshot)
		{
			Indices = Index<Project>(),
			IncludeGlobalState = true,
			WaitForCompletion = true
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_repos}/{_snapshot}?wait_for_completion=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.Snapshot(_repos, _snapshot, f),
			(client, f) => client.Snapshot.SnapshotAsync(_repos, _snapshot, f),
			(client, r) => client.Snapshot.Snapshot(r),
			(client, r) => client.Snapshot.SnapshotAsync(r)
		);

		protected override SnapshotDescriptor NewDescriptor() => new SnapshotDescriptor(_repos, _snapshot);
	}
}
