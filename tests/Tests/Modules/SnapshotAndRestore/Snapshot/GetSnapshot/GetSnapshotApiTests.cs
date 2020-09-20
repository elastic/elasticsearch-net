// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.GetSnapshot
{
	public class GetSnapshotApiTests
		: ApiTestBase<ReadOnlyCluster, GetSnapshotResponse, IGetSnapshotRequest, GetSnapshotDescriptor, GetSnapshotRequest>
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
			(client, f) => client.Snapshot.Get(_repos, _snapshot, f),
			(client, f) => client.Snapshot.GetAsync(_repos, _snapshot, f),
			(client, r) => client.Snapshot.Get(r),
			(client, r) => client.Snapshot.GetAsync(r)
		);

		protected override GetSnapshotDescriptor NewDescriptor() => new GetSnapshotDescriptor(_repos, _snapshot);
	}
}
