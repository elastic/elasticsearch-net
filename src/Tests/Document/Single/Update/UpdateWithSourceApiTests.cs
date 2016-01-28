using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;

namespace Tests.Document.Single.Update
{
	[Collection(IntegrationContext.Indexing)]
	public class UpdateWithSourceApiTests : ApiIntegrationTestBase<IUpdateResponse<Project>, IUpdateRequest<Project, Project>, UpdateDescriptor<Project, Project>, UpdateRequest<Project, Project>>
	{
		public UpdateWithSourceApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
		{
			foreach (var id in values.Values)
				this.Client.Index(Project.Instance, i=>i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Update<Project>(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.UpdateAsync<Project>(CallIsolatedValue, f),
			request: (client, r) => client.Update<Project>(r),
			requestAsync: (client, r) => client.UpdateAsync<Project>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}/_update?fields=name%2C_source";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			doc = Project.InstanceAnonymous,
			doc_as_upsert = true
		};

		protected override UpdateDescriptor<Project, Project> NewDescriptor() => new UpdateDescriptor<Project, Project>(DocumentPath<Project>.Id(CallIsolatedValue));

		protected override Func<UpdateDescriptor<Project,Project>, IUpdateRequest<Project, Project>> Fluent => d=>d
			.Doc(Project.Instance)
			.Fields(Field<Project>(p=>p.Name).And("_source"))
			.DocAsUpsert();

		protected override UpdateRequest<Project, Project> Initializer => new UpdateRequest<Project, Project>(CallIsolatedValue)
		{
			Doc = Project.Instance,
			DocAsUpsert = true,
			Fields = Field<Project>(p=>p.Name).And("_source")
		};

		[I] public Task ReturnsSourceAndFields() => this.AssertOnAllResponses(r =>
		{
			r.Get.Should().NotBeNull();
			r.Get.Found.Should().BeTrue();
			r.Get.Source.Should().NotBeNull();
			var name = Project.Projects.First().Name;
			r.Get.Source.Name.Should().Be(name);
			r.Get.Fields.Should().NotBeEmpty().And.ContainKey("name");
			r.Get.Fields.Value<string>("name").Should().Be(name);
		});
	}
}
