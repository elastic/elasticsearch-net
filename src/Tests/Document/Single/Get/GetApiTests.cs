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

namespace Tests.Document.Single.Get
{
	[Collection(IntegrationContext.ReadOnly)]
	public class GetApiTests : ApiIntegrationTestBase<IGetResponse<Project>, IGetRequest, GetDescriptor<Project>, GetRequest<Project>>
	{
		private string ProjectId => Project.Projects.First().Name;

		public GetApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Get<Project>(this.ProjectId),
			fluentAsync: (client, f) => client.GetAsync<Project>(this.ProjectId),
			request: (client, r) => client.Get<Project>(r),
			requestAsync: (client, r) => client.GetAsync<Project>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/project/project/{this.ProjectId}";

		protected override bool SupportsDeserialization => false;

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => null;
		protected override GetRequest<Project> Initializer => new GetRequest<Project>(this.ProjectId);

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
