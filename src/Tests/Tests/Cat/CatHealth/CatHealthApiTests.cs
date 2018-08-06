using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Cat.CatHealth
{
	public class CatHealthApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatHealthRecord>, ICatHealthRequest, CatHealthDescriptor, CatHealthRequest>
	{
		public CatHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatHealth(),
			fluentAsync: (client, f) => client.CatHealthAsync(),
			request: (client, r) => client.CatHealth(r),
			requestAsync: (client, r) => client.CatHealthAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/health";

		protected override void ExpectResponse(ICatResponse<CatHealthRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));
		}
	}

	public class CatHealthNoTimestampApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatHealthRecord>, ICatHealthRequest, CatHealthDescriptor, CatHealthRequest>
	{
		public CatHealthNoTimestampApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatHealth(f),
			fluentAsync: (client, f) => client.CatHealthAsync(f),
			request: (client, r) => client.CatHealth(r),
			requestAsync: (client, r) => client.CatHealthAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/health?ts=false";

		protected override Func<CatHealthDescriptor, ICatHealthRequest> Fluent => s => s
			.IncludeTimestamp(false);

		protected override CatHealthRequest Initializer => new CatHealthRequest
		{
			IncludeTimestamp = false
		};

		protected override void ExpectResponse(ICatResponse<CatHealthRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));

			foreach (var record in response.Records)
			{
				record.Timestamp.Should().BeNullOrWhiteSpace();
			}
		}
	}

}
