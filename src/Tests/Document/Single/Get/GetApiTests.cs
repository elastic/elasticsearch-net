using System;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using FluentAssertions;

namespace Tests.Document.Single.Get
{
	[Collection(IntegrationContext.ReadOnly)]
	public class GetApiTests : ApiIntegrationTestBase<IGetResponse<Project>, IGetRequest, GetDescriptor<Project>, GetRequest<Project>>
	{
		protected string ProjectId => Project.Projects.First().Name;

		protected string ProjectIdForUrl => Uri.EscapeDataString(this.ProjectId);

		public GetApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Get<Project>(this.ProjectId, f),
			fluentAsync: (client, f) => client.GetAsync<Project>(this.ProjectId, f),
			request: (client, r) => client.Get<Project>(r),
			requestAsync: (client, r) => client.GetAsync<Project>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/project/project/{ProjectIdForUrl}";

		protected override bool SupportsDeserialization => false;

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => null;

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(this.ProjectId);

		protected override void ExpectResponse(IGetResponse<Project> response)
		{
			response.Source.Should().NotBeNull();
			response.Source.Name.Should().Be(ProjectId);
		}
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class GetApiLowLevelTests : GetApiTests
	{
		public GetApiLowLevelTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string UrlPath => $"/project/project/{ProjectIdForUrl}?fields=name%2CnumberOfCommits";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Raw.Get<GetResponse<Project>>("project", "project", this.ProjectId, RequestParameter).Body,
			fluentAsync: (client, f) => client.Raw.GetAsync<GetResponse<Project>>("project", "project", this.ProjectId, RequestParameter)
				.ContinueWith<IGetResponse<Project>>(t=>t.Result.Body),
			request: (client, f) => client.Raw.Get<GetResponse<Project>>("project", "project", this.ProjectId, RequestParameter).Body,
			requestAsync: (client, f) => client.Raw.GetAsync<GetResponse<Project>>("project", "project", this.ProjectId, RequestParameter)
				.ContinueWith<IGetResponse<Project>>(t=>t.Result.Body)
		);

		private GetRequestParameters RequestParameter(GetRequestParameters r) => r
			.Fields("name", "numberOfCommits");

		protected override void ExpectResponse(IGetResponse<Project> response)
		{
			response.Fields.Should().NotBeNull();
			response.Fields.ValueOf<Project, string>(p => p.Name).Should().Be(ProjectId);
			response.Fields.ValueOf<Project, int?>(p => p.NumberOfCommits).Should().BeGreaterThan(0);
		}
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class GetApiFieldsTests : GetApiTests
	{
		public GetApiFieldsTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string UrlPath => $"/project/project/{ProjectIdForUrl}?fields=name%2CnumberOfCommits";

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => g => g
			.Fields(
				p => p.Name, 
				p => p.NumberOfCommits
			);

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(ProjectId)
		{
			Fields = Infer.Fields<Project>(p => p.Name, p => p.NumberOfCommits)
		};

		protected override void ExpectResponse(IGetResponse<Project> response)
		{
			response.Fields.Should().NotBeNull();
			response.Fields.ValueOf<Project, string>(p => p.Name).Should().Be(ProjectId);
			response.Fields.ValueOf<Project, int?>(p => p.NumberOfCommits).Should().BeGreaterThan(0);
		}

		protected override GetDescriptor<Project> NewDescriptor() => new GetDescriptor<Project>(ProjectId);
	}
}
