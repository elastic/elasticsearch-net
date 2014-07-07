using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Repository
{
	[TestFixture]
	public class CreateRepositoryTests : IntegrationTests
	{
		[Test]
		public void CreateAndDeleteRepository_ThenSnapshotWithWait()
		{
			var repositoryName = ElasticsearchConfiguration.NewUniqueIndexName();
			var createReposResult = this._client.CreateRepository(repositoryName, r => r
				.FileSystem(@"local\\path", o => o
					.Compress()
					.ConcurrentStreams(10)
				)
			);
			createReposResult.IsValid.Should().BeTrue();
			createReposResult.Acknowledged.Should().BeTrue();

			var backupName = ElasticsearchConfiguration.NewUniqueIndexName();
			var snapshotResponse = this._client.Snapshot(repositoryName, backupName, selector: f => f
				.WaitForCompletion(true)
				.Index(ElasticsearchConfiguration.NewUniqueIndexName())
				.IgnoreUnavailable()
				.Partial());
			snapshotResponse.IsValid.Should().BeTrue();
			snapshotResponse.Accepted.Should().BeTrue();
			snapshotResponse.Snapshot.Should().NotBeNull();
			snapshotResponse.Snapshot.EndTimeInMilliseconds.Should().BeGreaterThan(0);
			snapshotResponse.Snapshot.StartTime.Should().BeAfter(DateTime.UtcNow.AddDays(-1));

			var getSnapshotResponse = this._client.GetSnapshot(repositoryName, backupName);
			getSnapshotResponse.IsValid.Should().BeTrue();
			getSnapshotResponse.Snapshots.Should().NotBeEmpty();
			var snapShot = getSnapshotResponse.Snapshots.First();
			snapShot.StartTime.Should().BeAfter(DateTime.UtcNow.AddDays(-1));
			
			var getAllSnapshotResponse = this._client.GetSnapshot(repositoryName, "_all");
			getAllSnapshotResponse.IsValid.Should().BeTrue();
			getAllSnapshotResponse.Snapshots.Should().NotBeEmpty();
			getAllSnapshotResponse.Snapshots.Count().Should().BeGreaterOrEqualTo(1);
			
			var deleteReposResult = this._client.DeleteRepository(repositoryName);
			deleteReposResult.IsValid.Should().BeTrue();
			deleteReposResult.Acknowledged.Should().BeTrue();
		}
		
		[Test]
		public void CreateAndDeleteRepository_ThenSnapshotWithoutWait()
		{
			var repositoryName = ElasticsearchConfiguration.NewUniqueIndexName();
			var createReposResult = this._client.CreateRepository(repositoryName, r => r
				.FileSystem(@"local\\path", o => o
					.Compress()
					.ConcurrentStreams(10)
				)
			);
			createReposResult.IsValid.Should().BeTrue();
			createReposResult.Acknowledged.Should().BeTrue();

			var backupName = ElasticsearchConfiguration.NewUniqueIndexName();
			var snapshotResponse = this._client.Snapshot(repositoryName, backupName, selector: f => f
				.Index(ElasticsearchConfiguration.NewUniqueIndexName())
				.IgnoreUnavailable()
				.Partial());
			snapshotResponse.IsValid.Should().BeTrue();
			snapshotResponse.Accepted.Should().BeTrue();
			snapshotResponse.Snapshot.Should().BeNull();

			var deleteSnapshotReponse = this._client.DeleteSnapshot(repositoryName, backupName);
			deleteSnapshotReponse.IsValid.Should().BeTrue();

			var deleteReposResult = this._client.DeleteRepository(repositoryName);
			deleteReposResult.IsValid.Should().BeTrue();
			deleteReposResult.Acknowledged.Should().BeTrue();
		}
	}
}
