using System;
using System.IO;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.SnapshotAndRestore.Restore
{
	public class RestoreApiTests : ApiTestBase<IntrusiveOperationCluster, IRestoreResponse, IRestoreRequest, RestoreDescriptor, RestoreRequest>
	{
		public RestoreApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
			if (!TestClient.Configuration.RunIntegrationTests) return;

			var createRepository = Client.CreateRepository(RepositoryName, r => r
				.FileSystem(fs => fs
					.Settings(Path.Combine(cluster.FileSystem.RepositoryPath, RepositoryName))
				)
			);
			if (!createRepository.IsValid)
				throw new Exception("Setup: failed to create snapshot repository");

			var getSnapshotResponse = Client.GetSnapshot(RepositoryName, SnapshotName);

			if (!getSnapshotResponse.IsValid && getSnapshotResponse.ApiCall.HttpStatusCode == 404 ||
				!getSnapshotResponse.Snapshots.Any())
			{
				var snapshot = Client.Snapshot(RepositoryName, SnapshotName, s => s
					.WaitForCompletion()
				);

				if (!snapshot.IsValid)
					throw new Exception($"Setup: snapshot failed. {snapshot.OriginalException}. {snapshot.ServerError?.Error}");
			}
		}

		protected override object ExpectJson { get; } = new
		{
			rename_pattern = "nest-(.+)",
			rename_replacement = "nest-restored-$1",
		};

		protected override Func<RestoreDescriptor, IRestoreRequest> Fluent => d => d
			.RenamePattern("nest-(.+)")
			.RenameReplacement("nest-restored-$1");

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override RestoreRequest Initializer => new RestoreRequest(RepositoryName, SnapshotName)
		{
			RenamePattern = "nest-(.+)",
			RenameReplacement = "nest-restored-$1"
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{RepositoryName}/{SnapshotName}/_restore";

		private static string RepositoryName { get; } = RandomString();
		private static string SnapshotName { get; } = RandomString();

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Restore(RepositoryName, SnapshotName, f),
			(client, f) => client.RestoreAsync(RepositoryName, SnapshotName, f),
			(client, r) => client.Restore(r),
			(client, r) => client.RestoreAsync(r)
		);

		protected override RestoreDescriptor NewDescriptor() => new RestoreDescriptor(RepositoryName, SnapshotName);
	}
}
