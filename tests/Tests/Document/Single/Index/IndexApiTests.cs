// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Tests.Core.Extensions;
using Tests.Framework.DocumentationTests;
using System.Linq;
using static Tests.Domain.Helpers.TestValueHelper;
using Tests.Core.Client;

namespace Tests.Document.Single.Index;

public class IndexApiTests
	: ApiIntegrationTestBase<WritableCluster, IndexResponse, IndexRequestDescriptor<Project>, IndexRequest<Project>>
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

	protected override Action<IndexRequestDescriptor<Project>> Fluent => s => s
		.WaitForActiveShards(1)
		.OpType(OpType.Index)
		.Refresh(Refresh.True)
		.Routing("route");

	protected override HttpMethod HttpMethod => HttpMethod.PUT;
	protected override bool IncludeNullInExpected => false;
	protected override bool SupportsDeserialization => false;

	protected override IndexRequest<Project> Initializer => new(Document)
	{
		Refresh = Refresh.True,
		OpType = OpType.Index,
		WaitForActiveShards = "1",
		Routing = "route"
	};

	protected override string ExpectedUrlPathAndQuery
		=> $"/project/_doc/{CallIsolatedValue}?wait_for_active_shards=1&op_type=index&refresh=true&routing=route";

	private Project Document => new()
	{
		State = StateOfBeing.Stable,
		Name = CallIsolatedValue,
		StartedOn = FixedDate,
		LastActivity = FixedDate,
		CuratedTags = new List<Tag> { new Tag { Name = "x", Added = FixedDate } },
		SourceOnly = TestClient.Configuration.Random.SourceSerializer ? new SourceOnlyObject() : null
	};

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Index(Document, f),
		(client, f) => client.IndexAsync(Document, f),
		(client, r) => client.Index(r),
		(client, r) => client.IndexAsync(r)
	);

	protected override IndexRequestDescriptor<Project> NewDescriptor() => new(Document);
}

public class IndexIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
{
	public IndexIntegrationTests(WritableCluster cluster) : base(cluster) { }

	[I]
	public void OpTypeCreate()
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
		indexResult.Id.Should().Be(commitActivity.Id);
		indexResult.Version.Should().Be(1);
		indexResult.Shards.Should().NotBeNull();
		indexResult.Shards.Total.Should().BeGreaterOrEqualTo(1);
		indexResult.Shards.Successful.Should().BeGreaterOrEqualTo(1);
		indexResult.SeqNo.Should().BeGreaterOrEqualTo(0);
		indexResult.PrimaryTerm.Should().BeGreaterThan(0);

		indexResult = Client.Index(commitActivity, f => f.Index(indexName));

		indexResult.ShouldBeValid();
		indexResult.ApiCall.HttpStatusCode.Should().Be(200);
		indexResult.Version.Should().Be(2);
		indexResult.Result.Should().Be(Result.Updated);
	}
}
