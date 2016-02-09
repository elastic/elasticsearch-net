using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Index
{
	[Collection(IntegrationContext.Indexing)]
	public class IndexApiTests :
		ApiIntegrationTestBase<IIndexResponse, IIndexRequest<Project>, IndexDescriptor<Project>, IndexRequest<Project>>
	{
		private Project Document => new Project
		{
			State = StateOfBeing.Stable,
			Name = CallIsolatedValue,
			StartedOn = FixedDate,
			LastActivity = FixedDate,
			CuratedTags = new List<Tag> {new Tag {Name = "x", Added = FixedDate}},
			
		};

		public IndexApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Index<Project>(this.Document, f),
			fluentAsync: (client, f) => client.IndexAsync<Project>(this.Document, f),
			request: (client, r) => client.Index(r),
			requestAsync: (client, r) => client.IndexAsync(r)
			);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath
			=> $"/project/project/{CallIsolatedValue}?consistency=quorum&op_type=index&refresh=true&routing=route";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson =>
			new
			{
				name = CallIsolatedValue,
				state = "Stable",
				startedOn = FixedDate,
				lastActivity = FixedDate,
				curatedTags = new[] {new {name = "x", added = FixedDate}},
			};

		protected override IndexDescriptor<Project> NewDescriptor() => new IndexDescriptor<Project>(this.Document);

		protected override Func<IndexDescriptor<Project>, IIndexRequest<Project>> Fluent => s => s
			.Consistency(Consistency.Quorum)
			.OpType(OpType.Index)
			.Refresh()
			.Routing("route");

		protected override IndexRequest<Project> Initializer =>
			new IndexRequest<Project>(this.Document)
			{
				Refresh = true,
				OpType = OpType.Index,
				Consistency = Consistency.Quorum,
				Routing = "route"
			};

	}

	[Collection(IntegrationContext.Indexing)]
	public class IndexIntegrationTests : SimpleIntegration
	{
		public IndexIntegrationTests(IndexingCluster cluster) : base(cluster)
		{
		}

		[I]
		public void OpTypeCreate()
		{
			var indexName = this.RandomString();
			var project = Project.Generator.Generate(1).First();
			var indexResult = this.Client.Index(project, f => f
				.Index(indexName)
				.OpType(OpType.Create)
				);
			indexResult.IsValid.Should().BeTrue();
			indexResult.ApiCall.HttpStatusCode.Should().Be(201);
			indexResult.Created.Should().BeTrue();
			indexResult.Index.Should().Be(indexName);
			indexResult.Type.Should().Be(this.Client.Infer.TypeName<Project>());
			indexResult.Id.Should().Be(project.Name);

			indexResult = this.Client.Index(project, f => f
				.Index(indexName)
				.OpType(OpType.Create)
				);

			indexResult.IsValid.Should().BeFalse();
			indexResult.Created.Should().BeFalse();
			indexResult.ApiCall.HttpStatusCode.Should().Be(409);
		}

		[I]
		public void Index()
		{
			var indexName = this.RandomString();
			var commitActivity = CommitActivity.Generator.Generate(1).First();
			var indexResult = this.Client.Index(commitActivity, f => f.Index(indexName));
			indexResult.IsValid.Should().BeTrue();
			indexResult.ApiCall.HttpStatusCode.Should().Be(201);
			indexResult.Created.Should().BeTrue();
			indexResult.Index.Should().Be(indexName);
			indexResult.Type.Should().Be(this.Client.Infer.TypeName<CommitActivity>());
			indexResult.Id.Should().Be(commitActivity.Id);
			indexResult.Version.Should().Be(1);

			indexResult = this.Client.Index(commitActivity, f => f.Index(indexName));

			indexResult.IsValid.Should().BeTrue();
			indexResult.Created.Should().BeFalse();
			indexResult.ApiCall.HttpStatusCode.Should().Be(200);
			indexResult.Version.Should().Be(2);
		}
	}

	[Collection(IntegrationContext.Indexing)]
	public class IndexJObjectIntegrationTests : SimpleIntegration
	{
		public IndexJObjectIntegrationTests(IndexingCluster cluster) : base(cluster)
		{
		}

		[I]
		public void Index()
		{
			var jObjects = Enumerable.Range(1, 1000)
				.Select(i =>
					new JObject
					{
						{"id", i},
						{"name", $"name {i}"},
						{"value", Math.Pow(i, 2)},
						{"date", new DateTime(2016, 1, 1)},
						{
							"child", new JObject
							{
								{"child_name", $"child_name {i}{i}"},
								{"child_value", 3}
							}
						}
					});

			var jObject = jObjects.First();

			var indexResult = this.Client.Index(jObject, f => f
				.Id(jObject["id"].Value<int>())
				);

			indexResult.IsValid.Should().BeTrue();
			indexResult.ApiCall.HttpStatusCode.Should().Be(201);
			indexResult.Created.Should().BeTrue();
			indexResult.Index.Should().Be(Client.ConnectionSettings.DefaultIndex);
			indexResult.Type.Should().Be("jobject");

			var bulkResponse = this.Client.Bulk(b => b
				.IndexMany(jObjects.Skip(1), (bi, d) => bi
					.Document(d)
					.Id(d["id"].Value<int>())
				)
				);

			foreach (var response in bulkResponse.Items)
			{
				response.IsValid.Should().BeTrue();
				response.Status.Should().Be(201);
			}
		}
	}

	[Collection(IntegrationContext.Indexing)]
	public class IndexAnonymousTypesIntegrationTests : SimpleIntegration
	{
		public IndexAnonymousTypesIntegrationTests(IndexingCluster cluster) : base(cluster) { }

		[I]
		public void Index()
		{
			var anonymousType = new
			{
				name = "name",
				value = 3,
				date = new DateTime(2016, 1, 1),
				child = new
				{
					child_name = "child_name",
					child_value = 3
				}
			};

			var indexResult = this.Client.Index(anonymousType, f => f
				.Id(anonymousType.name)
			);

			indexResult.IsValid.Should().BeTrue();
			indexResult.ApiCall.HttpStatusCode.Should().Be(201);
			indexResult.Created.Should().BeTrue();
			indexResult.Index.Should().Be(Client.ConnectionSettings.DefaultIndex);
			indexResult.Type.Should().StartWith("<>");
		}
	}
}
