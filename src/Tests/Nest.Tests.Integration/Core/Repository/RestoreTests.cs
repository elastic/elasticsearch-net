using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Repository
{
	[TestFixture]
	public class RestoreTests : IntegrationTests
	{
		[Test]
		public void SnapshotRestore()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var repositoryName = ElasticsearchConfiguration.NewUniqueIndexName();
			var elasticsearchProject = new ElasticsearchProject()
			{
				Id = 1337,
				Name = "Coboles",
				Content = "COBOL elasticsearch client"
			};
			Client.Index(elasticsearchProject, i=>i.Index(indexName).Refresh(true));


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
				.Index(indexName)
				.WaitForCompletion(true)
				.IgnoreUnavailable()
				.Partial());
			snapshotResponse.IsValid.Should().BeTrue();
			snapshotResponse.Accepted.Should().BeTrue();
			snapshotResponse.Snapshot.Should().NotBeNull();
			snapshotResponse.Snapshot.EndTimeInMilliseconds.Should().BeGreaterThan(0);
			snapshotResponse.Snapshot.StartTime.Should().BeAfter(DateTime.UtcNow.AddDays(-1));

			var d = ElasticsearchConfiguration.DefaultIndex;
			var restoreResponse = this.Client.Restore(repositoryName, backupName, r => r
				.WaitForCompletion(true)
				.RenamePattern(d + "_(.+)")
				.RenameReplacement(d + "_restored_$1")
				.Index(indexName)
				.IgnoreUnavailable(true));

			var restoredIndexName = indexName.Replace(d +  "_", d + "_restored_");
			restoreResponse.IsValid.Should().BeTrue();
			restoreResponse.Snapshot.Should().NotBeNull();
			restoreResponse.Snapshot.Name.Should().Be(backupName);
			restoreResponse.Snapshot.Indices.Should().Equal(new string[] { restoredIndexName });

			var indexExistsResponse = this.Client.IndexExists(f => f.Index(restoredIndexName));
			indexExistsResponse.Exists.Should().BeTrue();

			var coboles = this.Client.Source<ElasticsearchProject>(elasticsearchProject.Id, restoredIndexName);
			coboles.Should().NotBeNull();
			coboles.Name.Should().Be(elasticsearchProject.Name);


			var deleteReposResult = this.Client.DeleteRepository(repositoryName);
			deleteReposResult.IsValid.Should().BeTrue();
			deleteReposResult.Acknowledged.Should().BeTrue();
		}
		
	}
}
