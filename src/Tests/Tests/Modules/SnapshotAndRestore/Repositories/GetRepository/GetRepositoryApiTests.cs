using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.SnapshotAndRestore.Repositories.GetRepository
{
	public class GetRepositoryApiTests
		: ApiTestBase<ReadOnlyCluster, IGetRepositoryResponse, IGetRepositoryRequest, GetRepositoryDescriptor, GetRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public GetRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetRepositoryDescriptor, IGetRepositoryRequest> Fluent => d => d.RepositoryName(_name);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetRepositoryRequest Initializer => new GetRepositoryRequest(_name);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetRepository(f),
			(client, f) => client.GetRepositoryAsync(f),
			(client, r) => client.GetRepository(r),
			(client, r) => client.GetRepositoryAsync(r)
		);
	}
}
