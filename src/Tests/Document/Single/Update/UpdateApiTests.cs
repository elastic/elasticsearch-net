using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Update
{
	[Collection(IntegrationContext.Indexing)]
	public class UpdateApiTests : ApiIntegrationTestBase<IUpdateResponse<Project>, IUpdateRequest<Project, Project>, UpdateDescriptor<Project, Project>, UpdateRequest<Project, Project>>
	{
		public UpdateApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}/_update";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			doc = Project.InstanceAnonymous,
			doc_as_upsert = true,
			detect_noop = true
		};

		protected override UpdateDescriptor<Project, Project> NewDescriptor() => new UpdateDescriptor<Project, Project>(DocumentPath<Project>.Id(CallIsolatedValue));

		protected override Func<UpdateDescriptor<Project, Project>, IUpdateRequest<Project, Project>> Fluent => u => u
			 .Doc(Project.Instance)
			 .DocAsUpsert()
			 .DetectNoop();

		protected override UpdateRequest<Project, Project> Initializer => new UpdateRequest<Project, Project>(CallIsolatedValue)
		{
			Doc = Project.Instance,
			DocAsUpsert = true,
			DetectNoop = true
		};
	}
}
