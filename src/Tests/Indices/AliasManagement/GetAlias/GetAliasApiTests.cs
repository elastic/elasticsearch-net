using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.AliasManagement.GetAlias
{
	public class GetAliasApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetAliasesResponse, IGetAliasRequest, GetAliasDescriptor, GetAliasRequest>
	{
		private static readonly Names Names = Infer.Names("projects-alias", "alias, x", "y");

		public GetAliasApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetAlias(f),
			fluentAsync: (client, f) => client.GetAliasAsync(f),
			request: (client, r) => client.GetAlias(r),
			requestAsync: (client, r) => client.GetAliasAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_alias/projects-alias%2Calias%2Cx%2Cy";
		protected override void ExpectResponse(IGetAliasesResponse response)
		{
			response.Indices.Should().NotBeNull().And.ContainKey("project");
			var projectIndex = response.Indices["project"];
			projectIndex.Should().HaveCount(1);
			projectIndex.First().Name.Should().Be("projects-alias");
		}
		protected override bool SupportsDeserialization => false;

		protected override Func<GetAliasDescriptor, IGetAliasRequest> Fluent => d=>d
			.Name(Names)
		;
		protected override GetAliasRequest Initializer => new GetAliasRequest(Names);
	}

	public class GetAliasNotFoundApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IGetAliasesResponse, IGetAliasRequest, GetAliasDescriptor, GetAliasRequest>
	{
		private static readonly Names Names = Infer.Names("bad-alias");

		public GetAliasNotFoundApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetAlias(f),
			fluentAsync: (client, f) => client.GetAliasAsync(f),
			request: (client, r) => client.GetAlias(r),
			requestAsync: (client, r) => client.GetAliasAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_alias/bad-alias";
		protected override bool SupportsDeserialization => false;

		protected override Func<GetAliasDescriptor, IGetAliasRequest> Fluent => d=>d
			.Name(Names)
		;
		protected override GetAliasRequest Initializer => new GetAliasRequest(Names);

		protected override void ExpectResponse(IGetAliasesResponse response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Error.Reason.Should().Contain("missing");
			response.Indices.Should().NotBeNull();
		}

	}
}
