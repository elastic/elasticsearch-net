// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
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
		protected override void SeedNode() => new DefaultSeeder(Client).SeedNodeNoData();
	}

	public class ManualReindexCluster : ClientTestClusterBase
	{
		protected override void SeedNode() => new DefaultSeeder(Client).SeedNodeNoData();
	}

	public class ReindexApiTests : IClusterFixture<ManualReindexCluster>
	{
		private readonly IElasticClient _client;
		private readonly IObservable<BulkAllResponse> _reindexManyTypesResult;
		private readonly IObservable<BulkAllResponse> _reindexProjectionResult;
		private readonly IObservable<BulkAllResponse> _reindexSingleTypeResult;

		public ReindexApiTests(ManualReindexCluster cluster)
		{
			_client = cluster.Client;

			// create a couple of projects
			var projects = Project.Generator.Generate(2);
			_client.IndexMany(projects, IndexName);
			_client.Indices.Refresh(IndexName);

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

			var bulkResult = _client.Bulk(b => bb);
			bulkResult.ShouldBeValid();

			_client.Indices.Refresh(IndexName);

			_reindexManyTypesResult = _client.Reindex<ILazyDocument>(r => r
				.BackPressureFactor(10)
				.ScrollAll("1m", 2, s => s
					.Search(ss => ss
						.Index(IndexName)
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
			_reindexProjectionResult = _client.Reindex<CommitActivity, CommitActivityVersion2>(
				IndexName, NewProjectionIndex, p => new CommitActivityVersion2(p));
		}

		private static string IndexName { get; } = "project";

		private static string NewManyTypesIndexName { get; } = $"project-many-{Guid.NewGuid().ToString("N").Substring(8)}";

		private static string NewProjectionIndex { get; } = $"project-projection-{Guid.NewGuid().ToString("N").Substring(8)}";

		private static string NewSingleTypeIndexName { get; } = $"project-single-{Guid.NewGuid().ToString("N").Substring(8)}";

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

			void Signal(Exception e)
			{
				ex = e;
				// ReSharper disable once AccessToModifiedClosure
				observableWait?.Signal();
			}

			// ReSharper disable once AccessToModifiedClosure
			CountdownEvent GetSignal() => observableWait;
		}

		public void ReindexMany(Func<CountdownEvent> getCountDown, Action<Exception> signal)
		{
			var manyTypesObserver = new ReindexObserver(
				onError: signal,
				onCompleted: () => ReindexManyTypesCompleted(getCountDown())
			);

			_reindexManyTypesResult.Subscribe(manyTypesObserver);
		}

		private void ReindexManyTypesCompleted(CountdownEvent handle)
		{
			var refresh = _client.Indices.Refresh(NewManyTypesIndexName);
			refresh.ShouldBeValid();

			var originalIndexCount = _client.Count<CommitActivity>(c => c
				.Index(IndexName)
				.Query(q => q.HasRelationName<CommitActivity>(p => p.Join))
			);
			var newIndexCount = _client.Count<CommitActivity>(c => c
				.Index(NewManyTypesIndexName)
				.Query(q => q.HasRelationName<CommitActivity>(p => p.Join))
			);

			originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

			var scroll = "20s";
			var searchResult = _client.Search<CommitActivity>(s => s
				.Index(NewManyTypesIndexName)
				.From(0)
				.Size(100)
				.Query(q => q.HasRelationName<CommitActivity>(p => p.Join))
				.Scroll(scroll)
			);

			do
			{
				var result = searchResult;
				searchResult = _client.Scroll<CommitActivity>(new ScrollRequest(result.ScrollId, scroll));
				foreach (var hit in searchResult.Hits) hit.Routing.Should().NotBeNullOrEmpty();
			} while (searchResult.IsValid && searchResult.Documents.Any());
			handle.Signal();
		}

		public void ReindexProjection(Func<CountdownEvent> getCountDown, Action<Exception> signal)
		{
			var projectionObserver = new ReindexObserver(
				onError: signal,
				onCompleted: () => ProjectionCompleted(getCountDown())
			);
			_reindexProjectionResult.Subscribe(projectionObserver);
		}

		private void ProjectionCompleted(CountdownEvent handle)
		{
			var refresh = _client.Indices.Refresh(NewProjectionIndex);
			refresh.ShouldBeValid();
			var originalIndexCount = _client.Count<CommitActivity>(c => c
				.Index(IndexName)
				.Query(q => q.HasRelationName<CommitActivity>(p => p.Join))
			);

			var newIndexSearch = _client.Search<CommitActivity>(c => c
				.Index(NewProjectionIndex)
				.Query(q => q.HasRelationName<CommitActivity>(p => p.Join))
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
			_reindexSingleTypeResult.Subscribe(singleTypeObserver);
		}

		private void ReindexSingleTypeCompleted(CountdownEvent handle)
		{
			var refresh = _client.Indices.Refresh(NewSingleTypeIndexName);
			refresh.ShouldBeValid();
			var originalIndexCount = _client.Count<Project>(c => c.Index(IndexName));

			// new index should only contain project document types
			var newIndexCount = _client.Count<Project>(c => c.Index(NewSingleTypeIndexName));

			originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

			handle.Signal();
		}

		public class CommitActivityVersion2
		{
			public CommitActivityVersion2(CommitActivity commit)
			{
				Join = commit.Join;
				ProjectName = commit.ProjectName + "-projected";
				Id = commit.Id + "-projected";
				Committer = commit.Committer;
			}

			public Developer Committer { get; }
			public string Id { get; }
			public JoinField Join { get; set; }
			public string ProjectName { get; }
		}
	}
}
