using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Document.Single.Update
{
	public class UpdateWithScriptApiTests : ApiIntegrationTestBase<WritableCluster, IUpdateResponse<Project>, IUpdateRequest<Project, Project>, UpdateDescriptor<Project, Project>, UpdateRequest<Project, Project>>
	{
		protected override bool NoClientSerializeOfExpected => true;

		public UpdateWithScriptApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
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
		protected override string UrlPath => $"/project/doc/{CallIsolatedValue}/_update";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			scripted_upsert = true,
			script = new
			{
				source = "ctx._source.name = \"params.name\"",
				lang = "painless",
				@params = new {
					name = "foo",
					other = (object)null
				}
			}
		};

		protected override UpdateDescriptor<Project, Project> NewDescriptor() => new UpdateDescriptor<Project, Project>(DocumentPath<Project>.Id(CallIsolatedValue));

		protected override Func<UpdateDescriptor<Project, Project>, IUpdateRequest<Project, Project>> Fluent => d => d
			.ScriptedUpsert()
			.Script(s => s
				.Source("ctx._source.name = \"params.name\"")
				.Lang("painless")
				.Params(p => p
					.Add("name", "foo")
					.Add("other", null)
				)
			 );

		protected override UpdateRequest<Project, Project> Initializer => new UpdateRequest<Project, Project>(CallIsolatedValue)
		{
			ScriptedUpsert = true,
			Script = new InlineScript("ctx._source.name = \"params.name\"")
			{
				Lang = "painless",
				Params = new Dictionary<string, object>
				{
					{ "name", "foo" },
					{ "other", null }
				}
			}

		};
	}
}


