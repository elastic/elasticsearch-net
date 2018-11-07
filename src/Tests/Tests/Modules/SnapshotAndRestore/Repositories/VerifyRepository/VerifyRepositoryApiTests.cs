using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.SnapshotAndRestore.Repositories.VerifyRepository
{
	public class VerifyRepositoryApiTests
		: ApiTestBase<ReadOnlyCluster, IVerifyRepositoryResponse, IVerifyRepositoryRequest, VerifyRepositoryDescriptor, VerifyRepositoryRequest>
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
			(client, f) => client.VerifyRepository(_name, f),
			(client, f) => client.VerifyRepositoryAsync(_name, f),
			(client, r) => client.VerifyRepository(r),
			(client, r) => client.VerifyRepositoryAsync(r)
		);

		protected override VerifyRepositoryDescriptor NewDescriptor() => new VerifyRepositoryDescriptor(_name);
	}
}
