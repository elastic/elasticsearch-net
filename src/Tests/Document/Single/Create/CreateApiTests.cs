using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Create
{
	public class CreateApiTests :
		ApiIntegrationTestBase<WritableCluster, ICreateResponse, ICreateRequest<Project>, CreateDescriptor<Project>, CreateRequest<Project>>
	{
		private Project Document => new Project
		{
			State = StateOfBeing.Stable,
			Name = CallIsolatedValue,
			StartedOn = FixedDate,
			LastActivity = FixedDate,
			CuratedTags = new List<Tag> {new Tag {Name = "x", Added = FixedDate}},
		};

		public CreateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Create(this.Document, f),
			fluentAsync: (client, f) => client.CreateAsync(this.Document, f),
			request: (client, r) => client.Create(r),
			requestAsync: (client, r) => client.CreateAsync(r)
			);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath
			=> $"/project/project/{CallIsolatedValue}/_create?wait_for_active_shards=1&refresh=true&routing=route";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson =>
			new
			{
				name = CallIsolatedValue,
				state = "Stable",
				startedOn = FixedDate,
				lastActivity = FixedDate,
				curatedTags = new[] {new {name = "x", added = FixedDate}},
			};

		protected override CreateDescriptor<Project> NewDescriptor() => new CreateDescriptor<Project>(this.Document);

		protected override Func<CreateDescriptor<Project>, ICreateRequest<Project>> Fluent => s => s
			.WaitForActiveShards("1")
			.Refresh(Refresh.True)
			.Routing("route");

		protected override CreateRequest<Project> Initializer =>
			new CreateRequest<Project>(this.Document)
			{
				Refresh = Refresh.True,
				WaitForActiveShards = "1",
				Routing = "route"
			};
	}

	public class CreateInvalidApiTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		public CreateInvalidApiTests(WritableCluster cluster) : base(cluster) { }

		[I]
		public void CreateWithSameIndexTypeAndId()
		{
			var index = RandomString();
			var project = Project.Generator.Generate(1).First();
			var createResponse = this.Client.Create(project, f => f
				.Index(index)
			);
			createResponse.ShouldBeValid();
			createResponse.ApiCall.HttpStatusCode.Should().Be(201);
			createResponse.Result.Should().Be(Result.Created);
			createResponse.Index.Should().Be(index);
			createResponse.Type.Should().Be(this.Client.Infer.TypeName<Project>());
			createResponse.Id.Should().Be(project.Name);

			createResponse.Shards.Should().NotBeNull();
			createResponse.Shards.Total.Should().BeGreaterThan(0);
			createResponse.Shards.Successful.Should().BeGreaterThan(0);
			createResponse.PrimaryTerm.Should().BeGreaterThan(0);
			createResponse.SequenceNumber.Should().BeGreaterOrEqualTo(0);

			createResponse = this.Client.Create(project, f => f
				.Index(index)
			);

			createResponse.ShouldNotBeValid();
			createResponse.ApiCall.HttpStatusCode.Should().Be(409);
		}
	}

	public class CreateJObjectIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		public CreateJObjectIntegrationTests(WritableCluster cluster) : base(cluster) { }

		[I]
		public void Create()
		{
			var index = RandomString();
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

			var createResponse = this.Client.Create(jObject, f => f
				.Index(index)
				.Id(jObject["id"].Value<int>())
			);

			createResponse.ShouldBeValid();
			createResponse.ApiCall.HttpStatusCode.Should().Be(201);
			createResponse.Result.Should().Be(Result.Created);
			createResponse.Index.Should().Be(index);
			createResponse.Type.Should().Be("jobject");

			var bulkResponse = this.Client.Bulk(b => b
				.Index(index)
				.CreateMany(jObjects.Skip(1), (bi, d) => bi
					.Document(d)
					.Id(d["id"].Value<int>())
				)
			);

			foreach (var item in bulkResponse.Items)
			{
				item.IsValid.Should().BeTrue();
				item.Status.Should().Be(201);
			}
		}
	}

	public class CreateAnonymousTypesIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		public CreateAnonymousTypesIntegrationTests(WritableCluster cluster) : base(cluster) { }

		[I]
		public void Create()
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

			var createResponse = this.Client.Create(anonymousType, f => f
				.Index(index)
				.Id(anonymousType.name)
			);

			createResponse.ShouldBeValid();
			createResponse.ApiCall.HttpStatusCode.Should().Be(201);
			createResponse.Index.Should().Be(index);
			createResponse.Result.Should().Be(Result.Created);
			createResponse.Type.Should().StartWith("<>");
		}
	}
}
