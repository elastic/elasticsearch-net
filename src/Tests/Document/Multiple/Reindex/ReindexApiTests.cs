using System;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.Reindex
{
	[CollectionDefinition(IntegrationContext.Reindex)]
	public class ReindexCluster : ClusterBase, ICollectionFixture<ReindexCluster>, IClassFixture<EndpointUsage>
	{
		public override void Boostrap()
		{
			var seeder = new Seeder(this.Node);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	} 

	[Collection(IntegrationContext.Reindex)]
	public class ReindexApiTests : SerializationTestBase
	{
		private readonly IObservable<IReindexResponse<ILazyDocument>> _reindexManyTypesResult;
		private readonly IObservable<IReindexResponse<Project>> _reindexSingleTypeResult;
		private readonly IElasticClient _client;

		private static string NewManyTypesIndexName { get; } = $"project-copy-{Guid.NewGuid().ToString("N").Substring(8)}";

		private static string NewSingleTypeIndexName { get; } = $"project-copy-{Guid.NewGuid().ToString("N").Substring(8)}";

		private static string IndexName { get; } = "project";

		public ReindexApiTests(ReindexCluster cluster, EndpointUsage usage)
		{
			this._client = cluster.Client();

			// create a couple of projects
			var projects = Project.Generator.Generate(2).ToList();
			var indexProjectsResponse = this._client.IndexMany(projects, IndexName);

			// create a thousand commits and associate with the projects
			var commits = CommitActivity.Generator.Generate(1000).ToList();
			var bb = new BulkDescriptor();
			for (int index = 0; index < commits.Count; index++)
			{
				var commit = commits[index];
				var project = index%2 == 0
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

			var bulkResult = this._client.Bulk(b => bb);
			bulkResult.IsValid.Should().BeTrue();

			this._client.Refresh(IndexName);

			this._reindexManyTypesResult = this._client.Reindex<ILazyDocument>(IndexName, NewManyTypesIndexName, r => r.AllTypes());
			this._reindexSingleTypeResult = this._client.Reindex<Project>(IndexName, NewSingleTypeIndexName);
		}

		[I] public void ReturnsExpectedResponse()
		{
			var handles = new[]
			{
				new ManualResetEvent(false),
				new ManualResetEvent(false)
			};

			var manyTypesObserver = new ReindexObserver<ILazyDocument>(
					onError: (e) => { throw e; },
					completed: () =>
					{
						var refresh = this._client.Refresh(NewManyTypesIndexName);
						var originalIndexCount = this._client.Count<CommitActivity>(c => c.Index(IndexName));
						var newIndexCount = this._client.Count<CommitActivity>(c => c.Index(NewManyTypesIndexName));

						originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

						var scroll = "20s";

						var searchResult = this._client.Search<CommitActivity>(s => s
							.Index(NewManyTypesIndexName)
							.From(0)
							.Size(100)
							.Query(q => q.MatchAll())
							.SearchType(SearchType.Scan)
							.Scroll(scroll)
						);
			
						do
						{
							var result = searchResult;
							searchResult = this._client.Scroll<CommitActivity>(scroll, result.ScrollId);
							foreach (var hit in searchResult.Hits)
							{
								hit.Timestamp.Should().HaveValue();
								hit.Parent.Should().NotBeNullOrEmpty();
								hit.Routing.Should().NotBeNullOrEmpty();
							}
						} while (searchResult.IsValid && searchResult.Documents.Any());
						handles[0].Set();
					}
				);

			this._reindexManyTypesResult.Subscribe(manyTypesObserver);

			var singleTypeObserver = new ReindexObserver<Project>(
					onError: (e) => { throw e; },
					completed: () =>
					{
						var refresh = this._client.Refresh(NewSingleTypeIndexName);
						var originalIndexCount = this._client.Count<Project>(c => c.Index(IndexName));

						// new index should only contain project document types
						var newIndexCount = this._client.Count<Project>(c => c.Index(NewSingleTypeIndexName).AllTypes());

						originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

						handles[1].Set();
					}
				);

			this._reindexSingleTypeResult.Subscribe(singleTypeObserver);

			WaitHandle.WaitAll(handles, TimeSpan.FromMinutes(3));
		}
	}
}
