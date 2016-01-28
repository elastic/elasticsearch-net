using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;
using Elasticsearch.Net;

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
		private readonly IObservable<IReindexResponse<ILazyDocument>> _reindexResult;
		private readonly IElasticClient _client;

		private static string NewIndexName { get; } = $"project-copy-{Guid.NewGuid().ToString("N").Substring(8)}";

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
			this._reindexResult = this._client.Reindex<ILazyDocument>(IndexName, NewIndexName, r=>r);
		}

		[I] public void ReturnsExpectedResponse()
		{
			var handle = new ManualResetEvent(false);
			var observer = new ReindexObserver<ILazyDocument>(
					onError: (e) => { throw e; },
					completed: () =>
					{
						var refresh = this._client.Refresh(NewIndexName);

						var originalIndexCount = this._client.Count<CommitActivity>(c => c.Index(IndexName));
						var newIndexCount = this._client.Count<CommitActivity>(c => c.Index(NewIndexName));

						originalIndexCount.Count.Should().BeGreaterThan(0).And.Be(newIndexCount.Count);

						var scroll = "20s";

						var searchResult = this._client.Search<CommitActivity>(s => s
							.Index(NewIndexName)
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
						handle.Set();
					}
				);

			this._reindexResult.Subscribe(observer);
			handle.WaitOne(TimeSpan.FromMinutes(3));
		}
	}
}
