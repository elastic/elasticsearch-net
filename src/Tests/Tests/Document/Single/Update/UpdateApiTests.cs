using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Document.Single.Update
{
	public class UpdateApiTests
		: ApiIntegrationTestBase<WritableCluster, IUpdateResponse<Project>, IUpdateRequest<Project, Project>, UpdateDescriptor<Project, Project>,
			UpdateRequest<Project, Project>>
	{
		public UpdateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			doc = Project.InstanceAnonymous,
			doc_as_upsert = true,
			detect_noop = true
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateDescriptor<Project, Project>, IUpdateRequest<Project, Project>> Fluent => u => u
			.Routing(CallIsolatedValue)
			.Doc(Project.Instance)
			.DocAsUpsert()
			.DetectNoop();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UpdateRequest<Project, Project> Initializer => new UpdateRequest<Project, Project>(CallIsolatedValue)
		{
			Routing = CallIsolatedValue,
			Doc = Project.Instance,
			DocAsUpsert = true,
			DetectNoop = true
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_update/{CallIsolatedValue}?routing={CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id).Routing(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Update<Project>(CallIsolatedValue, f),
			(client, f) => client.UpdateAsync<Project>(CallIsolatedValue, f),
			(client, r) => client.Update<Project>(r),
			(client, r) => client.UpdateAsync<Project>(r)
		);

		protected override UpdateDescriptor<Project, Project> NewDescriptor() =>
			new UpdateDescriptor<Project, Project>(CallIsolatedValue);

		protected override void ExpectResponse(IUpdateResponse<Project> response)
		{
			response.ShouldBeValid();
			response.Result.Should().Be(Result.Noop);
		}
	}
}
