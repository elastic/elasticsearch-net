using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatRepositories
{
	[Collection(IntegrationContext.Indexing)]
	public class CatRepositoriesApiTests : ApiIntegrationTestBase<ICatResponse<CatRepositoriesRecord>, ICatRepositoriesRequest, CatRepositoriesDescriptor, CatRepositoriesRequest>
	{
		private static readonly string RepositoryName = RandomString();

		public CatRepositoriesApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (!TestClient.Configuration.RunIntegrationTests) return;
			var repositoryLocation = Path.Combine(this.Cluster.Node.RepositoryPath, RandomString());

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
