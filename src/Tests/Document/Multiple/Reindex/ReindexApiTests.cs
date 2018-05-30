using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Multiple.Reindex
{
	public class ReindexCluster : ClientTestClusterBase
	{
		protected override void SeedCluster()
		{
			var seeder = new DefaultSeeder(this.Client);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}

	public class ManualReindexCluster : ClientTestClusterBase
	{
		protected override void SeedCluster()
		{
			var seeder = new DefaultSeeder(this.Client);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}

	public class ReindexApiTests : SerializationTestBase, IClusterFixture<ManualReindexCluster>
	{
		private readonly IObservable<IBulkAllResponse> _reindexManyTypesResult;
		private readonly IObservable<IBulkAllResponse> _reindexSingleTypeResult;
		private readonly IObservable<IBulkAllResponse> _reindexProjectionResult;
		private readonly IElasticClient _client;

		private static string NewManyTypesIndexName { get; } = $"project-many-{Guid.NewGuid().ToString("N").Substring(8)}";

		private static string NewSingleTypeIndexName { get; } = $"project-single-{Guid.NewGuid().ToString("N").Substring(8)}";

		private static string NewProjectionIndex { get; } = $"project-projection-{Guid.NewGuid().ToString("N").Substring(8)}";

		private static string IndexName { get; } = "project";

		public ReindexApiTests(ManualReindexCluster cluster)
		{
			this._client = cluster.Client;

			// create a couple of projects
			var projects = Project.Generator.Generate(2);
			var indexProjectsResponse = this._client.IndexMany(projects, IndexName);
			this._client.Refresh(IndexName);

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
					.Document(UpdateJoin(commit, project))
					.Index(IndexName)
					.Id(commit.Id)
					.Routing(project)
				);
			}
            CommitActivity UpdateJoin(CommitActivity a, string p)
            {
                a.ProjectName = p;
	            return a;
            }

			var bulkResult = this._client.Bulk(b => bb);
			bulkResult.ShouldBeValid();

			this._client.Refresh(IndexName);

			this._reindexManyTypesResult = this._client.Reindex<ILazyDocument>(r => r
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
			this._reindexSingleTypeResult = this._client.Reindex<Project>(IndexName, NewSingleTypeIndexName);
			this._reindexProjectionResult = this._client.Reindex<CommitActivity, CommitActivityVersion2>(
				IndexName, NewProjectionIndex, p =>  new CommitActivityVersion2(p));
		}

		public class CommitActivityVersion2
		{
			public string Id { get; }
			public string ProjectName { get; }
			public Developer Committer { get; }
			public JoinField Join { get; set; }

			public CommitActivityVersion2(CommitActivity commit)
			{
				this.Join = commit.Join;
				this.ProjectName = commit.ProjectName + "-projected";
				this.Id = commit.Id + "-projected";
				this.Committer = commit.Committer;
			}

		}

		[I] public void ReturnsExpectedResponse()
		{
			Exception ex = null;
			CountdownEvent observableWait = null;
			var reindexRoutines = new List<Action>
			{
				() => ReindexMany(GetSignal, Signal),
				() => ReindexSingleType(GetSignal, Signal),
				() => ReindexProjection(GetSignal, Signal)
			};
			observableWait = new CountdownEvent(reindexRoutines.Count);
			foreach (var a in reindexRoutines) a();

			observableWait.Wait(TimeSpan.FromMinutes(3));
			if (ex != null) throw ex;

			void Signal(Exception e) { ex = e; observableWait.Signal(); }
			CountdownEvent GetSignal() => observableWait;
		}

		public void ReindexMany(Func<CountdownEvent> getCountDown, Action<Exception> signal)
		{
			var manyTypesObserver = new ReindexObserver(
				onError: signal,
				onCompleted: () => ReindexManyTypesCompleted(getCountDown())
			);

			this._reindexManyTypesResult.Subscribe(manyTypesObserver);
		}

		private void ReindexManyTypesCompleted(CountdownEvent handle)
		{
			var refresh = this._client.Refresh(NewManyTypesIndexName);

			var originalIndexCount = this._client.Count<CommitActivity>(c => c
				.Index(IndexName)
				.Query(q => q.HasRelationName<CommitActivity>(p => p.Join))
			);
			var newIndexCount = this._client.Count<CommitActivity>(c => c
				.Index(NewManyTypesIndexName)
				.Query(q => q.HasRelationName<CommitActivity>(p => p.Join))
			);

			originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

			var scroll = "20s";
			var searchResult = this._client.Search<CommitActivity>(s => s
				.Index(NewManyTypesIndexName)
				.From(0)
				.Size(100)
				.Query(q => q.HasRelationName<CommitActivity>(p=>p.Join))
				.Scroll(scroll)
			);

			do
			{
				var result = searchResult;
				searchResult = this._client.Scroll<CommitActivity>(scroll, result.ScrollId);
				foreach (var hit in searchResult.Hits)
				{
					hit.Routing.Should().NotBeNullOrEmpty();
				}
			} while (searchResult.IsValid && searchResult.Documents.Any());
			handle.Signal();
		}

		public void ReindexProjection(Func<CountdownEvent> getCountDown, Action<Exception> signal)
		{
			var projectionObserver = new ReindexObserver(
				onError: signal,
				onCompleted: () => ProjectionCompleted(getCountDown())
			);
			this._reindexProjectionResult.Subscribe(projectionObserver);
		}

		private void ProjectionCompleted(CountdownEvent handle)
		{
			var refresh = this._client.Refresh(NewProjectionIndex);
			var originalIndexCount = this._client.Count<CommitActivity>(c => c
				.Index(IndexName)
				.Query(q=>q.HasRelationName<CommitActivity>(p => p.Join))
			);

			var newIndexSearch = this._client.Search<CommitActivity>(c => c
				.Index(NewProjectionIndex)
				.Query(q=>q.HasRelationName<CommitActivity>(p => p.Join))
			);

			originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexSearch.Total);

			newIndexSearch.Documents.Should().OnlyContain(c => c.Id.EndsWith("-projected"));

			handle.Signal();
		}

		public void ReindexSingleType(Func<CountdownEvent> getCountDown, Action<Exception> signal)
		{
			var singleTypeObserver = new ReindexObserver(
				onError: signal,
				onCompleted: () => ReindexSingleTypeCompleted(getCountDown())
			);
			this._reindexSingleTypeResult.Subscribe(singleTypeObserver);
		}

		private void ReindexSingleTypeCompleted(CountdownEvent handle)
		{
			var refresh = this._client.Refresh(NewSingleTypeIndexName);
			var originalIndexCount = this._client.Count<Project>(c => c.Index(IndexName));

			// new index should only contain project document types
			var newIndexCount = this._client.Count<Project>(c => c.Index(NewSingleTypeIndexName).AllTypes());

			originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

			handle.Signal();
		}

	}
}
