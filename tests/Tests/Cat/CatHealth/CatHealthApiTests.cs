// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatHealth
{
	public class CatHealthApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatHealthRecord>, ICatHealthRequest, CatHealthDescriptor, CatHealthRequest>
	{
		public CatHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/health";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Health(),
			(client, f) => client.Cat.HealthAsync(),
			(client, r) => client.Cat.Health(r),
			(client, r) => client.Cat.HealthAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatHealthRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));
	}

	public class CatHealthNoTimestampApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatHealthRecord>, ICatHealthRequest, CatHealthDescriptor, CatHealthRequest>
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
			(client, f) => client.Cat.Health(f),
			(client, f) => client.Cat.HealthAsync(f),
			(client, r) => client.Cat.Health(r),
			(client, r) => client.Cat.HealthAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatHealthRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));

			foreach (var record in response.Records) record.Timestamp.Should().BeNullOrWhiteSpace();
		}
	}
}
