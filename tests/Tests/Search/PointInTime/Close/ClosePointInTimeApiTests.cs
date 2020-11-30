// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.PointInTime.Close
{
	[SkipVersion("<7.10.0", "Api introduced in 7.10.0")]
	public class ClosePointInTimeApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClosePointInTimeResponse, IClosePointInTimeRequest, ClosePointInTimeDescriptor, ClosePointInTimeRequest>
	{
		public ClosePointInTimeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private const string IdKey = "Id";
		
		protected override void OnBeforeCall(IElasticClient client)
		{
			var pit = client.OpenPointInTime("devs", p => p.KeepAlive("5m"));
			pit.ShouldBeValid();
			ExtendedValue(IdKey, pit.Id);
		}

		protected override bool ExpectIsValid => true;
		
		protected override int ExpectStatusCode => 200;

		protected override Func<ClosePointInTimeDescriptor, IClosePointInTimeRequest> Fluent => c => c
			.Id(RanIntegrationSetup ? ExtendedValue<string>(IdKey) : RandomString());

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override ClosePointInTimeRequest Initializer => new()
		{
			Id = RanIntegrationSetup ? ExtendedValue<string>(IdKey) : RandomString()
		};

		protected override string UrlPath => "/_pit";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.ClosePointInTime(f),
			(c, f) => c.ClosePointInTimeAsync(f),
			(c, r) => c.ClosePointInTime(r),
			(c, r) => c.ClosePointInTimeAsync(r)
		);

		protected override void ExpectResponse(ClosePointInTimeResponse response)
		{
			response.Succeeded.Should().BeTrue();
			response.NumberFreed.Should().BeGreaterOrEqualTo(1);
		}
	}
}
