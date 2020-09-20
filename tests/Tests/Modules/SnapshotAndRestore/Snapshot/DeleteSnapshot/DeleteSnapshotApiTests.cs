// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.DeleteSnapshot
{
	public class DeleteSnapshotApiTests
		: ApiTestBase<WritableCluster, DeleteSnapshotResponse, IDeleteSnapshotRequest, DeleteSnapshotDescriptor, DeleteSnapshotRequest>
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
			(client, f) => client.Snapshot.Delete(_repos, _snapshot, f),
			(client, f) => client.Snapshot.DeleteAsync(_repos, _snapshot, f),
			(client, r) => client.Snapshot.Delete(r),
			(client, r) => client.Snapshot.DeleteAsync(r)
		);

		protected override DeleteSnapshotDescriptor NewDescriptor() => new DeleteSnapshotDescriptor(_repos, _snapshot);
	}
}
