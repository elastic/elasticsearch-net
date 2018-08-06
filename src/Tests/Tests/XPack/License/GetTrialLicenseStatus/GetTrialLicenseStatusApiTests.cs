using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.License.GetTrialLicenseStatus
{
	[SkipVersion("<6.1.0", "Only exists in Elasticsearch 6.1.0+")]
	public class GetTrialLicenseStatusApiTests : ApiIntegrationTestBase<XPackCluster, IGetTrialLicenseStatusResponse, IGetTrialLicenseStatusRequest, GetTrialLicenseStatusDescriptor, GetTrialLicenseStatusRequest>
	{
		public GetTrialLicenseStatusApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetTrialLicenseStatus(f),
			fluentAsync: (client, f) => client.GetTrialLicenseStatusAsync(f),
			request: (client, r) => client.GetTrialLicenseStatus(r),
			requestAsync: (client, r) => client.GetTrialLicenseStatusAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_xpack/license/trial_status";
		protected override bool SupportsDeserialization => false;

		protected override void ExpectResponse(IGetTrialLicenseStatusResponse response)
		{
			response.ShouldBeValid();
			response.EligibleToStartTrial.Should().BeFalse();
		}
	}
}
