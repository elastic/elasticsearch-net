// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.IndexManagement.AliasManagement;

public class GetAliasApiTests : ApiIntegrationTestBase<ReadOnlyCluster, GetAliasResponse, GetAliasRequestDescriptor, GetAliasRequest>
{
	private static readonly Names Names = Infer.Names(DefaultSeeder.ProjectsAliasName);

	public GetAliasApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;
	protected override HttpMethod ExpectHttpMethod => HttpMethod.GET;
	protected override bool SupportsDeserialization => false;
	protected override string ExpectedUrlPathAndQuery => $"_all/_alias/{DefaultSeeder.ProjectsAliasName}";

	protected override GetAliasRequest Initializer => new(Indices.All, Names);
	protected override Action<GetAliasRequestDescriptor> Fluent => d => d.Name(Names);

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Indices.GetAlias(Indices.All, f),
		(client, f) => client.Indices.GetAliasAsync(Indices.All, f),
		(client, r) => client.Indices.GetAlias(r),
		(client, r) => client.Indices.GetAliasAsync(r)
	);

	protected override void ExpectResponse(GetAliasResponse response)
	{
		response.Aliases.Should().NotBeEmpty($"expect to find indices pointing to {DefaultSeeder.ProjectsAliasName}");
		var indexAliases = response.Aliases[Infer.Index<Project>()];
		indexAliases.Should().NotBeNull("expect to find alias for project");
		indexAliases.Aliases.Should().NotBeEmpty("expect to find aliases dictionary definitions for project");
		var alias = indexAliases.Aliases[DefaultSeeder.ProjectsAliasName];
		alias.Should().NotBeNull();
	}
}

// TODO: Support exception information from specification and avoid default error response deserialization in transport

//public class GetAliasPartialMatchApiTests : ApiIntegrationTestBase<ReadOnlyCluster, GetAliasResponse, GetAliasRequestDescriptor, GetAliasRequest>
//{
//	private static readonly Names Names = Infer.Names(DefaultSeeder.ProjectsAliasName, "x", "y");

//	public GetAliasPartialMatchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

//	protected override bool ExpectIsValid => true;
//	protected override int ExpectStatusCode => 404;
//	protected override HttpMethod ExpectHttpMethod => HttpMethod.GET;
//	protected override bool SupportsDeserialization => false;
//	protected override string ExpectedUrlPathAndQuery => $"_all/_alias/{DefaultSeeder.ProjectsAliasName}%2Cx%2Cy";

//	protected override GetAliasRequest Initializer => new(Indices.All, Names);
//	protected override Action<GetAliasRequestDescriptor> Fluent => d => d.Name(Names);

//	protected override LazyResponses ClientUsage() => Calls(
//		(client, f) => client.Indices.GetAlias(Indices.All, f),
//		(client, f) => client.Indices.GetAliasAsync(Indices.All, f),
//		(client, r) => client.Indices.GetAlias(r),
//		(client, r) => client.Indices.GetAliasAsync(r)
//	);

//	protected override void ExpectResponse(GetAliasResponse response)
//	{
//		response.Aliases.Should().NotBeNull();
//		response.Aliases.Count.Should().BeGreaterThan(0);
//	}
//}

public class GetAliasNotFoundApiTests : ApiIntegrationTestBase<ReadOnlyCluster, GetAliasResponse, GetAliasRequestDescriptor, GetAliasRequest>
{
	private static readonly Names Names = Infer.Names("bad-alias");

	public GetAliasNotFoundApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => false;
	protected override int ExpectStatusCode => 404;
	protected override HttpMethod ExpectHttpMethod => HttpMethod.GET;
	protected override bool SupportsDeserialization => false;
	protected override string ExpectedUrlPathAndQuery => $"_all/_alias/bad-alias";

	protected override GetAliasRequest Initializer => new(Indices.All, Names);
	protected override Action<GetAliasRequestDescriptor> Fluent => d => d.Name(Names);

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Indices.GetAlias(Indices.All, f),
		(client, f) => client.Indices.GetAliasAsync(Indices.All, f),
		(client, r) => client.Indices.GetAlias(r),
		(client, r) => client.Indices.GetAliasAsync(r)
	);

	protected override void ExpectResponse(GetAliasResponse response)
	{
		response.ElasticsearchServerError.Should().NotBeNull();
		response.ElasticsearchServerError.Error.Reason.Should().Contain("missing");

		response.Aliases.Should().NotBeNull();
		response.Aliases.Should().BeEmpty();
	}
}
