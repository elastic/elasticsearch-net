using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cat.CatHealth
{
	public class CatHealthApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatHealthRecord>, ICatHealthRequest, CatHealthDescriptor, CatHealthRequest>
	{
		public CatHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/health";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatHealth(),
			(client, f) => client.CatHealthAsync(),
			(client, r) => client.CatHealth(r),
			(client, r) => client.CatHealthAsync(r)
		);

		protected override void ExpectResponse(ICatResponse<CatHealthRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));
	}

	public class CatHealthNoTimestampApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatHealthRecord>, ICatHealthRequest, CatHealthDescriptor, CatHealthRequest>
	{
		public CatHealthNoTimestampApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<CatHealthDescriptor, ICatHealthRequest> Fluent => s => s
			.IncludeTimestamp(false);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override CatHealthRequest Initializer => new CatHealthRequest
		{
			IncludeTimestamp = false
		};

		protected override string UrlPath => "/_cat/health?ts=false";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CatHealth(f),
			(client, f) => client.CatHealthAsync(f),
			(client, r) => client.CatHealth(r),
			(client, r) => client.CatHealthAsync(r)
		);

		protected override void ExpectResponse(ICatResponse<CatHealthRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));

			foreach (var record in response.Records) record.Timestamp.Should().BeNullOrWhiteSpace();
		}
	}
}
