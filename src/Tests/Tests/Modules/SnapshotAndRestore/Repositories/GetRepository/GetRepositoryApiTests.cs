using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Repositories.GetRepository
{
	public class GetRepositoryApiTests : ApiTestBase<ReadOnlyCluster, IGetRepositoryResponse, IGetRepositoryRequest, GetRepositoryDescriptor, GetRepositoryRequest>
	{
		public GetRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "repository1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetRepository(f),
			fluentAsync: (client, f) => client.GetRepositoryAsync(f),
			request: (client, r) => client.GetRepository(r),
			requestAsync: (client, r) => client.GetRepositoryAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override bool SupportsDeserialization => false;

		protected override Func<GetRepositoryDescriptor, IGetRepositoryRequest> Fluent => d => d.RepositoryName(_name);

		protected override GetRepositoryRequest Initializer => new GetRepositoryRequest(_name);
	}
}
