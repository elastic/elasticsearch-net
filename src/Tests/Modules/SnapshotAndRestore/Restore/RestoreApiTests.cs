using System;
using System.IO;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Restore
{
	// TODO HitsTheCorrectUrl is flaky here, sometimes it fails sometimes it passes
	[Collection(IntegrationContext.Indexing)]
	public class RestoreApiTests 
		: ApiIntegrationTestBase<IRestoreResponse, IRestoreRequest, RestoreDescriptor, RestoreRequest>
	{
		public RestoreApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
			_repositoryName = RandomString();
			_snapshotName = RandomString();
			if (!TestClient.Configuration.RunIntegrationTests) return;

			var createRepository = this.Client.CreateRepository(_repositoryName, r => r
				.FileSystem(fs => fs
					.Settings(Path.Combine(cluster.Node.RepositoryPath, _repositoryName))
				)
			);
			if (!createRepository.IsValid)
				throw new Exception("Setup: failed to create snapshot repository");
			var snapshot = this.Client.Snapshot(_repositoryName, _snapshotName, s => s
				.WaitForCompletion()
			);
			if (!snapshot.IsValid)
				throw new Exception("Setup: snapshot failed");
		}

		private readonly string _repositoryName;
		private readonly string _snapshotName;

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Restore(_repositoryName, _snapshotName, f),
			fluentAsync: (client, f) => client.RestoreAsync(_repositoryName, _snapshotName, f),
			request: (client, r) => client.Restore(r),
			requestAsync: (client, r) => client.RestoreAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_snapshot/{_repositoryName}/{_snapshotName}/_restore";
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			rename_pattern = "nest-(.+)",
			rename_replacement = "nest-restored-$1",
		};

		protected override bool SupportsDeserialization => false;

		protected override RestoreDescriptor NewDescriptor() => new RestoreDescriptor(_repositoryName, _snapshotName);

		protected override Func<RestoreDescriptor, IRestoreRequest> Fluent => d => d
			.RenamePattern("nest-(.+)")
			.RenameReplacement("nest-restored-$1");

		protected override RestoreRequest Initializer => new RestoreRequest(_repositoryName, _snapshotName)
		{
			RenamePattern = "nest-(.+)", 
			RenameReplacement = "nest-restored-$1"
		};
	}
}
