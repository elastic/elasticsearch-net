using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Repositories.VerifyRepository
{
	[Collection(IntegrationContext.ReadOnly)]
	public class VerifyRepositoryApiTests : ApiTestBase<IVerifyRepositoryResponse, IVerifyRepositoryRequest, VerifyRepositoryDescriptor, VerifyRepositoryRequest>
	{
		public VerifyRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "repository1";


		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.VerifyRepository(_name, f),
			fluentAsync: (client, f) => client.VerifyRepositoryAsync(_name, f),
			request: (client, r) => client.VerifyRepository(r),
			requestAsync: (client, r) => client.VerifyRepositoryAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_snapshot/{_name}/_verify";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new { };

		protected override VerifyRepositoryDescriptor NewDescriptor() => new VerifyRepositoryDescriptor(_name);

		protected override Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> Fluent => d => d;

		protected override VerifyRepositoryRequest Initializer => new VerifyRepositoryRequest(_name);
	}
}
