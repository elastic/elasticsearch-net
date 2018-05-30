using System;
using System.IO;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Snapshot
{
	public class SnapshotCrudTests
		: CrudTestBase<IntrusiveOperationCluster, ISnapshotResponse, IGetSnapshotResponse, IAcknowledgedResponse, IDeleteSnapshotResponse>
	{
		private static readonly string SnapshotIndexName = Guid.NewGuid().ToString("N").Substring(8);

		public SnapshotCrudTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
			_repositoryLocation = Path.Combine(cluster.FileSystem.RepositoryPath, RandomString());
		}

		protected override void IntegrationSetup(IElasticClient client)
		{
			var create = this.Client.CreateRepository(_repositoryName, cr => cr
				.FileSystem(fs => fs
					.Settings(_repositoryLocation)
				)
			);

			if (!create.IsValid || !create.Acknowledged)
				throw new Exception("Setup: failed to create snapshot repository");

			var createIndex = this.Client.CreateIndex(SnapshotIndexName);
			var waitForIndex = this.Client.ClusterHealth(c=>c
				.WaitForStatus(WaitForStatus.Yellow)
				.Index(SnapshotIndexName)
			);
		}

		private string _repositoryLocation;
		private string _repositoryName = RandomString();

		protected override LazyResponses Create() => Calls<SnapshotDescriptor, SnapshotRequest, ISnapshotRequest, ISnapshotResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.Snapshot(_repositoryName, s, f),
			fluentAsync: (s, c, f) => c.SnapshotAsync(_repositoryName, s, f),
			request: (s, c, r) => c.Snapshot(r),
			requestAsync: (s, c, r) => c.SnapshotAsync(r)
		);

		protected SnapshotRequest CreateInitializer(string snapshotName) => new SnapshotRequest(_repositoryName, snapshotName)
		{
			WaitForCompletion = true,
			Indices = SnapshotIndexName
		};

		protected ISnapshotRequest CreateFluent(string snapshotName, SnapshotDescriptor d) => d
			.WaitForCompletion()
			.Indices(SnapshotIndexName);

		protected override LazyResponses Read() => Calls<GetSnapshotDescriptor, GetSnapshotRequest, IGetSnapshotRequest, IGetSnapshotResponse>(
			ReadInitializer,
			ReadFluent,
			fluent: (s, c, f) => c.GetSnapshot(_repositoryName, s, f),
			fluentAsync: (s, c, f) => c.GetSnapshotAsync(_repositoryName, s, f),
			request: (s, c, r) => c.GetSnapshot(r),
			requestAsync: (s, c, r) => c.GetSnapshotAsync(r)
		);

		protected GetSnapshotRequest ReadInitializer(string snapshotName) => new GetSnapshotRequest(_repositoryName, snapshotName);
		protected IGetSnapshotRequest ReadFluent(string snapshotName, GetSnapshotDescriptor d) => null;

		protected override LazyResponses Delete() => Calls<DeleteSnapshotDescriptor, DeleteSnapshotRequest, IDeleteSnapshotRequest, IDeleteSnapshotResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.DeleteSnapshot(_repositoryName, s, f),
			fluentAsync: (s, c, f) => c.DeleteSnapshotAsync(_repositoryName, s, f),
			request: (s, c, r) => c.DeleteSnapshot(r),
			requestAsync: (s, c, r) => c.DeleteSnapshotAsync(r)
		);

		protected DeleteSnapshotRequest DeleteInitializer(string snapshotName) => new DeleteSnapshotRequest(_repositoryName, snapshotName);
		protected IDeleteSnapshotRequest DeleteFluent(string snapshotName, DeleteSnapshotDescriptor d) => null;

		protected override LazyResponses Update() => LazyResponses.Empty;

		protected override void ExpectAfterCreate(IGetSnapshotResponse response)
		{
			response.Snapshots.Should().HaveCount(1);
			var snapshot = response.Snapshots.FirstOrDefault();
			snapshot.Should().NotBeNull();
		}

		protected override void ExpectDeleteNotFoundResponse(IDeleteSnapshotResponse response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(404);
			response.ServerError.Error.Reason.Should().Contain("missing");
		}
	}
}
