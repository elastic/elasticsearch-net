// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Elastic.Transport;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Restore
{
	public class RestoreApiTests : ApiTestBase<IntrusiveOperationCluster, RestoreResponse, IRestoreRequest, RestoreDescriptor, RestoreRequest>
	{
		[SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
		public RestoreApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
			if (!TestClient.Configuration.RunIntegrationTests) return;

			var createRepository = Client.Snapshot.CreateRepository(RepositoryName, r => r
				.FileSystem(fs => fs
					.Settings(Path.Combine(cluster.FileSystem.RepositoryPath, RepositoryName))
				)
			);
			if (!createRepository.IsValid)
				throw new Exception("Setup: failed to create snapshot repository");

			var getSnapshotResponse = Client.Snapshot.Get(RepositoryName, SnapshotName);

			if (!getSnapshotResponse.IsValid && getSnapshotResponse.ApiCall.HttpStatusCode == 404 ||
				!getSnapshotResponse.Snapshots.Any())
			{
				var snapshot = Client.Snapshot.Snapshot(RepositoryName, SnapshotName, s => s
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
			(client, f) => client.Snapshot.Restore(RepositoryName, SnapshotName, f),
			(client, f) => client.Snapshot.RestoreAsync(RepositoryName, SnapshotName, f),
			(client, r) => client.Snapshot.Restore(r),
			(client, r) => client.Snapshot.RestoreAsync(r)
		);

		protected override RestoreDescriptor NewDescriptor() => new RestoreDescriptor(RepositoryName, SnapshotName);
	}
}
