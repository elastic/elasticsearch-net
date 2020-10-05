// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Transport.HttpMethod;

namespace Tests.XPack.License.GetBasicLicenseStatus
{
	[SkipVersion("<6.5.0", "")]
	[SkipOnCi]
	public class GetBasicLicenseStatusApiTests
		: ApiIntegrationTestBase<XPackCluster, GetBasicLicenseStatusResponse, IGetBasicLicenseStatusRequest, GetBasicLicenseStatusDescriptor, GetBasicLicenseStatusRequest>
	{
		public GetBasicLicenseStatusApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => GET;

		protected override GetBasicLicenseStatusRequest Initializer => new GetBasicLicenseStatusRequest();

		protected override string UrlPath => $"/_license/basic_status";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.License.GetBasicStatus(f),
			(client, f) => client.License.GetBasicStatusAsync(f),
			(client, r) => client.License.GetBasicStatus(r),
			(client, r) => client.License.GetBasicStatusAsync(r)
		);

		protected override void ExpectResponse(GetBasicLicenseStatusResponse response) =>
			response.EligableToStartBasic.Should().BeTrue();
	}
}
