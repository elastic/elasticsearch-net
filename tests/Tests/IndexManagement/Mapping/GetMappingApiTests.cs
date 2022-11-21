// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.EndpointTests;
using Elastic.Clients.Elasticsearch.IndexManagement;
using System;
using Tests.Domain;

namespace Tests.IndexManagement.Mapping;

public class GetMappingApiTests : ApiIntegrationTestBase<ReadOnlyCluster, GetMappingResponse, GetMappingRequestDescriptor<Project>, GetMappingRequest>
{
	public GetMappingApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;
	protected override string ExpectedUrlPathAndQuery => "/project/_mapping?ignore_unavailable=true";
	protected override HttpMethod HttpMethod => HttpMethod.GET;
	protected override Action<GetMappingRequestDescriptor<Project>> Fluent => c => c.Indices(Infer.Index<Project>()).IgnoreUnavailable();
	protected override GetMappingRequest Initializer => new(Infer.Index<Project>()) { IgnoreUnavailable = true };

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Indices.GetMapping(f),
		(client, f) => client.Indices.GetMappingAsync(f),
		(client, r) => client.Indices.GetMapping(r),
		(client, r) => client.Indices.GetMappingAsync(r)
	);

	protected override void ExpectResponse(GetMappingResponse response)
	{
		var projectMapping = response.Indices[Infer.Index<Project>()];

		projectMapping.Should().NotBeNull();

		var properties = projectMapping.Mappings.Properties;

		var leadDev = properties[Infer.Property<Project>(p => p.LeadDeveloper)];
		leadDev.Should().NotBeNull();

		var props = response.Indices["x"]?.Mappings.Properties;
		props.Should().BeNull();

		AssertExtensionMethods(response);
	}

	private static void AssertExtensionMethods(GetMappingResponse response)
	{
		/** The `GetMappingFor` extension method can be used to get a type mapping easily and safely */
		response.GetMappingFor<Project>().Should().NotBeNull();
		response.GetMappingFor(typeof(Project)).Should().NotBeNull();
	}
}

public class GetMappingNonExistentIndexApiTests : ApiIntegrationTestBase<ReadOnlyCluster, GetMappingResponse, GetMappingRequestDescriptor<Project>, GetMappingRequest>
{
	private const string NonExistentIndex = "non-existent-index";

	public GetMappingNonExistentIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => false;
	protected override int ExpectStatusCode => 404;
	protected override string ExpectedUrlPathAndQuery => $"/{NonExistentIndex}/_mapping";
	protected override HttpMethod HttpMethod => HttpMethod.GET;
	protected override Action<GetMappingRequestDescriptor<Project>> Fluent => c => c.Indices(NonExistentIndex);
	protected override GetMappingRequest Initializer => new(NonExistentIndex);

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Indices.GetMapping(f),
		(client, f) => client.Indices.GetMappingAsync(f),
		(client, r) => client.Indices.GetMapping(r),
		(client, r) => client.Indices.GetMappingAsync(r)
	);

	protected override void ExpectResponse(GetMappingResponse response)
	{
		response.Indices.Should().BeEmpty();
		response.ElasticsearchServerError.Should().NotBeNull();
	}
}
