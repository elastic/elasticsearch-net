// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.SnapshotStatus
{
	public class SnapshotStatusApiTests
		: ApiTestBase<ReadOnlyCluster, SnapshotStatusResponse, ISnapshotStatusRequest, SnapshotStatusDescriptor, SnapshotStatusRequest>
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
			(client, f) => client.Snapshot.Status(f),
			(client, f) => client.Snapshot.StatusAsync(f),
			(client, r) => client.Snapshot.Status(r),
			(client, r) => client.Snapshot.StatusAsync(r)
		);
	}
}
