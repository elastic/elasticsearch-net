// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Delete
{
	public class DeleteApiTests
		: ApiIntegrationTestBase<WritableCluster, DeleteResponse, IDeleteRequest, DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => d => d.Routing(CallIsolatedValue);
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue) { Routing = CallIsolatedValue };

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_doc/{CallIsolatedValue}?routing={U(CallIsolatedValue)}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id).Routing(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Delete(CallIsolatedValue, f),
			(client, f) => client.DeleteAsync(CallIsolatedValue, f),
			(client, r) => client.Delete(r),
			(client, r) => client.DeleteAsync(r)
		);

		protected override DeleteDescriptor<Project> NewDescriptor() => new DeleteDescriptor<Project>(CallIsolatedValue);

		protected override void ExpectResponse(DeleteResponse response)
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
		: ApiIntegrationTestBase<ReadOnlyCluster, DeleteResponse, IDeleteRequest,
			DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => d => d.Routing(CallIsolatedValue);
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(CallIsolatedValue) { Routing = CallIsolatedValue };

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_doc/{CallIsolatedValue}?routing={U(CallIsolatedValue)}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Delete(CallIsolatedValue, f),
			(client, f) => client.DeleteAsync(CallIsolatedValue, f),
			(client, r) => client.Delete(r),
			(client, r) => client.DeleteAsync(r)
		);

		protected override DeleteDescriptor<Project> NewDescriptor() => new DeleteDescriptor<Project>(CallIsolatedValue);

		protected override void ExpectResponse(DeleteResponse response)
		{
			response.ShouldNotBeValid();
			response.Result.Should().Be(Result.NotFound);
			response.Index.Should().Be("project");
			response.Id.Should().Be(CallIsolatedValue);
			response.Shards.Total.Should().BeGreaterOrEqualTo(1);
			response.Shards.Successful.Should().BeGreaterOrEqualTo(1);
			response.PrimaryTerm.Should().BeGreaterThan(0);
			response.SequenceNumber.Should().BeGreaterThan(0);
		}
	}

	public class DeleteNonExistentIndexDocumentApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, DeleteResponse, IDeleteRequest, DeleteDescriptor<Project>, DeleteRequest<Project>>
	{
		public DeleteNonExistentIndexDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override Func<DeleteDescriptor<Project>, IDeleteRequest> Fluent => d => d.Index(BadIndex).Routing(CallIsolatedValue);
		protected override DeleteRequest<Project> Initializer => new DeleteRequest<Project>(BadIndex, CallIsolatedValue) { Routing = CallIsolatedValue };

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{BadIndex}/_doc/{CallIsolatedValue}?routing={U(CallIsolatedValue)}";

		private string BadIndex => CallIsolatedValue + "-bad-index";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Delete(CallIsolatedValue, f),
			(client, f) => client.DeleteAsync(CallIsolatedValue, f),
			(client, r) => client.Delete(r),
			(client, r) => client.DeleteAsync(r)
		);

		protected override DeleteDescriptor<Project> NewDescriptor() =>
			new DeleteDescriptor<Project>(index: CallIsolatedValue, id: CallIsolatedValue);

		protected override void ExpectResponse(DeleteResponse response)
		{
			response.ShouldNotBeValid();
			response.Result.Should().Be(Result.Error);
			response.ServerError.Should().NotBeNull();
			response.ServerError.Error.Reason.Should().StartWith("no such index");
		}
	}
}
