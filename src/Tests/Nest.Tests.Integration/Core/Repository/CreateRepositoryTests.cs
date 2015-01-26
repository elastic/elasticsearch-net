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
		public void CreateAndValidateAndDeleteRepository_ThenSnapshotWithWait()
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

			// Repository verification added in ES 1.4
			if (ElasticsearchConfiguration.CurrentVersion > new Version("1.3.9"))
			{
				var validateResponse = this.Client.VerifyRepository(new VerifyRepositoryRequest(repositoryName));
				validateResponse.IsValid.Should().BeTrue();

				validateResponse.Nodes.Should().NotBeEmpty();
				var kv = validateResponse.Nodes.First();
				kv.Key.Should().NotBeNullOrWhiteSpace();
				kv.Value.Should().NotBeNull();
				kv.Value.Name.Should().NotBeNullOrWhiteSpace();
			}

			var backupName = ElasticsearchConfiguration.NewUniqueIndexName();
			var snapshotResponse = this.Client.Snapshot(repositoryName, backupName, selector: f => f
				.WaitForCompletion(true)
				.Index(ElasticsearchConfiguration.NewUniqueIndexName())
				.IgnoreUnavailable()
				.Partial());
			snapshotResponse.IsValid.Should().BeTrue();
			snapshotResponse.Accepted.Should().BeTrue();
			snapshotResponse.Snapshot.Should().NotBeNull();
			snapshotResponse.Snapshot.EndTimeInMilliseconds.Should().BeGreaterThan(0);
			snapshotResponse.Snapshot.StartTime.Should().BeAfter(DateTime.UtcNow.AddDays(-1));

			var getSnapshotResponse = this.Client.GetSnapshot(repositoryName, backupName);
			getSnapshotResponse.IsValid.Should().BeTrue();
			getSnapshotResponse.Snapshots.Should().NotBeEmpty();
			var snapShot = getSnapshotResponse.Snapshots.First();
			snapShot.StartTime.Should().BeAfter(DateTime.UtcNow.AddDays(-1));
			
			var getAllSnapshotResponse = this.Client.GetSnapshot(repositoryName, "_all");
			getAllSnapshotResponse.IsValid.Should().BeTrue();
			getAllSnapshotResponse.Snapshots.Should().NotBeEmpty();
			getAllSnapshotResponse.Snapshots.Count().Should().BeGreaterOrEqualTo(1);
			
			var deleteReposResult = this.Client.DeleteRepository(repositoryName);
			deleteReposResult.IsValid.Should().BeTrue();
			deleteReposResult.Acknowledged.Should().BeTrue();
		}

		[Test]
		public void CreateAndDeleteRepository_ThenSnapshotWithoutWait()
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
				.Index(ElasticsearchConfiguration.NewUniqueIndexName())
				.IgnoreUnavailable()
				.Partial());
			snapshotResponse.IsValid.Should().BeTrue();
			snapshotResponse.Accepted.Should().BeTrue();
			snapshotResponse.Snapshot.Should().BeNull();

			var deleteSnapshotReponse = this.Client.DeleteSnapshot(repositoryName, backupName);
			deleteSnapshotReponse.IsValid.Should().BeTrue();

			var deleteReposResult = this.Client.DeleteRepository(repositoryName);
			deleteReposResult.IsValid.Should().BeTrue();
			deleteReposResult.Acknowledged.Should().BeTrue();
		}
	}
}
