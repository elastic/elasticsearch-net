using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Document.Single.Get
{
	public class GetApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGetResponse<Project>, IGetRequest, GetDescriptor<Project>, GetRequest<Project>>
	{
		protected string ProjectId => Project.First.Name;

		public GetApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Get<Project>(ProjectId, f),
			fluentAsync: (client, f) => client.GetAsync<Project>(ProjectId, f),
			request: (client, r) => client.Get<Project>(r),
			requestAsync: (client, r) => client.GetAsync<Project>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/project/doc/{U(ProjectId)}?routing={U(ProjectId)}";

		protected override bool SupportsDeserialization => false;

		protected override GetDescriptor<Project> NewDescriptor() => new GetDescriptor<Project>(ProjectId);

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => g => g.Routing(ProjectId);

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(ProjectId)
		{
			Routing = ProjectId
		};

		protected override void ExpectResponse(IGetResponse<Project> response)
		{
			response.Source.Should().NotBeNull();
			response.Source.Name.Should().Be(ProjectId);
			response.Source.ShouldAdhereToSourceSerializerWhenSet();
		}
	}

	public class GetNonExistentDocumentApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGetResponse<Project>, IGetRequest, GetDescriptor<Project>, GetRequest<Project>>
	{
		protected string ProjectId => CallIsolatedValue;

		public GetNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Get<Project>(ProjectId, f),
			fluentAsync: (client, f) => client.GetAsync<Project>(ProjectId, f),
			request: (client, r) => client.Get<Project>(r),
			requestAsync: (client, r) => client.GetAsync<Project>(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/project/doc/{U(ProjectId)}?routing={U(ProjectId)}";

		protected override bool SupportsDeserialization => false;

		protected override GetDescriptor<Project> NewDescriptor() => new GetDescriptor<Project>(ProjectId);

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => g => g.Routing(ProjectId);

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(ProjectId)
		{
			Routing = ProjectId
		};

		protected override void ExpectResponse(IGetResponse<Project> response)
		{
			response.Found.Should().BeFalse();
			response.Index.Should().Be("project");
			response.Type.Should().Be("doc");
			response.Id.Should().Be(CallIsolatedValue);
		}
	}

	public class GetNonExistentIndexDocumentApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGetResponse<Project>, IGetRequest, GetDescriptor<Project>, GetRequest<Project>>
	{
		protected string ProjectId => CallIsolatedValue;
		protected string BadIndex => CallIsolatedValue + "-index";

		public GetNonExistentIndexDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Get<Project>(ProjectId, f),
			fluentAsync: (client, f) => client.GetAsync<Project>(ProjectId, f),
			request: (client, r) => client.Get<Project>(r),
			requestAsync: (client, r) => client.GetAsync<Project>(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/{BadIndex}/doc/{U(ProjectId)}";

		protected override bool SupportsDeserialization => false;

		protected override GetDescriptor<Project> NewDescriptor() =>
			new GetDescriptor<Project>(DocumentPath<Project>.Id(ProjectId).Index(BadIndex));

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => (g) => g.Index(BadIndex);

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(ProjectId, index: BadIndex);

		protected override void ExpectResponse(IGetResponse<Project> response)
		{
			response.Found.Should().BeFalse();
			response.Index.Should().BeNullOrWhiteSpace();
			response.ServerError.Should().NotBeNull();
		}
	}

	public class GetApiParentTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IGetResponse<CommitActivity>, IGetRequest, GetDescriptor<CommitActivity>, GetRequest<CommitActivity>
		>
	{
		protected CommitActivity CommitActivity => CommitActivity.CommitActivities.First();

		protected string CommitActivityId => CommitActivity.Id;

		public GetApiParentTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Get<CommitActivity>(CommitActivityId, f),
			fluentAsync: (client, f) => client.GetAsync<CommitActivity>(CommitActivityId, f),
			request: (client, r) => client.Get<CommitActivity>(r),
			requestAsync: (client, r) => client.GetAsync<CommitActivity>(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/project/doc/{U(CommitActivityId)}?routing={U(CommitActivity.ProjectName)}";

		protected override bool SupportsDeserialization => false;

		protected override GetDescriptor<CommitActivity> NewDescriptor() => new GetDescriptor<CommitActivity>(CommitActivity);

		protected override Func<GetDescriptor<CommitActivity>, IGetRequest> Fluent => g => g
			.Routing(CommitActivity.ProjectName);

		protected override GetRequest<CommitActivity> Initializer => new GetRequest<CommitActivity>(CommitActivityId)
		{
			Routing = CommitActivity.ProjectName
		};

		protected override void ExpectResponse(IGetResponse<CommitActivity> response)
		{
			response.Source.Should().NotBeNull();
			response.Source.Id.Should().Be(CommitActivityId);
			response.Routing.Should().NotBeNullOrEmpty();
#pragma warning disable 618
			response.Parent.Should().BeNullOrEmpty();
#pragma warning restore 618
		}
	}

	public class GetApiFieldsTests : GetApiTests
	{
		public GetApiFieldsTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string UrlPath => $"/project/doc/{U(ProjectId)}?stored_fields=name%2CnumberOfCommits&routing={U(ProjectId)}";

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => g => g
			.Routing(ProjectId)
			.StoredFields(
				p => p.Name,
				p => p.NumberOfCommits
			);

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(ProjectId)
		{
			Routing = ProjectId,
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
