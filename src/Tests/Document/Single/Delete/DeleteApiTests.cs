using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using FluentAssertions;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Document.Single.Delete
{
	public class DeleteApiTests : ApiIntegrationTestBase<WritableCluster, IDeleteResponse, IDeleteRequest, DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				this.Client.Index(Project.Instance, i=>i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Delete<Project>(CallIsolatedValue),
			fluentAsync: (client, f) => client.DeleteAsync<Project>(CallIsolatedValue),
			request: (client, r) => client.Delete(r),
			requestAsync: (client, r) => client.DeleteAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => null;
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteResponse response)
		{
			response.ShouldBeValid();
			response.Result.Should().Be(Result.Deleted);
			response.Shards.Should().NotBeNull();
			response.Shards.Total.Should().BeGreaterOrEqualTo(1);
			response.Shards.Successful.Should().BeGreaterOrEqualTo(1);
			response.PrimaryTerm.Should().BeGreaterThan(0);
			response.SequenceNumber.Should().BeGreaterThan(0);
		}
	}

	public class DeleteNonExistentDocumentApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IDeleteResponse, IDeleteRequest, DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Delete<Project>(CallIsolatedValue),
			fluentAsync: (client, f) => client.DeleteAsync<Project>(CallIsolatedValue),
			request: (client, r) => client.Delete(r),
			requestAsync: (client, r) => client.DeleteAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => null;
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteResponse response)
		{
			response.ShouldBeValid();
			response.Result.Should().Be(Result.NotFound);
			response.Index.Should().Be("project");
			response.Type.Should().Be("project");
			response.Id.Should().Be(this.CallIsolatedValue);
			response.Shards.Total.Should().BeGreaterOrEqualTo(1);
			response.Shards.Successful.Should().BeGreaterOrEqualTo(1);
			response.PrimaryTerm.Should().BeGreaterThan(0);
			response.SequenceNumber.Should().BeGreaterThan(0);
		}
	}
}
