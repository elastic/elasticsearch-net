// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Repositories.GetRepository
{
	public class GetRepositoryApiTests
		: ApiTestBase<WritableCluster, GetRepositoryResponse, IGetRepositoryRequest, GetRepositoryDescriptor, GetRepositoryRequest>
	{
		public GetRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetRepositoryDescriptor, IGetRepositoryRequest> Fluent => d => d.RepositoryName(CallIsolatedValue);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetRepositoryRequest Initializer => new GetRepositoryRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.GetRepository(f),
			(client, f) => client.Snapshot.GetRepositoryAsync(f),
			(client, r) => client.Snapshot.GetRepository(r),
			(client, r) => client.Snapshot.GetRepositoryAsync(r)
		);

	}
}
