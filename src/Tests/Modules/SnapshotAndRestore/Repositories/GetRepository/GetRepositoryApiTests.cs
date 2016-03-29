using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Repositories.GetRepository
{
	[Collection(IntegrationContext.ReadOnly)]
	public class GetRepositoryApiTests : ApiTestBase<IGetRepositoryResponse, IGetRepositoryRequest, GetRepositoryDescriptor, GetRepositoryRequest>
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
