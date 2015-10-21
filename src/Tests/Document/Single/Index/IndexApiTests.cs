using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Index
{
	[Collection(IntegrationContext.Indexing)]
	public class IndexApiTests : ApiIntegrationTestBase<IIndexResponse, IIndexRequest<Project>, IndexDescriptor<Project>, IndexRequest<Project>>
	{
		private Project Document => new Project
		{
			State = StateOfBeing.Stable,
			Name = "SomeProject",
			StartedOn = FixedDate,
			LastActivity = FixedDate,
			CuratedTags = new List<Tag> { new Tag { Name = "x", Added = FixedDate } }
		};

		public IndexApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Index<Project>(this.Document, f),
			fluentAsync: (client, f) => client.IndexAsync<Project>(this.Document, f),
			request: (client, r) => client.Index(r),
			requestAsync: (client, r) => client.IndexAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => "/project/project/SomeProject?consistency=all&op_type=index&refresh=true&routing=route";

		protected override object ExpectJson =>
			new
			{
				name = "SomeProject",
				state = "Stable",
				startedOn = FixedDate,
				lastActivity = FixedDate,
				curatedTags = new[] { new { name = "x", added = FixedDate } },
			};

		protected override IndexDescriptor<Project> NewDescriptor() => new IndexDescriptor<Project>(this.Document);
		protected override Func<IndexDescriptor<Project>, IIndexRequest<Project>> Fluent => s => s
			.Consistency(Consistency.All)
			.OpType(OpType.Index)
			.Refresh()
			.Routing("route");

		protected override IndexRequest<Project> Initializer =>
			new IndexRequest<Project>(this.Document)
			{
				Refresh = true,
				OpType = OpType.Index,
				Consistency = Consistency.All,
				Routing = "route"
			};

	}

	[Collection(IntegrationContext.Indexing)]
	public class IndexIntegrationTests : SimpleIntegration
	{
		public IndexIntegrationTests(IndexingCluster cluster) : base(cluster) { }

		[I] public void OpTypeCreate()
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

		[I] public void Index()
		{
			var indexName = this.RandomString();
			var project = Project.Generator.Generate(1).First();
			var indexResult = this.Client.Index(project, f => f .Index(indexName));
			indexResult.IsValid.Should().BeTrue();
			indexResult.ApiCall.HttpStatusCode.Should().Be(201);
			indexResult.Created.Should().BeTrue();
			indexResult.Index.Should().Be(indexName);
			indexResult.Type.Should().Be(this.Client.Infer.TypeName<Project>());
			indexResult.Id.Should().Be(project.Name);
			indexResult.Version.Should().Be(1);

			indexResult = this.Client.Index(project, f => f .Index(indexName));

			indexResult.IsValid.Should().BeTrue();
			indexResult.Created.Should().BeFalse();
			indexResult.ApiCall.HttpStatusCode.Should().Be(200);
			indexResult.Version.Should().Be(2);
		}

	}
}
