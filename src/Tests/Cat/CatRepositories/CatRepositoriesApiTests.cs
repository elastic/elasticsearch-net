using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net_5_2_0;
using FluentAssertions;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatRepositories
{
	[SkipVersion("<2.1.0", "")]
	public class CatRepositoriesApiTests : ApiIntegrationTestBase<IntrusiveOperationCluster, ICatResponse<CatRepositoriesRecord>, ICatRepositoriesRequest, CatRepositoriesDescriptor, CatRepositoriesRequest>
	{
		private static readonly string RepositoryName = RandomString();

		public CatRepositoriesApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (!TestClient.Configuration.RunIntegrationTests) return;
			var repositoryLocation = Path.Combine(this.Cluster.Node.FileSystem.RepositoryPath, RandomString());

			var create = this.Client.CreateRepository(RepositoryName, cr => cr
				.FileSystem(fs => fs
					.Settings(repositoryLocation)
				)
			);

			if (!create.IsValid || !create.Acknowledged)
				throw new Exception("Setup: failed to create snapshot repository");
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatRepositories(f),
			fluentAsync: (client, f) => client.CatRepositoriesAsync(f),
			request: (client, r) => client.CatRepositories(r),
			requestAsync: (client, r) => client.CatRepositoriesAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_cat/repositories";

		protected override void ExpectResponse(ICatResponse<CatRepositoriesRecord> response)
		{
			response.Records.Should().NotBeEmpty()
				.And.OnlyContain(r=>
					!string.IsNullOrEmpty(r.Id)
					&& !string.IsNullOrEmpty(r.Type)
				);
		}
	}
}
