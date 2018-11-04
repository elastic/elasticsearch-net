using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.Document.Single.Update
{
	public class UpdateWithSourceApiTests
		: ApiIntegrationTestBase<WritableCluster, IUpdateResponse<Project>, IUpdateRequest<Project, Project>, UpdateDescriptor<Project, Project>,
			UpdateRequest<Project, Project>>
	{
		public UpdateWithSourceApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			doc = Project.InstanceAnonymous,
			doc_as_upsert = true
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateDescriptor<Project, Project>, IUpdateRequest<Project, Project>> Fluent => d => d
			.Doc(Project.Instance)
			.Fields(Field<Project>(p => p.Name).And("_source"))
			.DocAsUpsert();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UpdateRequest<Project, Project> Initializer => new UpdateRequest<Project, Project>(CallIsolatedValue)
		{
			Doc = Project.Instance,
			DocAsUpsert = true,
			Fields = Field<Project>(p => p.Name).And("_source")
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}/_update?fields=name%2C_source";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Update<Project>(CallIsolatedValue, f),
			(client, f) => client.UpdateAsync<Project>(CallIsolatedValue, f),
			(client, r) => client.Update<Project>(r),
			(client, r) => client.UpdateAsync<Project>(r)
		);

		protected override UpdateDescriptor<Project, Project> NewDescriptor() =>
			new UpdateDescriptor<Project, Project>(DocumentPath<Project>.Id(CallIsolatedValue));

		[I] public Task ReturnsSourceAndFields() => AssertOnAllResponses(r =>
		{
			r.Get.Should().NotBeNull();
			r.Get.Found.Should().BeTrue();
			r.Get.Source.Should().NotBeNull();
			var name = Project.First.Name;
			r.Get.Source.Name.Should().Be(name);
			r.Get.Fields.Should().NotBeEmpty().And.ContainKey("name");
			r.Get.Fields.Value<string>("name").Should().Be(name);
		});
	}
}
