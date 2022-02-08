// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FluentAssertions;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Get
{
	public class GetApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GetResponse<Project>, GetRequestDescriptor, GetRequest>
	{
		public GetApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override Action<GetRequestDescriptor> Fluent => g => g.Routing(ProjectId);
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetRequest Initializer => new(Infer.Index<Project>(), ProjectId)
		{
			Routing = ProjectId
		};

		protected static string ProjectId => Project.First.Name;

		protected override bool SupportsDeserialization => false;
		protected override string ExpectedUrlPathAndQuery => $"/project/_doc/{U(ProjectId)}?routing={U(ProjectId)}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Get<Project>(Infer.Index<Project>(), ProjectId, f),
			(client, f) => client.GetAsync<Project>(Infer.Index<Project>(), ProjectId, f),
			(client, r) => client.Get<Project>(r),
			(client, r) => client.GetAsync<Project>(r)
		);

		protected override GetRequestDescriptor NewDescriptor() => new(Infer.Index<Project>(), ProjectId);

		protected override void ExpectResponse(GetResponse<Project> response)
		{
			response.Source.Should().NotBeNull();
			response.Source.Name.Should().Be(ProjectId);
			response.SeqNo.Should().BeGreaterOrEqualTo(0);
			response.PrimaryTerm.Should().HaveValue();
			//response.Source.ShouldAdhereToSourceSerializerWhenSet(); // TODO
			response.PrimaryTerm.Should().BeGreaterOrEqualTo(1);
		}
	}

	//public class GetNonExistentDocumentApiTests
	//	: ApiIntegrationTestBase<ReadOnlyCluster, GetResponse<Project>, IGetRequest, GetDescriptor<Project>, GetRequest<Project>>
	//{
	//	public GetNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	//	protected override bool ExpectIsValid => false;
	//	protected override int ExpectStatusCode => 404;
	//	protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => g => g.Routing(ProjectId);
	//	protected override HttpMethod HttpMethod => HttpMethod.GET;

	//	protected override GetRequest<Project> Initializer => new GetRequest<Project>(ProjectId)
	//	{
	//		Routing = ProjectId
	//	};

	//	protected string ProjectId => CallIsolatedValue;

	//	protected override bool SupportsDeserialization => false;
	//	protected override string UrlPath => $"/project/_doc/{U(ProjectId)}?routing={U(ProjectId)}";

	//	protected override LazyResponses ClientUsage() => Calls(
	//		(client, f) => client.Get(ProjectId, f),
	//		(client, f) => client.GetAsync(ProjectId, f),
	//		(client, r) => client.Get<Project>(r),
	//		(client, r) => client.GetAsync<Project>(r)
	//	);

	//	protected override GetDescriptor<Project> NewDescriptor() => new GetDescriptor<Project>(ProjectId);

	//	protected override void ExpectResponse(GetResponse<Project> response)
	//	{
	//		response.Found.Should().BeFalse();
	//		response.Index.Should().Be("project");
	//		response.Type.Should().Be("_doc");
	//		response.Id.Should().Be(CallIsolatedValue);
	//	}
	//}

	//public class GetNonExistentIndexDocumentApiTests
	//	: ApiIntegrationTestBase<ReadOnlyCluster, GetResponse<Project>, IGetRequest, GetDescriptor<Project>, GetRequest<Project>>
	//{
	//	public GetNonExistentIndexDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	//	protected string BadIndex => CallIsolatedValue + "-index";

	//	protected override bool ExpectIsValid => false;
	//	protected override int ExpectStatusCode => 404;

	//	protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => (g) => g.Index(BadIndex);
	//	protected override HttpMethod HttpMethod => HttpMethod.GET;

	//	protected override GetRequest<Project> Initializer => new GetRequest<Project>(BadIndex, ProjectId);
	//	protected string ProjectId => CallIsolatedValue;

	//	protected override bool SupportsDeserialization => false;
	//	protected override string UrlPath => $"/{BadIndex}/_doc/{U(ProjectId)}";

	//	protected override LazyResponses ClientUsage() => Calls(
	//		(client, f) => client.Get(ProjectId, f),
	//		(client, f) => client.GetAsync(ProjectId, f),
	//		(client, r) => client.Get<Project>(r),
	//		(client, r) => client.GetAsync<Project>(r)
	//	);

	//	protected override GetDescriptor<Project> NewDescriptor() =>
	//		new GetDescriptor<Project>(index: BadIndex, id: ProjectId);

	//	protected override void ExpectResponse(GetResponse<Project> response)
	//	{
	//		response.Found.Should().BeFalse();
	//		response.Index.Should().BeNullOrWhiteSpace();
	//		response.ServerError.Should().NotBeNull();
	//	}
	//}

	//public class GetApiParentTests
	//	: ApiIntegrationTestBase<ReadOnlyCluster, GetResponse<CommitActivity>, IGetRequest, GetDescriptor<CommitActivity>, GetRequest<CommitActivity>
	//	>
	//{
	//	public GetApiParentTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	//	protected CommitActivity CommitActivity => CommitActivity.CommitActivities.First();

	//	protected string CommitActivityId => CommitActivity.Id;

	//	protected override bool ExpectIsValid => true;
	//	protected override int ExpectStatusCode => 200;

	//	protected override Func<GetDescriptor<CommitActivity>, IGetRequest> Fluent => g => g
	//		.Routing(CommitActivity.ProjectName);

	//	protected override HttpMethod HttpMethod => HttpMethod.GET;

	//	protected override GetRequest<CommitActivity> Initializer => new GetRequest<CommitActivity>(CommitActivityId)
	//	{
	//		Routing = CommitActivity.ProjectName
	//	};

	//	protected override bool SupportsDeserialization => false;
	//	protected override string UrlPath => $"/project/_doc/{U(CommitActivityId)}?routing={U(CommitActivity.ProjectName)}";

	//	protected override LazyResponses ClientUsage() => Calls(
	//		(client, f) => client.Get(CommitActivityId, f),
	//		(client, f) => client.GetAsync(CommitActivityId, f),
	//		(client, r) => client.Get<CommitActivity>(r),
	//		(client, r) => client.GetAsync<CommitActivity>(r)
	//	);

	//	protected override GetDescriptor<CommitActivity> NewDescriptor() => new GetDescriptor<CommitActivity>(CommitActivity);

	//	protected override void ExpectResponse(GetResponse<CommitActivity> response)
	//	{
	//		response.Source.Should().NotBeNull();
	//		response.Source.Id.Should().Be(CommitActivityId);
	//		response.Routing.Should().NotBeNullOrEmpty();
	//	}
	//}

	//public class GetApiFieldsTests : GetApiTests
	//{
	//	public GetApiFieldsTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	//	protected override Func<GetDescriptor<Project>, IGetRequest> Fluent => g => g
	//		.Routing(GetApiTests.ProjectId)
	//		.StoredFields(
	//			p => p.Name,
	//			p => p.NumberOfCommits
	//		);

	//	protected override GetRequest<Project> Initializer => new GetRequest<Project>(GetApiTests.ProjectId)
	//	{
	//		Routing = GetApiTests.ProjectId,
	//		StoredFields = Infer.Fields<Project>(p => p.Name, p => p.NumberOfCommits)
	//	};

	//	protected override string UrlPath => $"/project/_doc/{U(GetApiTests.ProjectId)}?stored_fields=name%2CnumberOfCommits&routing={U(GetApiTests.ProjectId)}";

	//	protected override void ExpectResponse(GetResponse<Project> response)
	//	{
	//		response.Fields.Should().NotBeNull();
	//		response.Fields.ValueOf<Project, string>(p => p.Name).Should().Be(GetApiTests.ProjectId);
	//		response.Fields.ValueOf<Project, int?>(p => p.NumberOfCommits).Should().BeGreaterThan(0);
	//	}

	//	protected override GetDescriptor<Project> NewDescriptor() => new GetDescriptor<Project>(GetApiTests.ProjectId);
	//}
}
