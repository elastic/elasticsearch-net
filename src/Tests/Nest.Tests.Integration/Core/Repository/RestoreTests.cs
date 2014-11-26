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
	    private string _indexName;
	    private string _repositoryName;
	    private string _backupName;
	    private ElasticsearchProject _elasticsearchProject;

	    [SetUp]
	    public void Setup()
	    {
	        _indexName = ElasticsearchConfiguration.NewUniqueIndexName();
	        _repositoryName = ElasticsearchConfiguration.NewUniqueIndexName();
	        _backupName = ElasticsearchConfiguration.NewUniqueIndexName();

	        _elasticsearchProject = new ElasticsearchProject()
            {
                Id = 1337,
                Name = "Coboles",
                Content = "COBOL elasticsearch client"
	        };

	        Client.Index(_elasticsearchProject, i => i.Index(_indexName).Refresh(true));

	        this.Client.CreateRepository(_repositoryName, r => r
	            .FileSystem(@"local\\path", o => o
	                .Compress()
	                .ConcurrentStreams(10)));
	    }

	    [TearDown]
	    public void TearDown()
	    {
            var deleteReposResult = this.Client.DeleteRepository(_repositoryName);
	    }

	    [Test]
		public void SnapshotRestore()
		{
			var snapshotResponse = this.Client.Snapshot(_repositoryName, _backupName, selector: f => f
				.Index(_indexName)
				.WaitForCompletion(true)
				.IgnoreUnavailable()
				.Partial());
			snapshotResponse.IsValid.Should().BeTrue();
			snapshotResponse.Accepted.Should().BeTrue();
			snapshotResponse.Snapshot.Should().NotBeNull();
			snapshotResponse.Snapshot.EndTimeInMilliseconds.Should().BeGreaterThan(0);
			snapshotResponse.Snapshot.StartTime.Should().BeAfter(DateTime.UtcNow.AddDays(-1));

			var d = ElasticsearchConfiguration.DefaultIndex;
			var restoreResponse = this.Client.Restore(_repositoryName, _backupName, r => r
				.WaitForCompletion(true)
				.RenamePattern(d + "_(.+)")
				.RenameReplacement(d + "_restored_$1")
				.Index(_indexName)
				.IgnoreUnavailable(true));

			var restoredIndexName = _indexName.Replace(d +  "_", d + "_restored_");
			restoreResponse.IsValid.Should().BeTrue();
			restoreResponse.Snapshot.Should().NotBeNull();
			restoreResponse.Snapshot.Name.Should().Be(_backupName);
			restoreResponse.Snapshot.Indices.Should().Equal(new string[] { restoredIndexName });

			var indexExistsResponse = this.Client.IndexExists(f => f.Index(restoredIndexName));
			indexExistsResponse.Exists.Should().BeTrue();

			var coboles = this.Client.Source<ElasticsearchProject>(_elasticsearchProject.Id, restoredIndexName);
			coboles.Should().NotBeNull();
			coboles.Name.Should().Be(_elasticsearchProject.Name);
		}

	    [Test]
	    public void SnapshotRestoreObservable()
	    {
	        var snapshotObservable = this.Client.SnapshotObservable(_repositoryName, _backupName, descriptor => descriptor
	            .Index(_indexName));

            var snapshotObserver = new Observer<ISnapshotStatusResponse>(
	                onNext: r =>
                    {
                        var snapshot = r.Snapshots.ElementAt(0);
                        Assert.IsTrue(r.IsValid);
                        Assert.AreEqual(1, r.Snapshots.Count());
                        CollectionAssert.Contains(snapshot.Indices.Keys, _indexName);
	                },
	                onError: e => Assert.Fail(e.Message),
	                completed: () =>
	                {
                        var getSnapshotResponse = this.Client.GetSnapshot(_repositoryName, _backupName, descriptor => descriptor);
                        var snapshot = getSnapshotResponse.Snapshots.ElementAt(0);
                        Assert.IsTrue(getSnapshotResponse.IsValid);
                        Assert.AreEqual(1, getSnapshotResponse.Snapshots.Count());
                        Assert.AreEqual("SUCCESS", snapshot.State);
                        CollectionAssert.Contains(snapshot.Indices, _indexName);
	                }
                );

            snapshotObservable.Subscribe(snapshotObserver);

            var defaultIndex = ElasticsearchConfiguration.DefaultIndex;
            var restoreObservable = this.Client.RestoreObservable(_repositoryName, _backupName, r => r
                .RenamePattern(defaultIndex + "_(.+)")
                .RenameReplacement(defaultIndex + "_restored_$1")
                .Index(_indexName)
                .IgnoreUnavailable(true));

            var restoreObserver = new Observer<IRecoveryStatusResponse>(
	            onNext: r =>
	            {
	                var index = r.Indices.FirstOrDefault();
	                Assert.AreEqual(1, r.Indices.Count);
	            },
	            onError: e => Assert.Fail(e.Message),
	            completed: () =>
                {
                    var restoredIndexName = _indexName.Replace(defaultIndex + "_", defaultIndex + "_restored_");
                    var restoredIndexExistsResponse = this.Client.IndexExists(f => f.Index(restoredIndexName));
                    restoredIndexExistsResponse.Exists.Should().BeTrue();

                    var indexContent = this.Client.Source<ElasticsearchProject>(_elasticsearchProject.Id, restoredIndexName);
                    indexContent.Should().NotBeNull();
                    indexContent.Name.Should().Be(_elasticsearchProject.Name);        
	            }
	            );

	        restoreObservable.Subscribe(restoreObserver);
	    }
	}
}
