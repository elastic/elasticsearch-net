using System;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using FluentAssertions;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Document.Single.Get
{
	public class GetApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetResponse<Project>, IGetRequest, GetDescriptor<Project>, GetRequest<Project>>
	{
		protected string ProjectId => Project.First.Name;

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
		protected override string UrlPath => $"/project/project/{UrlEncode(this.ProjectId)}";

		protected override bool SupportsDeserialization => false;

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => null;

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(this.ProjectId);

		protected override void ExpectResponse(IGetResponse<Project> response)
		{
			response.Source.Should().NotBeNull();
			response.Source.Name.Should().Be(ProjectId);
		}
	}

	public class GetNonExistentDocumentApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetResponse<Project>, IGetRequest, GetDescriptor<Project>, GetRequest<Project>>
	{
		protected string ProjectId => this.CallIsolatedValue;

	    public GetNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Get<Project>(this.ProjectId, f),
			fluentAsync: (client, f) => client.GetAsync<Project>(this.ProjectId, f),
			request: (client, r) => client.Get<Project>(r),
			requestAsync: (client, r) => client.GetAsync<Project>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/project/project/{UrlEncode(this.ProjectId)}";

		protected override bool SupportsDeserialization => false;

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => null;

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(this.ProjectId);

		protected override void ExpectResponse(IGetResponse<Project> response)
		{
			response.Found.Should().BeFalse();
			response.Index.Should().Be("project");
			response.Type.Should().Be("project");
			response.Id.Should().Be(this.CallIsolatedValue);
		}
	}

	public class GetApiParentTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetResponse<CommitActivity>, IGetRequest, GetDescriptor<CommitActivity>, GetRequest<CommitActivity>>
	{
		protected CommitActivity CommitActivity => CommitActivity.CommitActivities.First();

		protected string CommitActivityId => CommitActivity.Id;

	    public GetApiParentTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Get<CommitActivity>(this.CommitActivityId, f),
			fluentAsync: (client, f) => client.GetAsync<CommitActivity>(this.CommitActivityId, f),
			request: (client, r) => client.Get<CommitActivity>(r),
			requestAsync: (client, r) => client.GetAsync<CommitActivity>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/commits/commits/{UrlEncode(this.CommitActivityId)}?parent={UrlEncode(this.CommitActivity.ProjectName)}";

		protected override bool SupportsDeserialization => false;

		protected override GetDescriptor<CommitActivity> NewDescriptor() => new GetDescriptor<CommitActivity>(CommitActivity);

		protected override Func<GetDescriptor<CommitActivity>, IGetRequest> Fluent => g => g
			.Parent(this.CommitActivity.ProjectName)
			;

		protected override GetRequest<CommitActivity> Initializer => new GetRequest<CommitActivity>(this.CommitActivityId)
		{
			Parent = this.CommitActivity.ProjectName
		};

		protected override void ExpectResponse(IGetResponse<CommitActivity> response)
		{
			response.Source.Should().NotBeNull();
			response.Source.Id.Should().Be(CommitActivityId);
			response.Parent.Should().NotBeNullOrEmpty();
			response.Routing.Should().NotBeNullOrEmpty();
		}
	}

	public class GetApiFieldsTests : GetApiTests
	{
		public GetApiFieldsTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string UrlPath => $"/project/project/{UrlEncode(this.ProjectId)}?stored_fields=name%2CnumberOfCommits";

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => g => g
			.StoredFields(
				p => p.Name,
				p => p.NumberOfCommits
			);

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(ProjectId)
		{
			StoredFields = Infer.Fields<Project>(p => p.Name, p => p.NumberOfCommits)
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
