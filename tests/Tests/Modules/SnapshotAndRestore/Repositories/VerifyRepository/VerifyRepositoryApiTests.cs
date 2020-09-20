// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Repositories.VerifyRepository
{
	public class VerifyRepositoryApiTests
		: ApiTestBase<ReadOnlyCluster, VerifyRepositoryResponse, IVerifyRepositoryRequest, VerifyRepositoryDescriptor, VerifyRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public VerifyRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new { };

		protected override Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override VerifyRepositoryRequest Initializer => new VerifyRepositoryRequest(_name);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}/_verify";


		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.VerifyRepository(_name, f),
			(client, f) => client.Snapshot.VerifyRepositoryAsync(_name, f),
			(client, r) => client.Snapshot.VerifyRepository(r),
			(client, r) => client.Snapshot.VerifyRepositoryAsync(r)
		);

		protected override VerifyRepositoryDescriptor NewDescriptor() => new VerifyRepositoryDescriptor(_name);
	}
}
