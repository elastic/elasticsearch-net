// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatRepositories
{
	[SkipVersion("<2.1.0", "")]
	public class CatRepositoriesApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, CatResponse<CatRepositoriesRecord>, ICatRepositoriesRequest, CatRepositoriesDescriptor,
			CatRepositoriesRequest>
	{
		private static readonly string RepositoryName = RandomString();

		public CatRepositoriesApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_cat/repositories";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (!TestClient.Configuration.RunIntegrationTests) return;

			var repositoryLocation = Path.Combine(Cluster.FileSystem.RepositoryPath, RandomString());

			var create = Client.Snapshot.CreateRepository(RepositoryName, cr => cr
				.FileSystem(fs => fs
					.Settings(repositoryLocation)
				)
			);

			if (!create.IsValid || !create.Acknowledged)
				throw new Exception("Setup: failed to create snapshot repository");
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Repositories(f),
			(client, f) => client.Cat.RepositoriesAsync(f),
			(client, r) => client.Cat.Repositories(r),
			(client, r) => client.Cat.RepositoriesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatRepositoriesRecord> response) => response.Records.Should()
			.NotBeEmpty()
			.And.OnlyContain(r =>
				!string.IsNullOrEmpty(r.Id)
				&& !string.IsNullOrEmpty(r.Type)
			);
	}
}
