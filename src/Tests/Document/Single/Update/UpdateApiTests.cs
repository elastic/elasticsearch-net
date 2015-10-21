using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Update
{
	[Collection(IntegrationContext.Indexing)]
	public class UpdateApiTests : ApiIntegrationTestBase<IUpdateResponse, IUpdateRequest<Project, Project>, UpdateDescriptor<Project, Project>, UpdateRequest<Project, Project>>
	{
		public UpdateApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Update<Project>(Project.Instance, f),
			fluentAsync: (client, f) => client.UpdateAsync<Project>(Project.Instance, f),
			request: (client, r) => client.Update<Project>(r),
			requestAsync: (client, r) => client.UpdateAsync<Project>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/project/{Project.Instance.Name}/_update";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			doc = Project.InstanceAnonymous,
			doc_as_upsert = true
		};

		protected override UpdateDescriptor<Project, Project> NewDescriptor() => new UpdateDescriptor<Project, Project>(Project.Instance);

		protected override Func<UpdateDescriptor<Project,Project>, IUpdateRequest<Project, Project>> Fluent => d=>d
			.Doc(Project.Instance)
			.DocAsUpsert();

		protected override UpdateRequest<Project, Project> Initializer => new UpdateRequest<Project, Project>(Project.Instance.Name)
		{
			Doc = Project.Instance,
			DocAsUpsert = true
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
