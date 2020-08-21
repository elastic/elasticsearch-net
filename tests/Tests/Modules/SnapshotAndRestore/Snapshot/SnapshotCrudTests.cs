// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Snapshot
{
	[SkipVersion(">=8.0.0-SNAPSHOT", "TODO investigate")]
	public class SnapshotCrudTests
		: CrudTestBase<IntrusiveOperationCluster, SnapshotResponse, GetSnapshotResponse, AcknowledgedResponseBase, DeleteSnapshotResponse>
	{
		private static readonly string SnapshotIndexName = Guid.NewGuid().ToString("N").Substring(8);

		private readonly string _repositoryLocation;
		private readonly string _repositoryName = RandomString();

		public SnapshotCrudTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) =>
			_repositoryLocation = Path.Combine(cluster.FileSystem.RepositoryPath, RandomString());

		protected override void IntegrationSetup(IElasticClient client)
		{
			var create = Client.Snapshot.CreateRepository(_repositoryName, cr => cr
				.FileSystem(fs => fs
					.Settings(_repositoryLocation)
				)
			);

			if (!create.IsValid || !create.Acknowledged)
				throw new Exception("Setup: failed to create snapshot repository");

			var createIndex = Client.Indices.Create(SnapshotIndexName);
			var waitForIndex = Client.Cluster.Health(SnapshotIndexName, c => c
				.WaitForStatus(WaitForStatus.Yellow)
			);
		}

		protected override LazyResponses Create() => Calls<SnapshotDescriptor, SnapshotRequest, ISnapshotRequest, SnapshotResponse>(
			CreateInitializer,
			CreateFluent,
			(s, c, f) => c.Snapshot.Snapshot(_repositoryName, s, f),
			(s, c, f) => c.Snapshot.SnapshotAsync(_repositoryName, s, f),
			(s, c, r) => c.Snapshot.Snapshot(r),
			(s, c, r) => c.Snapshot.SnapshotAsync(r)
		);

		protected SnapshotRequest CreateInitializer(string snapshotName) => new SnapshotRequest(_repositoryName, snapshotName)
		{
			WaitForCompletion = true,
			Indices = SnapshotIndexName
		};

		protected ISnapshotRequest CreateFluent(string snapshotName, SnapshotDescriptor d) => d
			.WaitForCompletion()
			.Indices(SnapshotIndexName);

		protected override LazyResponses Read() => Calls<GetSnapshotDescriptor, GetSnapshotRequest, IGetSnapshotRequest, GetSnapshotResponse>(
			ReadInitializer,
			ReadFluent,
			(s, c, f) => c.Snapshot.Get(_repositoryName, s, f),
			(s, c, f) => c.Snapshot.GetAsync(_repositoryName, s, f),
			(s, c, r) => c.Snapshot.Get(r),
			(s, c, r) => c.Snapshot.GetAsync(r)
		);

		protected GetSnapshotRequest ReadInitializer(string snapshotName) => new GetSnapshotRequest(_repositoryName, snapshotName);

		protected IGetSnapshotRequest ReadFluent(string snapshotName, GetSnapshotDescriptor d) => null;

		protected override LazyResponses Delete() =>
			Calls<DeleteSnapshotDescriptor, DeleteSnapshotRequest, IDeleteSnapshotRequest, DeleteSnapshotResponse>(
				DeleteInitializer,
				DeleteFluent,
				(s, c, f) => c.Snapshot.Delete(_repositoryName, s, f),
				(s, c, f) => c.Snapshot.DeleteAsync(_repositoryName, s, f),
				(s, c, r) => c.Snapshot.Delete(r),
				(s, c, r) => c.Snapshot.DeleteAsync(r)
			);

		protected DeleteSnapshotRequest DeleteInitializer(string snapshotName) => new DeleteSnapshotRequest(_repositoryName, snapshotName);

		protected IDeleteSnapshotRequest DeleteFluent(string snapshotName, DeleteSnapshotDescriptor d) => null;

		protected override LazyResponses Update() => LazyResponses.Empty;

		protected override void ExpectAfterCreate(GetSnapshotResponse response)
		{
			response.Snapshots.Should().HaveCount(1);
			var snapshot = response.Snapshots.FirstOrDefault();
			snapshot.Should().NotBeNull();
		}

		protected override void ExpectDeleteNotFoundResponse(DeleteSnapshotResponse response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(404);
			response.ServerError.Error.Reason.Should().Contain("missing");
		}
	}
}
