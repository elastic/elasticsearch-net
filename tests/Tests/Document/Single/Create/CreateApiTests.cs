// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Domain;
using Tests.Domain.Extensions;
using Tests.Framework.DocumentationTests;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Document.Single.Create
{
	public class CreateApiTests
		: ApiIntegrationTestBase<WritableCluster, CreateResponse, ICreateRequest<Project>, CreateDescriptor<Project>, CreateRequest<Project>>
	{
		public CreateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

		protected override Func<CreateDescriptor<Project>, ICreateRequest<Project>> Fluent => s => s
			.WaitForActiveShards("1")
			.Refresh(Refresh.True)
			.Routing("route");

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool IncludeNullInExpected => false;

		protected override CreateRequest<Project> Initializer =>
			new CreateRequest<Project>(Document)
			{
				Refresh = Refresh.True,
				WaitForActiveShards = "1",
				Routing = "route"
			};

		protected override string UrlPath
			=> $"/project/_create/{CallIsolatedValue}?wait_for_active_shards=1&refresh=true&routing=route";

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
			(client, f) => client.Create(Document, f),
			(client, f) => client.CreateAsync(Document, f),
			(client, r) => client.Create(r),
			(client, r) => client.CreateAsync(r)
		);

		protected override CreateDescriptor<Project> NewDescriptor() => new CreateDescriptor<Project>(Document);
	}

	public class CreateInvalidApiTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		public CreateInvalidApiTests(WritableCluster cluster) : base(cluster) { }

		[I]
		public void CreateWithSameIndexTypeAndId()
		{
			var index = RandomString();
			var project = Project.Generator.Generate(1).First();
			var createResponse = Client.Create(project, f => f
				.Index(index)
			);
			createResponse.ShouldBeValid();
			createResponse.ApiCall.HttpStatusCode.Should().Be(201);
			createResponse.Result.Should().Be(Result.Created);
			createResponse.Index.Should().Be(index);
			createResponse.Id.Should().Be(project.Name);

			createResponse.Shards.Should().NotBeNull();
			createResponse.Shards.Total.Should().BeGreaterThan(0);
			createResponse.Shards.Successful.Should().BeGreaterThan(0);
			createResponse.PrimaryTerm.Should().BeGreaterThan(0);
			createResponse.SequenceNumber.Should().BeGreaterOrEqualTo(0);

			createResponse = Client.Create(project, f => f
				.Index(index)
			);

			createResponse.ShouldNotBeValid();
			createResponse.ApiCall.HttpStatusCode.Should().Be(409);
		}
	}

	[JsonNetSerializerOnly]
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

			var createResponse = Client.Create(jObject, f => f
				.Index(index)
				.Id(jObject["id"].Value<int>())
			);

			createResponse.ShouldBeValid();
			createResponse.ApiCall.HttpStatusCode.Should().Be(201);
			createResponse.Result.Should().Be(Result.Created);
			createResponse.Index.Should().Be(index);

			var bulkResponse = Client.Bulk(b => b
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

		[I] public void Create()
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

			var createResponse = Client.Create(anonymousType, f => f
				.Index(index)
				.Id(anonymousType.name)
			);

			createResponse.ShouldBeValid();
			createResponse.ApiCall.HttpStatusCode.Should().Be(201);
			createResponse.Index.Should().Be(index);
			createResponse.Result.Should().Be(Result.Created);
		}
	}
}
