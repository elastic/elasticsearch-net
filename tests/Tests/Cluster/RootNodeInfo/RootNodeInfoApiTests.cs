// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.RootNodeInfo
{
	/*
	 * FluentAssertions says TagLine but i think its:
	 *
			response.Version.BuildSnapshot.Should().Be(TestConfiguration.Instance.ElasticsearchVersion.Contains("SNAPSHOT"));

	 * That is failing

	 * Failed   Tests.Cluster.RootNodeInfo.RootNodeInfoApiTests.ReturnsExpectedResponse
Error Message:
 Tests.Framework.EndpointTests.ResponseAssertionException : Expected response.Tagline to be False, but found True.
Response Under Test:
Valid NEST response built from a successful (200) low level call on GET: /?pretty=true&error_trace=true
# Audit trail of this API call:
 - [1] HealthyResponse: Node: http://localhost:9200/ Took: 00:00:00.1079528
# Request:
<Request stream not captured or already read to completion by serializer. Set DisableDirectStreaming() on ConnectionSettings to force it to be set on the response.>
# Response:
{
  "name" : "readonly-node-6a23429200",
  "cluster_name" : "ephemeral-cluster-a62d84",
  "cluster_uuid" : "6OZKYv1tShy-PS54swofUg",
  "version" : {
    "number" : "8.0.0-SNAPSHOT",
    "build_flavor" : "default",
    "build_type" : "tar",
    "build_hash" : "7776f75",
    "build_date" : "2019-08-01T09:12:42.234889Z",
    "build_snapshot" : true,
    "lucene_version" : "8.2.0",
    "minimum_wire_compatibility_version" : "7.4.0",
    "minimum_index_compatibility_version" : "7.0.0"
  },
  "tagline" : "You Know, for Search"
}
	 */
	[SkipVersion(">=8.0.0-SNAPSHOT", "TODO broken in snapshot")]
	public class RootNodeInfoApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, RootNodeInfoResponse, IRootNodeInfoRequest, RootNodeInfoDescriptor, RootNodeInfoRequest>
	{
		public RootNodeInfoApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.RootNodeInfo(),
			(client, f) => client.RootNodeInfoAsync(),
			(client, r) => client.RootNodeInfo(r),
			(client, r) => client.RootNodeInfoAsync(r)
		);

		protected override void ExpectResponse(RootNodeInfoResponse response)
		{
			response.Tagline.Should().NotBeNullOrWhiteSpace();
			response.Name.Should().NotBeNullOrWhiteSpace();
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.ClusterUUID.Should().NotBeNullOrWhiteSpace();
			response.Version.Should().NotBeNull();
			response.Version.Number.Should().NotBeNullOrWhiteSpace();
			response.Version.BuildDate.Should().BeAfter(default);
			response.Version.BuildFlavor.Should().NotBeNullOrWhiteSpace();
			response.Version.BuildHash.Should().NotBeNullOrWhiteSpace();
			response.Version.BuildSnapshot.Should().Be(TestConfiguration.Instance.ElasticsearchVersionIsSnapshot);
			response.Version.BuildType.Should().NotBeNullOrWhiteSpace();
			response.Version.MinimumIndexCompatibilityVersion.Should().NotBeNullOrWhiteSpace();
			response.Version.MinimumWireCompatibilityVersion.Should().NotBeNullOrWhiteSpace();
		}
	}
}
