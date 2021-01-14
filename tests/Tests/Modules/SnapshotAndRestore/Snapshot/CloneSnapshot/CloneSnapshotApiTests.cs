// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.CloneSnapshot
{
	[SkipVersion("<7.10.0", "APIs introduced in 7.10.0")]
	public class CloneSnapshotApiTests
		: ApiTestBase<WritableCluster, CloneSnapshotResponse, ICloneSnapshotRequest, CloneSnapshotDescriptor, CloneSnapshotRequest>
	{
		private const string Repository = "repository1";
		private const string SnapshotName = "snapshot1";
		private const string Target = "snapshot1-clone";

		public CloneSnapshotApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<CloneSnapshotDescriptor, ICloneSnapshotRequest> Fluent => d => d.Indices("*");
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override CloneSnapshotRequest Initializer => new CloneSnapshotRequest(Repository, SnapshotName, Target) { Indices = "*" };

		protected override object ExpectJson =>
			new
			{
				indices = "*"
			};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_snapshot/{Repository}/{SnapshotName}/_clone/{Target}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.Clone(Repository, SnapshotName, Target, f),
			(client, f) => client.Snapshot.CloneAsync(Repository, SnapshotName, Target, f),
			(client, r) => client.Snapshot.Clone(r),
			(client, r) => client.Snapshot.CloneAsync(r)
		);

		protected override CloneSnapshotDescriptor NewDescriptor() => new(Repository, SnapshotName, Target);
	}
}
