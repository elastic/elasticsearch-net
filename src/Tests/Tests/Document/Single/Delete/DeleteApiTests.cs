using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Document.Single.Delete
{
	public class DeleteApiTests
		: ApiIntegrationTestBase<WritableCluster, IDeleteResponse, IDeleteRequest, DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Delete<Project>(CallIsolatedValue),
			(client, f) => client.DeleteAsync<Project>(CallIsolatedValue),
			(client, r) => client.Delete(r),
			(client, r) => client.DeleteAsync(r)
		);

		protected override void ExpectResponse(IDeleteResponse response)
		{
			response.ShouldBeValid();
#pragma warning disable 618
			response.Found.Should().BeTrue();
#pragma warning restore 618
			response.Result.Should().Be(Result.Deleted);
		}
	}

	public class DeleteNonExistentDocumentApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IDeleteResponse, IDeleteRequest, DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 404;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Delete<Project>(CallIsolatedValue),
			(client, f) => client.DeleteAsync<Project>(CallIsolatedValue),
			(client, r) => client.Delete(r),
			(client, r) => client.DeleteAsync(r)
		);

		protected override void ExpectResponse(IDeleteResponse response)
		{
#pragma warning disable 618
			response.Found.Should().BeFalse();
#pragma warning restore 618
			response.Index.Should().Be("project");
			response.Type.Should().Be("project");
			response.Id.Should().Be(CallIsolatedValue);
		}
	}
}
