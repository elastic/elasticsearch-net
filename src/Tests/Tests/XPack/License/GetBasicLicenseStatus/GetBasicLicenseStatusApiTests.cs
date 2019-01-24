using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.License.GetBasicLicenseStatus
{
	[SkipVersion("<6.5.0", "")]
	public class GetBasicLicenseStatusApiTests
		: ApiIntegrationTestBase<XPackCluster, IGetBasicLicenseStatusResponse, IGetBasicLicenseStatusRequest, GetBasicLicenseStatusDescriptor, GetBasicLicenseStatusRequest>
	{
		public GetBasicLicenseStatusApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => GET;

		protected override GetBasicLicenseStatusRequest Initializer => new GetBasicLicenseStatusRequest();

		protected override string UrlPath => $"/_xpack/license/basic_status";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetBasicLicenseStatus(f),
			(client, f) => client.GetBasicLicenseStatusAsync(f),
			(client, r) => client.GetBasicLicenseStatus(r),
			(client, r) => client.GetBasicLicenseStatusAsync(r)
		);

		protected override void ExpectResponse(IGetBasicLicenseStatusResponse response) =>
			response.EligableToStartBasic.Should().BeTrue();
	}
}
