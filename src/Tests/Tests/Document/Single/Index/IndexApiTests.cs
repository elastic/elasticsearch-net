using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Domain.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Document.Single.Index
{
	public class IndexApiTests
		: ApiIntegrationTestBase<WritableCluster, IIndexResponse, IIndexRequest<Project>, IndexDescriptor<Project>, IndexRequest<Project>>
	{
		public IndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson =>
			new
			{
				name = CallIsolatedValue,
				type = Project.TypeName,
				join = Document.Join.ToAnonymousObject(),
				state = "Stable",
				visibility = "Public",
				startedOn = FixedDate,
				lastActivity = FixedDate,
				numberOfContributors = 0,
				sourceOnly = Dependant(null, new { notWrittenByDefaultSerializer = "written" }),
				curatedTags = new[] { new { name = "x", added = FixedDate } },
			};

		protected override int ExpectStatusCode => 201;

		protected override Func<IndexDescriptor<Project>, IIndexRequest<Project>> Fluent => s => s
			.WaitForActiveShards("1")
			.OpType(OpType.Index)
			.Refresh(Refresh.True)
			.Routing("route");

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool IncludeNullInExpected => false;

		protected override IndexRequest<Project> Initializer =>
			new IndexRequest<Project>(Document)
			{
				Refresh = Refresh.True,
				OpType = OpType.Index,
				WaitForActiveShards = "1",
				Routing = "route"
			};

		protected override string UrlPath
			=> $"/project/_doc/{CallIsolatedValue}?wait_for_active_shards=1&op_type=index&refresh=true&routing=route";

		private Project Document => new Project
		{
			State = StateOfBeing.Stable,
			Name = CallIsolatedValue,
			StartedOn = FixedDate,
			LastActivity = FixedDate,
			CuratedTags = new List<Tag> { new Tag { Name = "x", Added = FixedDate } },
			SourceOnly = TestClient.Configuration.Random.SourceSerializer ? new SourceOnlyObject() : null
		};

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Index<Project>(Document, f),
			(client, f) => client.IndexAsync<Project>(Document, f),
			(client, r) => client.Index(r),
			(client, r) => client.IndexAsync(r)
		);

		protected override IndexDescriptor<Project> NewDescriptor() => new IndexDescriptor<Project>(Document);
	}

	public class IndexIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		public IndexIntegrationTests(WritableCluster cluster) : base(cluster) { }

		[I] public void OpTypeCreate()
		{
			var indexName = RandomString();
			var project = Project.Generator.Generate(1).First();
			var indexResult = Client.Index(project, f => f
				.Index(indexName)
				.OpType(OpType.Create)
			);
			indexResult.ShouldBeValid();
			indexResult.ApiCall.HttpStatusCode.Should().Be(201);
			indexResult.Result.Should().Be(Result.Created);
			indexResult.Index.Should().Be(indexName);
			indexResult.Type.Should().Be("_doc");
			indexResult.Id.Should().Be(project.Name);

			indexResult = Client.Index(project, f => f
				.Index(indexName)
				.OpType(OpType.Create)
			);

			indexResult.ShouldNotBeValid();
			indexResult.ApiCall.HttpStatusCode.Should().Be(409);
			indexResult.ServerError.Should().NotBeNull();
			indexResult.ServerError.Error.Type.Should().Contain("conflict");
			indexResult.ServerError.Status.Should().Be(409);
		}

		[I]
		public void Index()
		{
			var indexName = RandomString();
			var commitActivity = CommitActivity.CommitActivities.First();
			var indexResult = Client.Index(commitActivity, f => f.Index(indexName));
			indexResult.ShouldBeValid();
			indexResult.ApiCall.HttpStatusCode.Should().Be(201);
			indexResult.Result.Should().Be(Result.Created);
			indexResult.Index.Should().Be(indexName);
			indexResult.Type.Should().Be("_doc");
			indexResult.Id.Should().Be(commitActivity.Id);
			indexResult.Version.Should().Be(1);
			indexResult.Shards.Should().NotBeNull();
			indexResult.Shards.Total.Should().BeGreaterOrEqualTo(1);
			indexResult.Shards.Successful.Should().BeGreaterOrEqualTo(1);
			indexResult.SequenceNumber.Should().BeGreaterOrEqualTo(0);
			indexResult.PrimaryTerm.Should().BeGreaterThan(0);

			indexResult = Client.Index(commitActivity, f => f.Index(indexName));

			indexResult.ShouldBeValid();
			indexResult.ApiCall.HttpStatusCode.Should().Be(200);
			indexResult.Version.Should().Be(2);
			indexResult.Result.Should().Be(Result.Updated);
		}
	}

	public class IndexJObjectIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		public IndexJObjectIntegrationTests(WritableCluster cluster) : base(cluster) { }

		[I]
		public void Index()
		{
			var index = RandomString();
			var jObjects = Enumerable.Range(1, 1000)
				.Select(i =>
					new JObject
					{
						{ "id", i },
						{ "name", $"name {i}" },
						{ "value", Math.Pow(i, 2) },
						{ "date", new DateTime(2016, 1, 1) },
						{
							"child", new JObject
							{
								{ "child_name", $"child_name {i}{i}" },
								{ "child_value", 3 }
							}
						}
					});

			var jObject = jObjects.First();

			var indexResult = Client.Index(jObject, f => f
				.Index(index)
				.Id(jObject["id"].Value<int>())
			);

			indexResult.ShouldBeValid();
			indexResult.ApiCall.HttpStatusCode.Should().Be(201);
			indexResult.Result.Should().Be(Result.Created);
			indexResult.Index.Should().Be(index);
			indexResult.Type.Should().Be("jobject");
			indexResult.Shards.Should().NotBeNull();
			indexResult.Shards.Total.Should().BeGreaterOrEqualTo(1);
			indexResult.Shards.Successful.Should().BeGreaterOrEqualTo(1);
			indexResult.SequenceNumber.Should().BeGreaterOrEqualTo(0);
			indexResult.PrimaryTerm.Should().BeGreaterThan(0);

			var bulkResponse = Client.Bulk(b => b
				.Index(index)
				.IndexMany(jObjects.Skip(1), (bi, d) => bi
					.Document(d)
					.Id(d["id"].Value<int>())
				)
			);

			foreach (var item in bulkResponse.Items)
			{
				item.IsValid.Should().BeTrue();
				item.Status.Should().Be(201);
				item.Shards.Should().NotBeNull();
				item.Shards.Total.Should().BeGreaterOrEqualTo(1);
				item.Shards.Successful.Should().BeGreaterOrEqualTo(1);
				item.SequenceNumber.Should().BeGreaterOrEqualTo(0);
				item.PrimaryTerm.Should().BeGreaterThan(0);
			}
		}
	}

	public class IndexAnonymousTypesIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		public IndexAnonymousTypesIntegrationTests(WritableCluster cluster) : base(cluster) { }

		[I]
		public void Index()
		{
			var index = RandomString();
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

			var indexResult = Client.Index(anonymousType, f => f
				.Index(index)
				.Id(anonymousType.name)
			);

			indexResult.ShouldBeValid();
			indexResult.ApiCall.HttpStatusCode.Should().Be(201);
			indexResult.Result.Should().Be(Result.Created);
			indexResult.Index.Should().Be(index);
			indexResult.Type.Should().StartWith("<>");
			indexResult.Shards.Should().NotBeNull();
			indexResult.Shards.Total.Should().BeGreaterOrEqualTo(1);
			indexResult.Shards.Successful.Should().BeGreaterOrEqualTo(1);
			indexResult.SequenceNumber.Should().BeGreaterOrEqualTo(0);
			indexResult.PrimaryTerm.Should().BeGreaterThan(0);
		}
	}
}
