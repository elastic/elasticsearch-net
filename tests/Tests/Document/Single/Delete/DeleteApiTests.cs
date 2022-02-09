// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Delete;

// This uses the auto-generated public API
public class DeleteApiTests
	: ApiIntegrationTestBase<WritableCluster, DeleteResponse, DeleteRequestDescriptor, DeleteRequest>
{
	public DeleteApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;
	protected override HttpMethod HttpMethod => HttpMethod.DELETE;

	protected override Action<DeleteRequestDescriptor> Fluent => d => d.Routing(CallIsolatedValue);
	protected override DeleteRequest Initializer => new(Infer.Index<Project>(), CallIsolatedValue) { Routing = CallIsolatedValue };

	protected override bool SupportsDeserialization => false;

	protected override string ExpectedUrlPathAndQuery => $"/project/_doc/{CallIsolatedValue}?routing={U(CallIsolatedValue)}";

	protected override void IntegrationSetup(IElasticsearchClient client, CallUniqueValues values)
	{
		foreach (var id in values.Values)
			Client.Index(Project.Instance, i => i.Id(id).Routing(id));
	}

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Delete(Infer.Index<Project>(), CallIsolatedValue, f), // TODO: Should this client method require the index name and ID?
		(client, f) => client.DeleteAsync(Infer.Index<Project>(), CallIsolatedValue, f),
		(client, r) => client.Delete(r),
		(client, r) => client.DeleteAsync(r)
	);

	protected override DeleteRequestDescriptor NewDescriptor() => new(Infer.Index<Project>(), CallIsolatedValue); // TODO: Should have an overload without these since the client methods require them

	protected override void ExpectResponse(DeleteResponse response)
	{
		response.ShouldBeValid();
		response.Result.Should().Be(Result.Deleted);
		response.Shards.Should().NotBeNull();
		response.Shards.Total.Should().BeGreaterOrEqualTo(1);
		response.Shards.Successful.Should().BeGreaterOrEqualTo(1);
		response.PrimaryTerm.Should().BeGreaterThan(0);
		response.SeqNo.Should().BeGreaterThan(0);
	}
}

// These use the 7.x public API surface which requires some manual generation at the moment
public class OriginalDeleteApiTests
	: ApiIntegrationTestBase<WritableCluster, DeleteResponse, DeleteRequestDescriptor<Project>, DeleteRequest<Project>>
{
	public OriginalDeleteApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;
	protected override HttpMethod HttpMethod => HttpMethod.DELETE;

	protected override Action<DeleteRequestDescriptor<Project>> Fluent => d => d.Routing(CallIsolatedValue);
	protected override DeleteRequest<Project> Initializer => new(CallIsolatedValue) { Routing = CallIsolatedValue };

	protected override bool SupportsDeserialization => false;
	protected override string ExpectedUrlPathAndQuery => $"/project/_doc/{CallIsolatedValue}?routing={U(CallIsolatedValue)}";

	protected override void IntegrationSetup(IElasticsearchClient client, CallUniqueValues values)
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

	protected override DeleteRequestDescriptor<Project> NewDescriptor() => new(CallIsolatedValue);

	protected override void ExpectResponse(DeleteResponse response)
	{
		response.ShouldBeValid();
		response.Result.Should().Be(Result.Deleted);
		response.Shards.Should().NotBeNull();
		response.Shards.Total.Should().BeGreaterOrEqualTo(1);
		response.Shards.Successful.Should().BeGreaterOrEqualTo(1);
		response.PrimaryTerm.Should().BeGreaterThan(0);
		response.SeqNo.Should().BeGreaterThan(0);
	}
}

public class DeleteNonExistentDocumentApiTests
	: ApiIntegrationTestBase<ReadOnlyCluster, DeleteResponse, DeleteRequestDescriptor<Project>, DeleteRequest<Project>>
{
	public DeleteNonExistentDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => false;
	protected override int ExpectStatusCode => 404;
	protected override HttpMethod HttpMethod => HttpMethod.DELETE;

	protected override Action<DeleteRequestDescriptor<Project>> Fluent => d => d.Routing(CallIsolatedValue);
	protected override DeleteRequest<Project> Initializer => new(CallIsolatedValue) { Routing = CallIsolatedValue };

	protected override bool SupportsDeserialization => false;
	protected override string ExpectedUrlPathAndQuery => $"/project/_doc/{CallIsolatedValue}?routing={U(CallIsolatedValue)}";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Delete(CallIsolatedValue, f),
		(client, f) => client.DeleteAsync(CallIsolatedValue, f),
		(client, r) => client.Delete(r),
		(client, r) => client.DeleteAsync(r)
	);

	protected override DeleteRequestDescriptor<Project> NewDescriptor() => new(CallIsolatedValue);

	protected override void ExpectResponse(DeleteResponse response)
	{
		response.ShouldNotBeValid();
		response.Result.Should().Be(Result.NotFound);
		response.Index.Should().Be("project");
		response.Id.Should().Be(CallIsolatedValue);
		response.Shards.Total.Should().BeGreaterOrEqualTo(1);
		response.Shards.Successful.Should().BeGreaterOrEqualTo(1);
		response.PrimaryTerm.Should().BeGreaterThan(0);
		response.SeqNo.Should().BeGreaterThan(0);
	}
}

public class DeleteNonExistentIndexDocumentApiTests
	: ApiIntegrationTestBase<ReadOnlyCluster, DeleteResponse, DeleteRequestDescriptor<Project>, DeleteRequest<Project>>
{
	public DeleteNonExistentIndexDocumentApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => false;
	protected override int ExpectStatusCode => 404;

	protected override HttpMethod HttpMethod => HttpMethod.DELETE;

	protected override Action<DeleteRequestDescriptor<Project>> Fluent => d => d.Index(BadIndex).Routing(CallIsolatedValue);
	protected override DeleteRequest<Project> Initializer => new(BadIndex, CallIsolatedValue) { Routing = CallIsolatedValue };

	protected override bool SupportsDeserialization => false;
	protected override string ExpectedUrlPathAndQuery => $"/{BadIndex}/_doc/{CallIsolatedValue}?routing={U(CallIsolatedValue)}";

	private string BadIndex => CallIsolatedValue + "-bad-index";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Delete(CallIsolatedValue, f),
		(client, f) => client.DeleteAsync(CallIsolatedValue, f),
		(client, r) => client.Delete(r),
		(client, r) => client.DeleteAsync(r)
	);

	protected override DeleteRequestDescriptor<Project> NewDescriptor() => new(index: CallIsolatedValue, id: CallIsolatedValue);

	protected override void ExpectResponse(DeleteResponse response)
	{
		response.ShouldNotBeValid();
		//response.Result.Should().Be(Result.Error); TODO: This doesn't work until a valid default is set
		response.ServerError.Should().NotBeNull();
		response.ServerError.Error.Reason.Should().StartWith("no such index");
	}
}
