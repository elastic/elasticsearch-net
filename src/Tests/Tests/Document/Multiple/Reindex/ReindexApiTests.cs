using System;
using System.Linq;
using System.Threading;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;

namespace Tests.Document.Multiple.Reindex
{
	public class ReindexCluster : ClientTestClusterBase
	{
		protected override void SeedCluster()
		{
			var seeder = new DefaultSeeder(Client);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}

	public class ManualReindexCluster : ClientTestClusterBase
	{
		protected override void SeedCluster()
		{
			var seeder = new DefaultSeeder(Client);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}

	public class ReindexApiTests : IClusterFixture<ManualReindexCluster>
	{
		private readonly IElasticClient _client;
		private readonly IObservable<IBulkAllResponse> _reindexManyTypesResult;
		private readonly IObservable<IBulkAllResponse> _reindexProjectionResult;
		private readonly IObservable<IBulkAllResponse> _reindexSingleTypeResult;

		public ReindexApiTests(ManualReindexCluster cluster)
		{
			_client = cluster.Client;

			// create a couple of projects
			var projects = Project.Generator.Generate(2);
			var indexProjectsResponse = _client.IndexMany(projects, IndexName);
			_client.Refresh(IndexName);

			// create a thousand commits and associate with the projects
			var commits = CommitActivity.Generator.Generate(5000);
			var bb = new BulkDescriptor();
			for (var i = 0; i < commits.Count; i++)
			{
				var commit = commits[i];
				var project = i % 2 == 0
					? projects[0].Name
					: projects[1].Name;

				bb.Index<CommitActivity>(bi => bi
					.Document(commit)
					.Index(IndexName)
					.Id(commit.Id)
					.Routing(project)
					.Parent(project)
				);
			}

			var bulkResult = _client.Bulk(b => bb);
			bulkResult.ShouldBeValid();

			_client.Refresh(IndexName);

			_reindexManyTypesResult = _client.Reindex<ILazyDocument>(r => r
				.BackPressureFactor(10)
				.ScrollAll("1m", 2, s => s
					.Search(ss => ss
						.Index(IndexName)
						.AllTypes()
					)
					.MaxDegreeOfParallelism(4)
				)
				.BulkAll(b => b
					.Index(NewManyTypesIndexName)
					.Size(100)
					.MaxDegreeOfParallelism(2)
					.RefreshOnCompleted()
				)
			);
			_reindexSingleTypeResult = _client.Reindex<Project>(IndexName, NewSingleTypeIndexName);
			_reindexProjectionResult =
				_client.Reindex<CommitActivity, CommitActivityVersion2>(IndexName, NewProjectionIndex, p => new CommitActivityVersion2(p));
		}

		private static string IndexName { get; } = "project";

		private static string NewManyTypesIndexName { get; } = $"project-many-{Guid.NewGuid().ToString("N").Substring(8)}";

		private static string NewProjectionIndex { get; } = $"project-projection-{Guid.NewGuid().ToString("N").Substring(8)}";

		private static string NewSingleTypeIndexName { get; } = $"project-single-{Guid.NewGuid().ToString("N").Substring(8)}";

		[I] public void ReturnsExpectedResponse()
		{
			var observableWait = new CountdownEvent(3);

			Exception ex = null;
			var manyTypesObserver = new ReindexObserver(
				onError: (e) =>
				{
					ex = e;
					observableWait.Signal();
				},
				onCompleted: () => ReindexManyTypesCompleted(observableWait)
			);

			_reindexManyTypesResult.Subscribe(manyTypesObserver);
			_reindexManyTypesResult.Wait(TimeSpan.FromMinutes(5), r => { });


			var singleTypeObserver = new ReindexObserver(
				onError: (e) =>
				{
					ex = e;
					observableWait.Signal();
				},
				onCompleted: () => ReindexSingleTypeCompleted(observableWait)
			);
			_reindexSingleTypeResult.Subscribe(singleTypeObserver);

			var projectionObserver = new ReindexObserver(
				onError: (e) =>
				{
					ex = e;
					observableWait.Signal();
				},
				onCompleted: () => ProjectionCompleted(observableWait)
			);
			_reindexProjectionResult.Subscribe(projectionObserver);

			observableWait.Wait(TimeSpan.FromMinutes(3));
			if (ex != null) throw ex;
		}

		private void ProjectionCompleted(CountdownEvent handle)
		{
			var refresh = _client.Refresh(NewProjectionIndex);
			var originalIndexCount = _client.Count<CommitActivity>(c => c.Index(IndexName));

			// new index should only contain project document types
			var newIndexSearch = _client.Search<CommitActivity>(c => c.Index(NewProjectionIndex));

			originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexSearch.Total);

			newIndexSearch.Documents.Should().OnlyContain(c => c.Id.EndsWith("-projected"));

			handle.Signal();
		}

		private void ReindexSingleTypeCompleted(CountdownEvent handle)
		{
			var refresh = _client.Refresh(NewSingleTypeIndexName);
			var originalIndexCount = _client.Count<Project>(c => c.Index(IndexName));

			// new index should only contain project document types
			var newIndexCount = _client.Count<Project>(c => c.Index(NewSingleTypeIndexName).AllTypes());

			originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

			handle.Signal();
		}

		private void ReindexManyTypesCompleted(CountdownEvent handle)
		{
			var refresh = _client.Refresh(NewManyTypesIndexName);
			var originalIndexCount = _client.Count<CommitActivity>(c => c.Index(IndexName));
			var newIndexCount = _client.Count<CommitActivity>(c => c.Index(NewManyTypesIndexName));

			originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

			var scroll = "20s";

			var searchResult = _client.Search<CommitActivity>(s => s
				.Index(NewManyTypesIndexName)
				.From(0)
				.Size(100)
				.Query(q => q.MatchAll())
				.Scroll(scroll)
			);

			do
			{
				var result = searchResult;
				searchResult = _client.Scroll<CommitActivity>(scroll, result.ScrollId);
				foreach (var hit in searchResult.Hits)
				{
					hit.Parent.Should().NotBeNullOrEmpty();
					hit.Routing.Should().NotBeNullOrEmpty();
				}
			} while (searchResult.IsValid && searchResult.Documents.Any());
			handle.Signal();
		}

		public class CommitActivityVersion2
		{
			public CommitActivityVersion2(CommitActivity commit)
			{
				ProjectName = commit.ProjectName + "-projected";
				Id = commit.Id + "-projected";
				Committer = commit.Committer;
			}

			public Developer Committer { get; }
			public string Id { get; }
			public string ProjectName { get; }
		}
	}
}
