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
		public GetApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => g => g.Routing(ProjectId);
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(ProjectId)
		{
			Routing = ProjectId
		};

		protected string ProjectId => Project.First.Name;

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_doc/{U(ProjectId)}?routing={U(ProjectId)}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Get<Project>(ProjectId, f),
			(client, f) => client.GetAsync<Project>(ProjectId, f),
			(client, r) => client.Get<Project>(r),
			(client, r) => client.GetAsync<Project>(r)
		);

		protected override GetDescriptor<Project> NewDescriptor() => new GetDescriptor<Project>(ProjectId);

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
		public GetNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => g => g.Routing(ProjectId);
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(ProjectId)
		{
			Routing = ProjectId
		};

		protected string ProjectId => CallIsolatedValue;

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_doc/{U(ProjectId)}?routing={U(ProjectId)}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Get<Project>(ProjectId, f),
			(client, f) => client.GetAsync<Project>(ProjectId, f),
			(client, r) => client.Get<Project>(r),
			(client, r) => client.GetAsync<Project>(r)
		);

		protected override GetDescriptor<Project> NewDescriptor() => new GetDescriptor<Project>(ProjectId);

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
		public GetNonExistentIndexDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected string BadIndex => CallIsolatedValue + "-index";

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;

		protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => (g) => g.Index(BadIndex);
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetRequest<Project> Initializer => new GetRequest<Project>(BadIndex, ProjectId);
		protected string ProjectId => CallIsolatedValue;

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{BadIndex}/_doc/{U(ProjectId)}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Get<Project>(ProjectId, f),
			(client, f) => client.GetAsync<Project>(ProjectId, f),
			(client, r) => client.Get<Project>(r),
			(client, r) => client.GetAsync<Project>(r)
		);

		protected override GetDescriptor<Project> NewDescriptor() =>
			new GetDescriptor<Project>(index: BadIndex, id: ProjectId);

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
		public GetApiParentTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected CommitActivity CommitActivity => CommitActivity.CommitActivities.First();

		protected string CommitActivityId => CommitActivity.Id;

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<GetDescriptor<CommitActivity>, IGetRequest> Fluent => g => g
			.Routing(CommitActivity.ProjectName);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetRequest<CommitActivity> Initializer => new GetRequest<CommitActivity>(CommitActivityId)
		{
			Routing = CommitActivity.ProjectName
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_doc/{U(CommitActivityId)}?routing={U(CommitActivity.ProjectName)}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Get<CommitActivity>(CommitActivityId, f),
			(client, f) => client.GetAsync<CommitActivity>(CommitActivityId, f),
			(client, r) => client.Get<CommitActivity>(r),
			(client, r) => client.GetAsync<CommitActivity>(r)
		);

		protected override GetDescriptor<CommitActivity> NewDescriptor() => new GetDescriptor<CommitActivity>(CommitActivity);

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

		protected override string UrlPath => $"/project/_doc/{U(ProjectId)}?stored_fields=name%2CnumberOfCommits&routing={U(ProjectId)}";

		protected override void ExpectResponse(IGetResponse<Project> response)
		{
			response.Fields.Should().NotBeNull();
			response.Fields.ValueOf<Project, string>(p => p.Name).Should().Be(ProjectId);
			response.Fields.ValueOf<Project, int?>(p => p.NumberOfCommits).Should().BeGreaterThan(0);
		}

		protected override GetDescriptor<Project> NewDescriptor() => new GetDescriptor<Project>(ProjectId);
	}
}
