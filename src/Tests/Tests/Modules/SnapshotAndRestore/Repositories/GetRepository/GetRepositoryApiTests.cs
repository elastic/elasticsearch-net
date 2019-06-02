using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Repositories.GetRepository
{
	public class GetRepositoryApiTests
		: ApiTestBase<ReadOnlyCluster, GetRepositoryResponse, IGetRepositoryRequest, GetRepositoryDescriptor, GetRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public GetRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetRepositoryDescriptor, IGetRepositoryRequest> Fluent => d => d.RepositoryName(_name);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetRepositoryRequest Initializer => new GetRepositoryRequest(_name);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.GetRepository(f),
			(client, f) => client.Snapshot.GetRepositoryAsync(f),
			(client, r) => client.Snapshot.GetRepository(r),
			(client, r) => client.Snapshot.GetRepositoryAsync(r)
		);
	}
}
