using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cat.CatSnapshots
{
	[Collection(TypeOfCluster.Indexing)]
	public class CatSnapshotsApiTests : ApiIntegrationTestBase<ICatResponse<CatSnapshotsRecord>, ICatSnapshotsRequest, CatSnapshotsDescriptor, CatSnapshotsRequest>
	{
		private static readonly string SnapshotName = RandomString();
		private static readonly string SnapshotIndexName = RandomString();
		private static readonly string RepositoryName = RandomString();

		public CatSnapshotsApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

			var createIndex = this.Client.CreateIndex(SnapshotIndexName);
			this.Client.ClusterHealth(g => g.WaitForStatus(WaitForStatus.Yellow));
			client.Snapshot(RepositoryName, SnapshotName, s=>s.WaitForCompletion().Index(SnapshotIndexName));
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatSnapshots(RepositoryName, f),
			fluentAsync: (client, f) => client.CatSnapshotsAsync(RepositoryName, f),
			request: (client, r) => client.CatSnapshots(r),
			requestAsync: (client, r) => client.CatSnapshotsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_cat/snapshots/{RepositoryName}";

		protected override CatSnapshotsRequest Initializer => new CatSnapshotsRequest(RepositoryName);

		protected override void ExpectResponse(ICatResponse<CatSnapshotsRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.OnlyContain(r=>r.Status == "SUCCESS");
		}
	}
}
