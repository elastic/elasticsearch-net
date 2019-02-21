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
		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_doc/{CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Delete<Project>(CallIsolatedValue, f),
			(client, f) => client.DeleteAsync<Project>(CallIsolatedValue, f),
			(client, r) => client.Delete(r),
			(client, r) => client.DeleteAsync(r)
		);

		protected override DeleteDescriptor<Project> NewDescriptor() => new DeleteDescriptor<Project>(CallIsolatedValue);

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

	public class DeleteNonExistentDocumentApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IDeleteResponse, IDeleteRequest,
			DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_doc/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Delete<Project>(CallIsolatedValue, f),
			(client, f) => client.DeleteAsync<Project>(CallIsolatedValue, f),
			(client, r) => client.Delete(r),
			(client, r) => client.DeleteAsync(r)
		);

		protected override DeleteDescriptor<Project> NewDescriptor() => new DeleteDescriptor<Project>(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteResponse response)
		{
			response.ShouldNotBeValid();
			response.Result.Should().Be(Result.NotFound);
			response.Index.Should().Be("project");
			response.Type.Should().Be("doc");
			response.Id.Should().Be(CallIsolatedValue);
			response.Shards.Total.Should().BeGreaterOrEqualTo(1);
			response.Shards.Successful.Should().BeGreaterOrEqualTo(1);
			response.PrimaryTerm.Should().BeGreaterThan(0);
			response.SequenceNumber.Should().BeGreaterThan(0);
		}
	}

	public class DeleteNonExistentIndexDocumentApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IDeleteResponse, IDeleteRequest, DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteNonExistentIndexDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => f => f.Index(BadIndex);
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(BadIndex, CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{BadIndex}/_doc/{CallIsolatedValue}";

		private string BadIndex => CallIsolatedValue + "-bad-index";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Delete<Project>(CallIsolatedValue, f),
			(client, f) => client.DeleteAsync<Project>(CallIsolatedValue, f),
			(client, r) => client.Delete(r),
			(client, r) => client.DeleteAsync(r)
		);

		protected override DeleteDescriptor<Project> NewDescriptor() =>
			new DeleteDescriptor<Project>(index: CallIsolatedValue, id: CallIsolatedValue);

		protected override void ExpectResponse(IDeleteResponse response)
		{
			response.ShouldNotBeValid();
			response.Result.Should().Be(Result.Error);
			response.ServerError.Should().NotBeNull();
			response.ServerError.Error.Reason.Should().Be("no such index");
		}
	}
}
