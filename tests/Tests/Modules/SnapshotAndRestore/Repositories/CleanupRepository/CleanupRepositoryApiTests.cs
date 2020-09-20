// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Repositories.CleanupRepository
{
	public class CleanupRepositoryApiTests
		: ApiTestBase<ReadOnlyCluster, CleanupRepositoryResponse, ICleanupRepositoryRequest, CleanupRepositoryDescriptor, CleanupRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public CleanupRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new { };

		protected override Func<CleanupRepositoryDescriptor, ICleanupRepositoryRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override CleanupRepositoryRequest Initializer => new CleanupRepositoryRequest(_name);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}/_cleanup";


		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.CleanupRepository(_name, f),
			(client, f) => client.Snapshot.CleanupRepositoryAsync(_name, f),
			(client, r) => client.Snapshot.CleanupRepository(r),
			(client, r) => client.Snapshot.CleanupRepositoryAsync(r)
		);

		protected override CleanupRepositoryDescriptor NewDescriptor() => new CleanupRepositoryDescriptor(_name);
	}
}
