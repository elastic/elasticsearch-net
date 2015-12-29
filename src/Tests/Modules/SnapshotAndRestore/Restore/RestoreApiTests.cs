using System;
using System.IO;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Restore
{
	[Collection(IntegrationContext.Indexing)]
	public class RestoreApiTests : ApiTestBase<IRestoreResponse, IRestoreRequest, RestoreDescriptor, RestoreRequest>
	{
		public RestoreApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
			if (!TestClient.Configuration.RunIntegrationTests) return;

			var createRepository = this.Client.CreateRepository(RepositoryName, r => r
				.FileSystem(fs => fs
					.Settings(Path.Combine(cluster.Node.RepositoryPath, RepositoryName))
				)
			);
			if (!createRepository.IsValid)
				throw new Exception("Setup: failed to create snapshot repository");

		    var getSnapshotResponse = this.Client.GetSnapshot(RepositoryName, SnapshotName);

		    if ((!getSnapshotResponse.IsValid && getSnapshotResponse.ApiCall.HttpStatusCode == 404) || 
                !getSnapshotResponse.Snapshots.Any())
		    {
                    var snapshot = this.Client.Snapshot(RepositoryName, SnapshotName, s => s
                        .WaitForCompletion()
                    );

                    if (!snapshot.IsValid)
                        throw new Exception($"Setup: snapshot failed. {snapshot.OriginalException}. {snapshot.ServerError?.Error}");
		    }
		}

		private static string RepositoryName { get; } = RandomString();
		private static string SnapshotName { get; } = RandomString();

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Restore(RepositoryName, SnapshotName, f),
			fluentAsync: (client, f) => client.RestoreAsync(RepositoryName, SnapshotName, f),
			request: (client, r) => client.Restore(r),
			requestAsync: (client, r) => client.RestoreAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_snapshot/{RepositoryName}/{SnapshotName}/_restore";

		protected override object ExpectJson { get; } = new
		{
			rename_pattern = "nest-(.+)",
			rename_replacement = "nest-restored-$1",
		};

		protected override bool SupportsDeserialization => false;

		protected override RestoreDescriptor NewDescriptor() => new RestoreDescriptor(RepositoryName, SnapshotName);

		protected override Func<RestoreDescriptor, IRestoreRequest> Fluent => d => d
			.RenamePattern("nest-(.+)")
			.RenameReplacement("nest-restored-$1");

		protected override RestoreRequest Initializer => new RestoreRequest(RepositoryName, SnapshotName)
		{
			RenamePattern = "nest-(.+)", 
			RenameReplacement = "nest-restored-$1"
		};
	}
}
