// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.PointInTime.Open
{
	[SkipVersion("<7.10.0", "Api introduced in 7.10.0")]
	public class OpenPointInTimeApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, OpenPointInTimeResponse, IOpenPointInTimeRequest, OpenPointInTimeDescriptor, OpenPointInTimeRequest>
	{
		public OpenPointInTimeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		
		protected override int ExpectStatusCode => 200;

		protected override Func<OpenPointInTimeDescriptor, IOpenPointInTimeRequest> Fluent => c => c
			.Index("devs")
			.KeepAlive("1m");

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override OpenPointInTimeRequest Initializer => new("devs")
		{
			KeepAlive = "1m"
		};

		protected override string UrlPath => "/devs/_pit?keep_alive=1m";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.OpenPointInTime(selector: f),
			(c, f) => c.OpenPointInTimeAsync(selector: f),
			(c, r) => c.OpenPointInTime(r),
			(c, r) => c.OpenPointInTimeAsync(r)
		);

		protected override void ExpectResponse(OpenPointInTimeResponse response) =>
			response.Id.Should().NotBeNullOrEmpty();
	}
}
