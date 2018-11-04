using System;
using System.IO;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatSnapshots
{
	[SkipVersion("<2.1.0", "")]
	public class CatSnapshotsApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ICatResponse<CatSnapshotsRecord>, ICatSnapshotsRequest, CatSnapshotsDescriptor,
			CatSnapshotsRequest>
	{
		private static readonly string RepositoryName = RandomString();
		private static readonly string SnapshotIndexName = RandomString();
		private static readonly string SnapshotName = RandomString();

		public CatSnapshotsApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override CatSnapshotsRequest Initializer => new CatSnapshotsRequest(RepositoryName);
		protected override string UrlPath => $"/_cat/snapshots/{RepositoryName}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (!TestClient.Configuration.RunIntegrationTests) return;

			var repositoryLocation = Path.Combine(Cluster.FileSystem.RepositoryPath, RandomString());

			var create = Client.CreateRepository(RepositoryName, cr => cr
				.FileSystem(fs => fs
					.Settings(repositoryLocation)
				)
			);

			if (!create.IsValid || !create.Acknowledged)
				throw new Exception("Setup: failed to create snapshot repository");

			var createIndex = Client.CreateIndex(SnapshotIndexName);
			Client.ClusterHealth(g => g.WaitForStatus(WaitForStatus.Yellow).Index(SnapshotIndexName));
			client.Snapshot(RepositoryName, SnapshotName, s => s.WaitForCompletion().Index(SnapshotIndexName));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatSnapshots(RepositoryName, f),
			(client, f) => client.CatSnapshotsAsync(RepositoryName, f),
			(client, r) => client.CatSnapshots(r),
			(client, r) => client.CatSnapshotsAsync(r)
		);

		protected override void ExpectResponse(ICatResponse<CatSnapshotsRecord> response) =>
			response.Records.Should().NotBeEmpty().And.OnlyContain(r => r.Status == "SUCCESS");
	}
}
