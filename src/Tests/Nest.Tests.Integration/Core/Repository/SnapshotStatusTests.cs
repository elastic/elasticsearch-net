using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Repository
{
	[TestFixture]
	public class SnapshotStatusTests : IntegrationTests
	{
		[Test]
		public void SnapshotRestoreFullPath()
		{
			var repositoryName = ElasticsearchConfiguration.NewUniqueIndexName();

			var createReposResult = this.Client.CreateRepository(repositoryName, r => r
				.FileSystem(@"local\\path", o => o
					.Compress()
					.ConcurrentStreams(10)
				)
			);
			createReposResult.IsValid.Should().BeTrue();
			createReposResult.Acknowledged.Should().BeTrue();

			var backupName = ElasticsearchConfiguration.NewUniqueIndexName();
			var snapshotResponse = this.Client.Snapshot(repositoryName, backupName, selector: f => f
				.Index(ElasticsearchConfiguration.DefaultIndex)
				.IgnoreUnavailable()
			);
			snapshotResponse.IsValid.Should().BeTrue();
			snapshotResponse.Accepted.Should().BeTrue();

			var status = this.Client.SnapshotStatus(new SnapshotStatusRequest(repositoryName, backupName));

			status.Snapshots.Should().NotBeEmpty();

			var snapshot = status.Snapshots.First();
			snapshot.Repository.Should().Be(repositoryName);
			snapshot.Snapshot.Should().Be(backupName);
			snapshot.State.Should().NotBeNullOrWhiteSpace();
			snapshot.ShardsStats.Should().NotBeNull();
			snapshot.ShardsStats.Total.Should().Be(ElasticsearchConfiguration.NumberOfShards);
			snapshot.Stats.Should().NotBeNull();
			snapshot.Indices.Should().NotBeNull().And.HaveCount(1);
		
		}
		[Test]
		public void SnapshotRestoreOnlyRepository()
		{
			var repositoryName = ElasticsearchConfiguration.NewUniqueIndexName();

			var createReposResult = this.Client.CreateRepository(repositoryName, r => r
				.FileSystem(@"local\\path", o => o
					.Compress()
					.ConcurrentStreams(10)
				)
			);
			createReposResult.IsValid.Should().BeTrue();
			createReposResult.Acknowledged.Should().BeTrue();

			var backupName = ElasticsearchConfiguration.NewUniqueIndexName();
			var snapshotResponse = this.Client.Snapshot(repositoryName, backupName, selector: f => f
				.Index(ElasticsearchConfiguration.DefaultIndex)
				.IgnoreUnavailable()
			);
			snapshotResponse.IsValid.Should().BeTrue();
			snapshotResponse.Accepted.Should().BeTrue();
			
			var status = this.Client.SnapshotStatus(new SnapshotStatusRequest(repositoryName));

			status.Snapshots.Should().NotBeEmpty();

			var snapshot = status.Snapshots.First();
			snapshot.Repository.Should().Be(repositoryName);
			snapshot.Snapshot.Should().Be(backupName);
			snapshot.State.Should().NotBeNullOrWhiteSpace();
			snapshot.ShardsStats.Should().NotBeNull();
			snapshot.ShardsStats.Total.Should().Be(ElasticsearchConfiguration.NumberOfShards);
			snapshot.Stats.Should().NotBeNull();
			snapshot.Indices.Should().NotBeNull().And.HaveCount(1);
		
		}
		
	}
}
