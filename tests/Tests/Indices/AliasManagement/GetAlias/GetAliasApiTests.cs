// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.AliasManagement.GetAlias
{
	public class GetAliasApiTests : ApiIntegrationTestBase<ReadOnlyCluster, GetAliasResponse, IGetAliasRequest, GetAliasDescriptor, GetAliasRequest>
	{
		private static readonly Names Names = Names(DefaultSeeder.ProjectsAliasName);

		public GetAliasApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;


		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_all/_alias/{DefaultSeeder.ProjectsAliasName}";

		protected override GetAliasRequest Initializer => new GetAliasRequest(Nest.Indices.All, Names);
		protected override Func<GetAliasDescriptor, IGetAliasRequest> Fluent => d => d.Name(Names);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.GetAlias(AllIndices, f),
			(client, f) => client.Indices.GetAliasAsync(AllIndices, f),
			(client, r) => client.Indices.GetAlias(r),
			(client, r) => client.Indices.GetAliasAsync(r)
		);

		protected override void ExpectResponse(GetAliasResponse response)
		{
			response.Indices.Should().NotBeEmpty($"expect to find indices pointing to {DefaultSeeder.ProjectsAliasName}");
			var indexAliases = response.Indices[Index<Project>()];
			indexAliases.Should().NotBeNull("expect to find alias for project");
			indexAliases.Aliases.Should().NotBeEmpty("expect to find aliases dictionary definitions for project");
			var alias = indexAliases.Aliases[DefaultSeeder.ProjectsAliasName];
			alias.Should().NotBeNull();
		}
	}

	public class GetAliasPartialMatchApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GetAliasResponse, IGetAliasRequest, GetAliasDescriptor, GetAliasRequest>
	{
		private static readonly Names Names = Names(DefaultSeeder.ProjectsAliasName, "x", "y");

		public GetAliasPartialMatchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => TestConfiguration.Instance.InRange("<5.5.0") ? 200 : 404;


		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_all/_alias/{DefaultSeeder.ProjectsAliasName}%2Cx%2Cy";

		protected override GetAliasRequest Initializer => new GetAliasRequest(Nest.Indices.All, Names);
		protected override Func<GetAliasDescriptor, IGetAliasRequest> Fluent => d => d.Name(Names);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.GetAlias(AllIndices, f),
			(client, f) => client.Indices.GetAliasAsync(AllIndices, f),
			(client, r) => client.Indices.GetAlias(r),
			(client, r) => client.Indices.GetAliasAsync(r)
		);

		protected override void ExpectResponse(GetAliasResponse response)
		{
			response.Indices.Should().NotBeNull();
			response.Indices.Count.Should().BeGreaterThan(0);
		}
	}

	public class GetAliasNotFoundApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, GetAliasResponse, IGetAliasRequest, GetAliasDescriptor, GetAliasRequest>
	{
		private static readonly Names Names = Names("bad-alias");

		public GetAliasNotFoundApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsDeserialization => false;

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_all/_alias/bad-alias";

		protected override GetAliasRequest Initializer => new GetAliasRequest(AllIndices, Names);
		protected override Func<GetAliasDescriptor, IGetAliasRequest> Fluent => d => d.Name(Names);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.GetAlias(AllIndices, f),
			(client, f) => client.Indices.GetAliasAsync(AllIndices, f),
			(client, r) => client.Indices.GetAlias(r),
			(client, r) => client.Indices.GetAliasAsync(r)
		);

		protected override void ExpectResponse(GetAliasResponse response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Error.Reason.Should().Contain("missing");

			response.IsValid.Should().BeFalse();

			response.Indices.Should().NotBeNull();
			response.Indices.Should().BeEmpty();
		}
	}
}
