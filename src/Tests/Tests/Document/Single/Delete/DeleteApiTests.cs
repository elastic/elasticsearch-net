using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using FluentAssertions;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
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
			fluent: (client, f) => client.Delete<Project>(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteAsync<Project>(CallIsolatedValue, f),
			request: (client, r) => client.Delete(r),
			requestAsync: (client, r) => client.DeleteAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/project/doc/{CallIsolatedValue}?routing={U(Project.Routing)}";

		protected override bool SupportsDeserialization => false;

		protected override DeleteDescriptor<Project> NewDescriptor() => new DeleteDescriptor<Project>(CallIsolatedValue);
		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => d => d.Routing(Project.Instance.Name);
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue)
		{
			Routing = Project.Instance.Name
		};

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

	public class DeleteNonExistentDocumentApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IDeleteResponse, IDeleteRequest,
		DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Delete<Project>(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteAsync<Project>(CallIsolatedValue, f),
			request: (client, r) => client.Delete(r),
			requestAsync: (client, r) => client.DeleteAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/project/doc/{CallIsolatedValue}?routing={CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override DeleteDescriptor<Project> NewDescriptor() => new DeleteDescriptor<Project>(CallIsolatedValue);
		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => d => d.Routing(CallIsolatedValue);
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue)
		{
			Routing = CallIsolatedValue
		};

		protected override void ExpectResponse(IDeleteResponse response)
		{
			response.ShouldNotBeValid();
			response.Result.Should().Be(Result.NotFound);
			response.Index.Should().Be("project");
			response.Type.Should().Be("doc");
			response.Id.Should().Be(this.CallIsolatedValue);
			response.Shards.Total.Should().BeGreaterOrEqualTo(1);
			response.Shards.Successful.Should().BeGreaterOrEqualTo(1);
			response.PrimaryTerm.Should().BeGreaterThan(0);
			response.SequenceNumber.Should().BeGreaterThan(0);
		}
	}

	public class DeleteNonExistentIndexDocumentApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IDeleteResponse, IDeleteRequest, DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteNonExistentIndexDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private string BadIndex => CallIsolatedValue + "-bad-index";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Delete<Project>(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.DeleteAsync<Project>(CallIsolatedValue, f),
			request: (client, r) => client.Delete(r),
			requestAsync: (client, r) => client.DeleteAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/{BadIndex}/doc/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override DeleteDescriptor<Project> NewDescriptor() =>
			new DeleteDescriptor<Project>(DocumentPath<Project>.Id(CallIsolatedValue).Index(CallIsolatedValue));

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => f => f.Index(BadIndex);
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue, index: BadIndex);

		protected override void ExpectResponse(IDeleteResponse response)
		{
			response.ShouldNotBeValid();
			response.Result.Should().Be(Result.Error);
			response.ServerError.Should().NotBeNull();
			response.ServerError.Error.Reason.Should().Be("no such index");
		}
	}
}
